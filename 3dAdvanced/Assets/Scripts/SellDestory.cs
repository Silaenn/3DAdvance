using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SellDestory : MonoBehaviour
{
    [SerializeField] private float destoryDelayTime;
    void Start()
    {
        Destroy(gameObject, destoryDelayTime);   
    }
}
