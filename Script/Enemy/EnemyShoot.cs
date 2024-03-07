using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    public NavMeshAgent enemy;
    public Transform player;
    public GameObject enemyBullet;
    public Transform spawnPoint;
    public float enemySpeed, bulletTime;
    public int HP;
    public Animator animator;

    [SerializeField] private float timer, sightRange, attackRange;
    [SerializeField] bool playerInSight, playerInAttackRange;
    [SerializeField] LayerMask playerLayer;
    WaveSpawn spawner;

    void Start()
    {
        // หา GameObject ของผู้เล่นด้วย tag
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
            playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
            if (playerInSight && !playerInAttackRange)
                enemy.SetDestination(player.position);
            else if (playerInSight && playerInAttackRange)
                ShootAtPlayer();
        }
    }

    void ShootAtPlayer()
    {
        bulletTime -= Time.deltaTime;

        if (bulletTime > 0) return;

        bulletTime = timer;

        GameObject bulletObj = Instantiate(enemyBullet, spawnPoint.transform.position, spawnPoint.transform.rotation) as GameObject;
        Rigidbody bulletRig = bulletObj.GetComponent<Rigidbody>();

        // เรียกใช้ NavMeshAgent เพื่อเคลื่อนที่ไปหาผู้เล่น
        enemy.SetDestination(player.position);

        // ลบความเร็วในแนวดิ่งของกระสุน
        Vector3 bulletDirection = (player.position - spawnPoint.position).normalized;
        bulletDirection.y = 0; // ลบความเร็วในแนวดิ่ง
        bulletRig.velocity = bulletDirection * enemySpeed;

        Destroy(bulletObj, 5f);
    }

   public void TakeDamage(int dmgAmount)
    {
        HP -= dmgAmount;
        if(HP <= 0)
        {
            StartCoroutine(DestroyAfterAnimation());
            animator.SetTrigger("die");
            GetComponent<Collider>().enabled = false;
        }
        else
        {
            animator.SetTrigger("damage");
        }
    }

    IEnumerator DestroyAfterAnimation()
    {
        
        yield return new WaitForSeconds(1f);
        // Destroy the GameObject
        if(spawner != null) spawner.currentMonster.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void SetSpawner(WaveSpawn _spawner)
    {
        spawner = _spawner;
    }
}