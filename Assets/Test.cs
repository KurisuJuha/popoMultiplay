using System.Linq;
using System.Collections;
using System.Collections.Generic;
using JuhaKurisu.PopoTools.Utility;
using JuhaKurisu.PopoTools.Multiplay;

public class Test : PopoBehaviour
{
    [UnityEngine.SerializeField] private string url;
    private TestLogic logic;
    private MultiplayEngine engine;

    protected override void Start()
    {
        logic = new TestLogic();
        engine = new(url, OnTick: logic.Tick);
    }
}
