namespace DireRaven22075
{
    public enum PacketType
    {
        None,
        Connect,
        Disconnect,
        Command,
        Sync,
    }
    public class Packet
    {
        public PacketType t { get; private set; }
        public byte[] data { get; private set; }

        public Packet(PacketType t, byte[] data)
        {
            this.t = t;
            this.data = data;
        }
        public void UpdateData(byte[] data)
        {
            this.data = data;
        }
        public void UpdateType(PacketType t)
        {
            this.t = t;
        }
    }
}