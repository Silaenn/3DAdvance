using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LootItemType{
    RIFLE,
    AMMO
}

public class LootItem : MonoBehaviour
{
    public LootItemType lootItemType;

   private void OnTriggerEnter(Collider other) {

    switch(lootItemType){
        case LootItemType.RIFLE: 
     if(other.GetComponent<CharackterTag>()){
        if(other.TryGetComponent(out PlayerShoot playerShoot)){
            playerShoot.OnGettingWeapon();
            Destroy(gameObject);
        } 
        if(other.TryGetComponent(out EnemyController enemyController)){
            enemyController.SetLookingForEnemyState();
            Destroy(gameObject);
        } 
     }; break;

     case LootItemType.AMMO: 
        if(other.GetComponent<PlayerShoot>()){
            other.GetComponent<PlayerShoot>().GetAmmo();
            Destroy(gameObject);
        }
        if(other.GetComponent<EnemyController>()){
            other.GetComponent<EnemyController>().GetAmmo();
            Destroy(gameObject);
        }
     break;
    }
   }
}
