using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BuildModeTrigger))]public class BuildModeScript : MonoBehaviour
{
    public void End(){
        this.enabled = false;
    }
}
