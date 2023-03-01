using System.Linq;
using System.Collections.Generic;
using JuhaKurisu.PopoTools.Multiplay;

public class TestLogic
{
    public Dictionary<ClientID, uint> counter = new();
    public int elapsedFrame;

    public void Tick(MultiplayClient[] clients)
    {
        HashSet<ClientID> clientIDHashSet = clients.Select(c => c.id).ToHashSet();

        foreach (var client in clients)
        {
            if (!counter.ContainsKey(client.id)) counter[client.id] = 0;

            if (client.input.spaceButton)
                counter[client.id]++;
        }

        ClientID[] removeKeys = counter.Keys.ToArray();
        foreach (var id in removeKeys)
            if (!clientIDHashSet.Contains(id)) counter.Remove(id);

        elapsedFrame++;
    }
}
