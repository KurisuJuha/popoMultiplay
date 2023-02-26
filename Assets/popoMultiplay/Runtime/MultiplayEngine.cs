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

        private void OnBytes(byte[] bytes)
        {
            HashSet<ClientID> oldClientIDs = clients.Keys.ToHashSet();
            DataReader reader = new DataReader(bytes);

            // 人数を読み込む
            playerCount = reader.ReadInt();

            // 人数分読み込む
            for (int i = 0; i < playerCount; i++)
            {
                ClientID id = new(reader.ReadGuid());
                MultiplayInput input = new(reader.ReadBytes());

                // 見終えたクライアントは消しとく
                oldClientIDs.Remove(id);

                MultiplayClient client;

                // 既存のプレイヤーであればそのまま設定
                // 新規のプレイヤーであれば追加
                if (clients.ContainsKey(id)) client = clients[id];
                else client = new MultiplayClient(id);

                // 最新のinputをセット
                client.input = input;
            }

            // 残ったプレイヤーの存在を消す
            foreach (var id in oldClientIDs) clients.Remove(id);
        }
    }
}