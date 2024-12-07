using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletDamage;

    [SerializeField] private GameObject bloodVfx;
  
    void Update()
    {
        transform.Translate(transform.forward * bulletSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Health>()){
            Instantiate(bloodVfx, transform.position, Quaternion.identity);
            other.GetComponent<Health>().TakeDamage(bulletDamage);
        }
    }
}
