using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Functions
{
    public static void Rotate(this Transform O, float value){
        O.eulerAngles = new Vector3(0.0f, 0.0f, O.eulerAngles.z + value);
    }
}
