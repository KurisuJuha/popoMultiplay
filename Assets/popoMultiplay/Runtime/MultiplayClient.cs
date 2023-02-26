using System;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public class MultiplayClient
    {
        public readonly Guid clientID;
        public MultiplayInput input { get; private set; }
    }
}