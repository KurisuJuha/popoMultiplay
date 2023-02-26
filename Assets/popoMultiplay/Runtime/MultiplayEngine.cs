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
        private readonly string url;
        private readonly WebSocket webSocket;
        private int playerCount;

        public MultiplayEngine(string url, TickEventHandler OnTick = null, ConnectedEventHandler OnConnected = null, ClosedEventHandler OnClosed = null)
        {
            this.url = url;
            this.OnTick = OnTick;
            this.OnConnected = OnConnected;
            this.OnClosed = OnClosed;
            webSocket = new WebSocket(this.url);
            webSocket.OnOpen += () => OnConnected();
            webSocket.OnClose += closeCode => OnClosed(closeCode);
            webSocket.OnMessage += bytes => OnBytes(bytes);
        }

        public void Start()
        {
            webSocket.Connect();
        }

        public void End()
        {
            webSocket.Close();
            clients.Clear();
        }

    }
}