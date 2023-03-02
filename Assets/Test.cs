using System.Linq;
using JuhaKurisu.PopoTools.Utility;
using JuhaKurisu.PopoTools.Multiplay;
using JuhaKurisu.PopoTools.Extentions;
using UnityEngine;

public class Test : PopoBehaviour
{
    [SerializeField] private string url;
    [SerializeField] private TMPro.TMP_Text text;
    private TestLogic logic;
    private MultiplayEngine engine;

    protected override async void Start()
    {
        logic = new TestLogic();
        engine = new(url, OnTick: logic.Tick);
        await engine.Start();
    }

    protected override void Update()
    {
        engine.Dispatch();
        text.text = logic.counter
            .OrderBy(kvp => -(long)kvp.Value)
            .Select(kvp => $"{kvp.Key.id.ToString()}: {kvp.Value}")
            .Join("\n");
    }

    protected override async void OnDestroy()
    {
        await engine.End();
    }
}
