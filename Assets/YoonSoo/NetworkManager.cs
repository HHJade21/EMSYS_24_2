using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;
public delegate void OnMessageHandler(string message);
namespace DireRaven22075
{    public class NetworkManager : Singleton<NetworkManager>
    {
        private const bool isServer = true;
        protected override void Awake()
        {
            base.Awake();

        }
        private void StartServer()
        {

        }
    }
}
