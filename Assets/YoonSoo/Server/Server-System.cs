using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Text;
using UnityEngine;
using System;
namespace DireRaven22075.Network
{
    public partial class Server : MonoBehaviour
    {
        private TcpListener server;
        private void Awake()
        {
            server = new TcpListener(IPAddress.Any, Constants.port);
            Debug.Log($"Server is running on port {Constants.port}");
            server.Start();
            Application.targetFrameRate = Constants.tickRate;
            Application.runInBackground = true;
        }
        private void Update()
        {
            if (server.Pending())
            {
                TcpClient client = server.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[client.ReceiveBufferSize];
                stream.Read(buffer, 0, client.ReceiveBufferSize);
                string data = Encoding.UTF8.GetString(buffer);
                Console.WriteLine($"Recived Data : {data}");
            }
        }

        private void OnApplicationQuit() {
            server.Stop();
            Console.WriteLine("Server is stopped");

        }
    }
}