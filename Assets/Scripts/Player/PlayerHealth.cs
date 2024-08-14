using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    private CapsuleCollider hitbox;
    [SerializeField] private GameManager gameManager;
    private PlayerController controller;

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
        PlayerPrefs.SetFloat("PlayerMaxhealth", 10);
        health = PlayerPrefs.GetFloat("PlayerMaxhealth");
        PlayerPrefs.SetFloat("PlayerCurhealth", health);
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
            SceneManager.LoadScene(11);
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
        PlayerPrefs.SetFloat("PlayerCurhealth", health);
        Invoke("ResetCooldown", 1);
    }
    void ResetCooldown()
    {
        dmgCooldown = false;
    }
}
