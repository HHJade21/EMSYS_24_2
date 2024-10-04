using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using UnityEngine;

///필요한 이벤트 핸들러가 몇 개지...?
public delegate void OnMessageHandler(string message);
public delegate void OnConnectHandler();

namespace DireRaven22075
{    public class NetworkManager : Singleton<NetworkManager>
    {
        protected override void Awake()
        {
            base.Awake();

        }
        private void StartServer()
        {

        }
    }
}
