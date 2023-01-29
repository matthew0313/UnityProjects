using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashIndicator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animation());
    }

    IEnumerator Animation(){
        for(int i = 0 ; i < 100 ; i++){
            transform.position = new Vector3(transform.position.x, transform.position.y + 0.02f, transform.position.z);
            yield return new WaitForSeconds(0.01f);
        }
        Destroy(gameObject);
    }
}
