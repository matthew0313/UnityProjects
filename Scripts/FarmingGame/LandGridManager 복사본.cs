using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandGridManager : MonoBehaviour
{
    public Land[,] land = new Land[65,65];
}
public struct Land{
    public bool tilled;
    public TilledLand tilled_script;
}
