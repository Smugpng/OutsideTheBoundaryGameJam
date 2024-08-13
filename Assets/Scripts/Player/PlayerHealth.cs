using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private CapsuleCollider hitbox;
    [SerializeField] private GameManager gameManager;
    private PlayerController controller;
    [Header("Health must be 1-10")]
    public float maxHealth = 10;

    [SerializeField] private float health;
    [SerializeField] public float drownTimer =0;
    private bool dmgCooldown = false;
    private void Awake()
    {
        hitbox = GetComponent<CapsuleCollider>();
        controller = GetComponent<PlayerController>();
    }
    private void Start()
    {
        health = maxHealth;
    }
    private void Update()
    {
        DrownTimer();
        if((drownTimer >= 30) && !dmgCooldown)
        {
            dmgCooldown = true;
            TakeDmg();
        }
        if(health <= 0)
        {
            gameManager.GoToMainMenu();
        }
    }
    void DrownTimer()
    {
        if (controller.inWater)
        {
            drownTimer += Time.deltaTime;
        }
        else
        {
            drownTimer = 0;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if ((other.tag == "enemyBullet") && !dmgCooldown)
        {
            
            TakeDmg();
        }
    }
    void TakeDmg()
    {
        health--;
        Invoke("ResetCooldown", 1);
    }
    void ResetCooldown()
    {
        dmgCooldown = false;
    }
}
