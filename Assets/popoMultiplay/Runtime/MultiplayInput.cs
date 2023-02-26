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
    }
}