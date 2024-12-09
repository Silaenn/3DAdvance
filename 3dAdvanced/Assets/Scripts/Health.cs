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

    [Header("Safe Zone Logic")]
    public bool isOutsideSafeZone;
    [SerializeField] private float tickTimerDamageOutsideSafeZone;
    [SerializeField] private float defaultTickTimerDamageOutsideSafeZone;
    void Start()
    {
        CurrentHp = maxHp;
    }

    void Update()
    {
        DamageOutsideSafeZone();
    }

    void DamageOutsideSafeZone(){
        if(isOutsideSafeZone){
            tickTimerDamageOutsideSafeZone -= Time.deltaTime;
            if(tickTimerDamageOutsideSafeZone <= 0){
                TakeDamage(5);
                tickTimerDamageOutsideSafeZone = defaultTickTimerDamageOutsideSafeZone;
            }
        }
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
