using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutsceneTrigger : MonoBehaviour
{
    [SerializeField] int triggerCutscene;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("EE");
        if (other.tag == "Player")
        {
            GameManager.Instance.M_CutsceneManager.CutsceneStart(triggerCutscene);
        }
    }
}
