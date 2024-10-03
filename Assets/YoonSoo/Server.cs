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
        private TcpClient[] clients = new TcpClient[Constants.maxConnection];

        private volatile int clientCount = 0;
        protected override void Awake()
        {
            base.Awake();
            server = new TcpListener(IPAddress.Any, Constants.port);
            Debug.Log("서버가 시작되었습니다. 포트 5000에서 대기 중...");
            server.Start();
            StartCoroutine(StartServer());
        }
        private IEnumerator StartServer()
        {
            while (true)
            {
                if (clientCount < Constants.maxConnection)
                {
                    TcpClient client = server.AcceptTcpClient();
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
                yield return null;
            }
        }
        private void HandleClient(TcpClient client, int index)
        {
            using (client)
            {
                using (NetworkStream stream = client.GetStream())
                {
                    do
                    {
                        Debug.Log(string.Format("{0} : {1}", stream.ReadByte(), index));
                    } while (true);
                }
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