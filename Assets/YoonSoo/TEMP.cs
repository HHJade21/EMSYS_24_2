using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

class Program
{
    private const int MaxClients = 4; // 최대 4개의 클라이언트
    private static TcpClient[] clients = new TcpClient[MaxClients];
    private static int clientCount = 0;

    static void Main()
    {
        TcpListener server = new TcpListener(IPAddress.Any, 5000);
        server.Start();
        Console.WriteLine("서버가 시작되었습니다. 포트 5000에서 대기 중...");

        while (true)
        {
            if (clientCount < MaxClients)
            {
                TcpClient client = server.AcceptTcpClient();
                clients[clientCount++] = client;
                Console.WriteLine($"클라이언트 {clientCount}이(가) 연결되었습니다.");
                Task.Run(() => HandleClient(client, clientCount - 1));
            }
            else
            {
                Console.WriteLine("최대 클라이언트 수에 도달했습니다. 추가 연결 거부.");
                TcpClient client = server.AcceptTcpClient();
                client.Close();
            }
        }
    }

    static void HandleClient(TcpClient client, int clientId)
    {
        NetworkStream stream = client.GetStream();
        byte[] buffer = new byte[1024];
        int bytesRead;

        try
        {
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine($"클라이언트 {clientId + 1}로부터 수신된 메시지: {message}");

                // 모든 클라이언트에게 메시지 전송
                foreach (var otherClient in clients)
                {
                    if (otherClient != null && otherClient != client)
                    {
                        byte[] response = Encoding.ASCII.GetBytes($"클라이언트 {clientId + 1}: {message}");
                        otherClient.GetStream().Write(response, 0, response.Length);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"클라이언트 {clientId + 1} 오류: {ex.Message}");
        }
        finally
        {
            client.Close();
            clients[clientId] = null;
            clientCount--;
            Console.WriteLine($"클라이언트 {clientId + 1}이(가) 연결을 종료했습니다.");
        }
    }
}
