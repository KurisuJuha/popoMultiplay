using System.Linq;
using JuhaKurisu.PopoTools.ByteSerializer;
using Input = UnityEngine.Input;
using KeyCode = UnityEngine.KeyCode;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public struct MultiplayInput
    {
        public bool spaceButton { get; private set; }
        public bool upButton { get; private set; }
        public bool downButton { get; private set; }
        public bool rightButton { get; private set; }
        public bool leftButton { get; private set; }

        internal MultiplayInput(byte[] bytes)
        {
            DataReader reader = new(bytes);

            spaceButton = reader.ReadBoolean();
            upButton = reader.ReadBoolean();
            downButton = reader.ReadBoolean();
            rightButton = reader.ReadBoolean();
            leftButton = reader.ReadBoolean();
        }

        internal byte[] Serialize()
        {
            DataWriter writer = new();

            writer.Append(spaceButton);
            writer.Append(upButton);
            writer.Append(downButton);
            writer.Append(rightButton);
            writer.Append(leftButton);

            return writer.bytes.ToArray();
        }

        internal static MultiplayInput NewInput()
        {
            MultiplayInput input = new MultiplayInput();

            input.spaceButton = Input.GetKey(KeyCode.Space);
            input.upButton = Input.GetKey(KeyCode.UpArrow);
            input.downButton = Input.GetKey(KeyCode.DownArrow);
            input.rightButton = Input.GetKey(KeyCode.RightArrow);
            input.leftButton = Input.GetKey(KeyCode.LeftArrow);

            return input;
        }
    }
}