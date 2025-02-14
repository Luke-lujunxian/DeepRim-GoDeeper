using System.Collections.Generic;
using UnityEngine;
using Verse;

namespace DeepRim;

public class Command_TransferLayer : Command_Action
{
    public UndergroundManager manager;

    public Building_MiningShaft shaft;

    public override void ProcessInput(Event ev)
    {
        Find.WindowStack.Add(MakeMenu());
    }

    private FloatMenu MakeMenu()
    {
        var list = new List<FloatMenuOption>
        {
            new FloatMenuOption("None", delegate { shaft.transferLevel = 0; })
        };
        using var enumerator = manager.layersState.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var pair = enumerator.Current;
            if (pair.Value != null)
            {
                list.Add(new FloatMenuOption($"Layer at Depth:{pair.Key}0m",
                    delegate { shaft.transferLevel = pair.Key; }));
            }
        }

        return new FloatMenu(list);
    }
}