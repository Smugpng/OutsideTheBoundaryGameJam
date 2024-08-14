using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Health : MonoBehaviour
{
    [Header("Health should be in 1-10 Range")]
    public float maxHealth;
    private float health;
    private bool damgageCooldown;

    [Header("WhileAlive")]
    private GameObject thisCharacter;
    private CapsuleCollider mainCollider;
    private Animator animator;
    private Rigidbody mainRB;
    private NavMeshAgent navMeshAgent;
    private EnemyAI EnemyAI;

    private GameObject hitMe;
    private void Awake()
    {
        health = maxHealth;
        animator = GetComponent<Animator>();
        mainRB = GetComponent<Rigidbody>();
        mainCollider = GetComponent<CapsuleCollider>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        EnemyAI = GetComponent<EnemyAI>();
        thisCharacter = this.gameObject;
        GetRagdollParts();
        RagdollOff();
    }
    #region Health
    private void OnTriggerEnter(Collider other)
    {
        if (!damgageCooldown && other.tag == "bullet")
        {
            damgageCooldown = true;
            TakeDMG();
            Invoke("ResetCooldown", .5f);
            hitMe = other.gameObject;
        }
    }
    private void ResetCooldown()
    {
        damgageCooldown = false;
        Destroy(hitMe);
    }
    void TakeDMG()
    {
        health--;
    }

    private void Update()
    {
        if (health <= 0) 
        {
            Invoke("destroy", 30);
        RagdollOn();
        }
    }
    void destroy()
    {
        Destroy(thisCharacter);
    }
    #endregion
    #region Ragdoll
    [Header("WhileDead")]
    private Collider[] ragdollColldiers;
    private Rigidbody[] ragdollRB;
    void GetRagdollParts()
    {
        ragdollColldiers = thisCharacter.GetComponentsInChildren<Collider>();
        ragdollRB = thisCharacter.GetComponentsInChildren<Rigidbody>();  
    }
    void RagdollOn()
    {
        foreach (Collider coll in ragdollColldiers)
        {
            coll.enabled = true;
        }
        foreach (Rigidbody rb in ragdollRB)
        {
            rb.isKinematic = false;
        }
        animator.enabled = false;
        mainCollider.enabled = false;
        mainRB.isKinematic = true;
        EnemyAI.enabled = false;
        navMeshAgent.enabled = false;

    }
    void RagdollOff()
    {
        foreach (Collider coll in ragdollColldiers)
        {
            coll.enabled = false;
        }
        foreach(Rigidbody rb in ragdollRB)
        {
            rb.isKinematic = true;
        }
        animator.enabled = true;   
        mainCollider.enabled = true;
        mainRB.isKinematic = false;
        navMeshAgent.enabled = true;
        EnemyAI.enabled = true;
    }
    #endregion
}
