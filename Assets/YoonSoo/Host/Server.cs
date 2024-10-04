using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;

using UnityEngine;

namespace DireRaven22075
{
    public class Server : Singleton<NetworkManager>
    {
        private TcpListener server;
        [SerializeField]
        private TcpClient[] clients = new TcpClient[Constants.maxConnection];

        [SerializeField]
        private volatile int clientCount = 0;
        protected override void Awake()
        {
            base.Awake();
            server = new TcpListener(IPAddress.Any, Constants.port);
            Debug.Log($"서버가 시작되었습니다. 포트 {Constants.port}에서 대기 중...");
            server.Start();
            StartServer();
        }
        private async void StartServer()
        {
            while (clientCount < Constants.maxConnection)
            {
                if (clientCount < Constants.maxConnection)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    clients[clientCount++] = client;
                    Debug.Log($"클라이언트 {clientCount}이(가) 연결되었습니다.");
                    Thread thread = new Thread(() => HandleClient(client, clientCount - 1));
                    thread.Start();
                }
                else
                {
                    Debug.Log("최대 클라이언트 수에 도달했습니다. 추가 연결 거부.");
                    TcpClient client = server.AcceptTcpClient();
                    client.Close();
                }
            }
        }
        private void HandleClient(TcpClient client, int index)
        {
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            int bytesRead;
            try
            {
                while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0) // 데이터를 읽어들이는 동안 반복
                {
                    string receivedMessage = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                    Console.WriteLine(Constants.ip);
                    Console.WriteLine("수신된 메시지: " + receivedMessage + " : " + index);

                    // 클라이언트에게 응답 메시지 전송
                    byte[] sendData = Encoding.ASCII.GetBytes("서버 응답: " + receivedMessage);
                    stream.Write(sendData, 0, sendData.Length);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("클라이언트 연결 오류: " + e.Message);
            }
            finally
            {
                client.Close(); // 클라이언트와의 연결을 닫음
                Console.WriteLine("클라이언트 연결 종료.");
            }
        }
#if false
        #region setting value
        private const int maxConnection = 10;
        private const int port = 50001;

        #endregion
        #region private members
        private TcpListener tcpListener;
        private Thread tcpListenerThread;
        private TcpClient connectedTcpClient;
        #endregion

        protected override void Awake() {
            base.Awake();
             // Start TcpServer background thread
            tcpListener = new TcpListener(IPAddress.Loopback, port);
            
            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequest));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
        }

        // Update is called once per frame
        void Update()
        {
            if ()
        }

        // Runs in background TcpServerThread; Handles incomming TcpClient requests
        private void ListenForIncommingRequest()
        {
            try
            {
                // Create listener on 192.168.0.2 port 50001
                tcpListener = new TcpListener(IPAddress.Loopback, 50001);
                tcpListener.Start();
                Debug.Log("Server is listening");

                while (true)
                {
                    using (connectedTcpClient = tcpListener.AcceptTcpClient())
                    {
                        // Get a stream object for reading
                        using (NetworkStream stream = connectedTcpClient.GetStream())
                        {
                            // Read incomming stream into byte array.
                            do
                            {
                                Debug.Log(stream.ReadByte());
                                // TODO
                            } while (true);
                        }
                    }
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("SocketException " + socketException.ToString());
            }
        }
    }
}
#endif
    }
}