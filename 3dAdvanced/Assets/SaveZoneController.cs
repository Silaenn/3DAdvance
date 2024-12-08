using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveZoneController : MonoBehaviour
{
    [SerializeField] private float defaultSize;
    [SerializeField] private float smallestSize;
    [SerializeField] private float shrinkingSpeed;
    void Start()
    {
        transform.localScale = new Vector3(defaultSize, defaultSize, defaultSize);
    }

    void Update()
    {
        if(transform.localScale.x >= smallestSize){
            transform.localScale -= new Vector3(shrinkingSpeed, 0, shrinkingSpeed) * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Health>()){
            other.GetComponent<Health>().isOutsideSafeZone = false;
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.GetComponent<Health>()){
            other.GetComponent<Health>().isOutsideSafeZone = true;
        }
    }
}
