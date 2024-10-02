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
        #region private members
        private TcpListener tcpListener;
        private Thread tcpListenerThread;
        private TcpClient connectedTcpClient;
        #endregion

        protected override void Awake() {
            base.Awake();
             // Start TcpServer background thread
            tcpListenerThread = new Thread(new ThreadStart(ListenForIncommingRequest));
            tcpListenerThread.IsBackground = true;
            tcpListenerThread.Start();
        }

        // Update is called once per frame
        void Update()
        {

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
