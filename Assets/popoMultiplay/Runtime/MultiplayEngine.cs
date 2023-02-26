using System.Linq;
using System.Collections.Generic;
using NativeWebSocket;
using JuhaKurisu.PopoTools.ByteSerializer;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class MultiplayEngine
    {
        private readonly Dictionary<ClientID, MultiplayClient> clients = new();
        private readonly TickEventHandler OnTick;
        private readonly ConnectedEventHandler OnConnected;
        private readonly ClosedEventHandler OnClosed;
        private int playerCount;
    }
}