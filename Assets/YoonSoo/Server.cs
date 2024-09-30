using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

namespace DireRaven22075
{    public class Server : Singleton<NetworkManager>
    {
        private const Boolean print = true;
        protected override void Awake()
        {
            base.Awake();
            StartServer();
        }

        private void StartServer()
        {

        }

        private void IncommingHandler()
        {
            TcpListener server = null;
            try
            {
                Int32 port = 13000;
                IPAddress localAddr = IPAddress.Parse("localhost");
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
            finally
            {
                server.Stop();
            }
        }
    }
}
