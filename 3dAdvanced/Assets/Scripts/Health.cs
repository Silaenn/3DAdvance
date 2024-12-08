using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [Header("Health Point")]
    [SerializeField] protected float maxHp;
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
    [HideInInspector] public UnityEvent onDead;
    void Start()
    {
        CurrentHp = maxHp;
    }

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
        onDead?.Invoke();
        isDead = true;
        GameManager.Instance.DecreaseAliveCharacter(transform);
    }
}
