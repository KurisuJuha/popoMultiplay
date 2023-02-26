using System.Linq;
using JuhaKurisu.PopoTools.Utility;
using JuhaKurisu.PopoTools.Multiplay;
using JuhaKurisu.PopoTools.Extentions;

public class Test : PopoBehaviour
{
    [UnityEngine.SerializeField] private string url;
    [UnityEngine.SerializeField] private TMPro.TMP_Text text;
    private TestLogic logic;
    private MultiplayEngine engine;

    protected override void Start()
    {
        logic = new TestLogic();
        engine = new(url, OnTick: logic.Tick);
        engine.Start();
    }

    protected override void Update()
    {
        text.text = logic.counter
            .OrderBy(kvp => kvp.Value)
            .Select(kvp => $"{kvp.Key.id.ToString()}: {kvp.Value}")
            .Join("\n");
    }
}
