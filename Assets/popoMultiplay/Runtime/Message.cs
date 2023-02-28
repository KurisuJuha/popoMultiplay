using System.Linq;
using System.Collections.ObjectModel;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class Message
    {
        public readonly MessageType type;
        public readonly ReadOnlyCollection<byte> data;

        public Message(byte[] data)
        {
            this.data = new(data);
        }
    }
}