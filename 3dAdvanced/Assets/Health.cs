using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Point")]
    [SerializeField] private float maxHp;
    [SerializeField] private float currentHp;
    public float CurrentHp{
        get => currentHp;
        set {
            currentHp = value;
            OnChangeHealth();

            if(currentHp <= 0){
                Dead();
            }
        }
    }

    public bool isDead;
    void Start()
    {
        currentHp = maxHp;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float damageAmount){
        if(isDead) return;

        CurrentHp -= damageAmount;
    }

    public virtual void OnChangeHealth(){

    }

    public virtual void Dead(){
        print("this carakter dead");
        isDead = true;
    }
}
