using System;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public struct MultiplayClient
    {
        public readonly Guid clientID;
        public MultiplayInput input { get; private set; }
    }
}