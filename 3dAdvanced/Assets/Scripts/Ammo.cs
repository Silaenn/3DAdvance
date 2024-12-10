using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    [SerializeField] protected int currentAmmo;
    [SerializeField] protected int defaultAmmo;
    [SerializeField] protected int maxAmmo;
    public bool isAmmo;
    public int CurrentAmmo {
        get=> currentAmmo;
        set{
            currentAmmo = value;
            if(currentAmmo >= maxAmmo){
                currentAmmo = maxAmmo;
            }
        }
    }

    public void GetAmmo(){
        CurrentAmmo += 50;
    }

}
