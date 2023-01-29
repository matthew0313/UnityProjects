using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryFunction : MonoBehaviour
{
    [SerializeField] BuildingStorage Storage;
    [SerializeField] List<Storage> ItemRequired;
    void Awake(){
        Storage.a.AddListener(() => {

        });
    }
}
