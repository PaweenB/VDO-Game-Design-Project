using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    BoxCollider boxCollider;
    public int enemyDamage;

    [SerializeField] LayerMask playerLayer;
    [SerializeField] float sightRange, attackRange;
    [SerializeField] bool playerInSight, playerInAttackRange;

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.Find("Player");
        animator = GetComponent<Animator>();
        boxCollider = GetComponentInChildren<BoxCollider>();
    }

    void Update()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(playerInSight && !playerInAttackRange)Chase();
        else if(playerInSight && playerInAttackRange)Attack();
    }

    void Chase()
    {
        if (GetComponent<Enemy>().HP > 0)
        {
            agent.SetDestination(player.transform.position);
        }
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        agent.SetDestination(transform.position);
    }

    void EnableAttack()
    {
        boxCollider.enabled = true;
    }

    void DisableAttack()
    {
        boxCollider.enabled = false;
    }

   private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerUI playerUI = other.GetComponent<PlayerUI>();
            if (playerUI != null)
            {
                playerUI.TakeDamage(enemyDamage); // ทำให้ Player รับความเสียหาย
            }
        }
    }
}
