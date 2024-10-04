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
    public partial class Client : MonoBehaviour
    {
        private TcpClient connection;
        private void Start()
        {
            try
            {
                this.connection = new TcpClient(Constants.ip, Constants.port);
                Debug.Log($"Connected to server {this.gameObject.name}");
            }
            catch (Exception e)
            {
                Debug.Log($"Failed to connect to server: {e}");
            }
        }
        private void Update()
        {
            Debug.Log($"{this.connection.Available} :: {this.connection.Connected}");
            if (this.connection.Available < 0)
            {
                this.connection.Connect(Constants.ip, Constants.port);
                Debug.Log($"{gameObject.name} is waiting for message...");
            }
            else
            {
                SendMessage(bytes);
            }
        }
        private void SendMessage(Byte[] buffer)
        {
            try
            {
                NetworkStream stream = this.connection.GetStream();
                if (stream.CanWrite)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
            }
            catch (SocketException e)
            {
                Debug.Log($"Socket exception: {e}");
            }
        }
#if false
        private TcpClient socketConnection;
        private void ConnectToTcpServer()
        {
            try
            {
                socketConnection = new TcpClient(Constants.ip, Constants.port);
            }
            catch (Exception e)
            {
                Debug.Log("On client connect exception " + e);
            }
        }

        private void Update()
        {
            if (socketConnection == null)
            {
                ConnectToTcpServer();
            }
            if (socketConnection.Available <= 0)
            {
                Debug.Log($"{gameObject.name} is waiting for message...");
            }
            else
            {
                SendMessage(new byte[] { 0x01, 0x02, 0x03 });
            }
        }
        /// Send message to server using socket connection.
        private void SendMessage(Byte[] buffer)
        {
            if (socketConnection == null)
            {
                Console.WriteLine("socketConnection is null");
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
#endif
    }
}
