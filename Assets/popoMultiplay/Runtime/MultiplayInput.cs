using System.Linq;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public struct MultiplayInput
    {
        public readonly bool spaceButton;

        internal MultiplayInput(byte[] bytes)
        {
            DataReader reader = new(bytes);

            spaceButton = reader.ReadBoolean();
        }

        internal byte[] Serialize()
        {
            DataWriter writer = new();

            writer.Append(spaceButton);

            return writer.bytes.ToArray();
        }

        internal static MultiplayInput NewInput()
        {
            return new MultiplayInput();
        }
    }
}