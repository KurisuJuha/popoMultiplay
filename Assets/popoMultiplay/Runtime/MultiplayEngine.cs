using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using NativeWebSocket;
using JuhaKurisu.PopoTools.ByteSerializer;
using JuhaKurisu.PopoTools.Multiplay.Extentions;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class MultiplayEngine
    {
        public readonly string url;
        public ReadOnlyCollection<MultiplayClient> clientArray => new(clients.Values.ToArray());
        public ReadOnlyDictionary<ClientID, MultiplayClient> clientDictionary => new(clients);
        public int playerCount { get; private set; }
        private readonly Dictionary<ClientID, MultiplayClient> clients = new();
        private readonly TickEventHandler OnTick;
        private readonly ConnectedEventHandler OnConnected;
        private readonly ClosedEventHandler OnClosed;
        private readonly WebSocket webSocket;

        public MultiplayEngine(string url, TickEventHandler OnTick = null, ConnectedEventHandler OnConnected = null, ClosedEventHandler OnClosed = null)
        {
            this.url = url;
            this.OnTick = OnTick;
            this.OnConnected = OnConnected;
            this.OnClosed = OnClosed;
            webSocket = new WebSocket(this.url);
            webSocket.OnOpen += () => UnityEngine.Debug.Log("open");
            webSocket.OnOpen += () => OnConnected?.Invoke();
            webSocket.OnClose += closeCode => UnityEngine.Debug.Log($"close: {closeCode}");
            webSocket.OnClose += closeCode => OnClosed?.Invoke(closeCode);
            webSocket.OnMessage += bytes => OnMessage(bytes);
            webSocket.OnError += e => UnityEngine.Debug.Log(e);
        }

        public async Task Start()
        {
            await webSocket.Connect();
        }

        public void Dispatch()
        {
#if !UNITY_WEBGL || UNITY_EDITOR
            webSocket.DispatchMessageQueue();
#endif
        }

        public async Task End()
        {
            await webSocket.Close();
            clients.Clear();
        }

        private void SendInput()
        {
            webSocket.Send(MultiplayInput.NewInput().Serialize());
        }

        private void OnMessage(byte[] bytes)
        {
            // メッセージを読む
            ReadMessage(bytes);

            // サーバーに最新のinput情報を送る
            SendInput();
        }

        private void ReadMessage(byte[] bytes)
        {
            DataReader reader = new(bytes);
            Message message = reader.ReadMessage();

            switch (message.type)
            {
                case MessageType.Input:
                    ReadInputMessage(message.data);
                    // ロジックを実行
                    OnTick.Invoke(clients.Values.ToArray());
                    break;
                case MessageType.InputLog:
                    ReadInputLogMessage(message.data);
                    break;

            }
        }

        private void ReadInputLogMessage(byte[] bytes)
        {
            DataReader reader = new(bytes);

            int logCount = reader.ReadInt();

            for (int i = 0; i < logCount; i++)
            {
                ReadInputMessage(reader.ReadBytes());
                // ロジックを実行
                OnTick.Invoke(clients.Values.ToArray());
            }
        }

        private void ReadInputMessage(byte[] bytes)
        {
            HashSet<ClientID> oldClientIDs = clients.Keys.ToHashSet();
            DataReader reader = new DataReader(bytes);

            // 人数を読み込む
            playerCount = reader.ReadInt();

            // 人数分読み込む
            for (int i = 0; i < playerCount; i++)
            {
                // idを読む
                ClientID id = new(reader.ReadGuid());

                MultiplayInput input = new MultiplayInput();

                // inputを読む
                byte[] inputBytes = reader.ReadBytes();
                if (inputBytes.Length != 0) input = new(inputBytes);

                // 見終えたクライアントは消しとく
                oldClientIDs.Remove(id);

                MultiplayClient client;

                // 既存のプレイヤーであればそのまま設定
                // 新規のプレイヤーであれば追加
                if (clients.ContainsKey(id)) client = clients[id];
                else
                {
                    client = new MultiplayClient(id);
                    clients[id] = client;
                }

                // 最新のinputをセット
                client.input = input;
            }

            // 残ったプレイヤーの存在を消す
            foreach (var id in oldClientIDs) clients.Remove(id);
        }
    }
}