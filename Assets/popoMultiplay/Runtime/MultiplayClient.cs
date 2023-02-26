namespace JuhaKurisu.PopoTools.Multiplay
{
    public class MultiplayClient
    {
        public readonly ClientID id;
        public MultiplayInput input { get; internal set; }

        public MultiplayClient(ClientID id)
        {
            this.id = id;
        }
    }
}