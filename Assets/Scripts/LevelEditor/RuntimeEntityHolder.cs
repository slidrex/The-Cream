using Assets.Scripts.Entities.Placeable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class RuntimeEntityHolder : ObjectHolder
{
    public void Configure(int manacost)
    {
        Cost.text = manacost.ToString();
    }
}
