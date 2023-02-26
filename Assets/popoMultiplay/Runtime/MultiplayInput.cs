using System.Linq;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public struct MultiplayInput
    {
        public readonly bool testButton;

        internal MultiplayInput(byte[] bytes)
        {
            DataReader reader = new(bytes);

            testButton = reader.ReadBoolean();
        }

        internal byte[] Serialize()
        {
            DataWriter writer = new();

            writer.Append(testButton);

            return writer.bytes.ToArray();
        }

        internal static MultiplayInput NewInput()
        {
            return new MultiplayInput();
        }
    }
}