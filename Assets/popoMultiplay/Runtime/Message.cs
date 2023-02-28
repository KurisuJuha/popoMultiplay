using System.Collections.ObjectModel;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class Message
    {
        public readonly MessageType type;
        public readonly ReadOnlyCollection<byte> data;

        public Message(MessageType type, byte[] data)
        {
            this.data = new(data);
        }
    }
}