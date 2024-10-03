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
    public class Client : Singleton<NetworkManager>
    {
        private TcpClient socketConnection;

        [SerializeField]
        private KeyCode press;
        [SerializeField]
        private Byte[] bytes;
        public void Start()
        {
        }

        private void ConnectToTcpServer()
        {
            try
            {
                socketConnection = new TcpClient("127.0.0.1", Constants.port);
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception " + e);
            }
        }
        private void Update()
        {
            if (Input.GetKeyDown(press))
            {
                ConnectToTcpServer();
                SendMessage(bytes);
            }
        }
        /// Send message to server using socket connection.     
        private void SendMessage(Byte[] buffer)
        {
            if (socketConnection == null)
            {
                return;
            }
            try
            {
                // Get a stream object for writing.             
                NetworkStream stream = socketConnection.GetStream();
                if (stream.CanWrite)
                {
                    // Write byte array to socketConnection stream.                 
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (SocketException socketException)
            {
                Debug.Log("Socket exception: " + socketException);
            }
        }
    }
}
