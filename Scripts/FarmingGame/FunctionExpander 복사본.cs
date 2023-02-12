using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FunctionExpander
{
    public static void Hide(this GameObject O){
        O.SetActive(false);
    }
    public static void Show(this GameObject O){
        O.SetActive(true);
    }
}
