using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DroppedItem : MonoBehaviour
{
    public ItemName Type;
}
[Flags] public enum ItemName{
    Metal = 1 << 0,
    Copper = 1 << 1
}
