namespace DireRaven22075
{
    public static partial class Constants
    {
        public struct Network
        {
            /// <summary>
            /// The port number to connect to the server
            /// P2P 연결을 위해 사용하는 포트 번호
            /// </summary>
            public const int port = 50001;
            /// <summary>
            /// The max connection count in the server
            /// P2P 연결을 위한 최대 연결 수
            /// </summary>
            public const int maxConnection = 10;
            /// <summary>
            /// Tickrate for the network sync
            /// 네트워크 동기화를 위한 틱 (1초에 실행될 횟수)
            /// </summary>
            public const int tick = 120;
            /// <summary>
            /// The buffer size for the network stream
            /// 네트워크 버퍼 크기
            /// </summary>
            public const int bufferSize = 1024;
        }
    }
}