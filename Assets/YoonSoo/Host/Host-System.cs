using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

namespace DireRaven22075
{
    public class Host : MonoBehaviour
    {
        #region Variables
        private volatile int count = 0;
        private IPAddress[] connection = new IPAddress[Constants.Network.maxConnection];
        private TcpClient[] clients = new TcpClient[Constants.Network.maxConnection];
        private TcpListener host;

        private IPEndPoint endPoint;
        private volatile bool isWaiting = false;
        private object lockObject = new object();
        #endregion
        protected void Awake()
        {
            
            host = new TcpListener(IPAddress.Any, Constants.Network.port);
            host.Start();
            Thread thread = new Thread(() => Waiting());
            thread.Start();
        }

        private void Waiting()
        {
            while (true)
            {
                while (count < Constants.Network.maxConnection)
                {

                    lock (lockObject)
                    {
                        isWaiting = false;
                        Debug.Log($"Client {count} is connected.");
                        isWaiting = true;
                    }
                }
            }
        }
        private void Update()
        {
            if (!isWaiting)
            {
                isWaiting = true;
                Thread thread = new Thread(() => AcceptClient());
                thread.Start();
            }
        }
        private void AcceptClient()
        {
            if (count < Constants.maxConnection)
            {
                isWaiting = false;
                TcpClient client = host.AcceptTcpClient();
                clients[count++] = client;
                Debug.Log($"Client {count} is connected.");
                Thread thread = new Thread(() => HandleClient(client));
                thread.Start();
                isWaiting = true;
            }
            else
            {
                Debug.Log("Maximum client count reached. Additional connection denied.");
                TcpClient client = host.AcceptTcpClient();
                client.Close();
            }
            isWaiting = false;
        }
        private void HandleClient(TcpClient client)
        {
            using (NetworkStream stream = client.GetStream())
            {
                while (true)
                {
                    Debug.Log(stream.ReadByte());
                }
            }
        }
    }
}