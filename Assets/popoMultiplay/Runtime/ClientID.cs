using System;

namespace JuhaKurisu.PopoTools.Multiplay
{
    public struct ClientID
    {
        public readonly Guid id;

        public ClientID(Guid id)
        {
            this.id = id;
        }

        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}