using System.Collections.Generic;
using JuhaKurisu.PopoTools.Multiplay;

public class TestLogic
{
    public Dictionary<ClientID, ulong> counter = new();
    public int elapsedFrame;

    public void Tick(MultiplayClient[] clients)
    {
        foreach (var client in clients)
        {
            if (client.input.spaceButton)
                counter[client.id]++;
        }

        elapsedFrame++;
    }
}
