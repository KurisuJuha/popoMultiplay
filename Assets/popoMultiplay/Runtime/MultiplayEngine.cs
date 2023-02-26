using System.Linq;
using System.Collections.Generic;
using NativeWebSocket;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class MultiplayEngine
    {
        private readonly Dictionary<ClientID, MultiplayClient> clients = new();
        private int playerCount;
    }
}