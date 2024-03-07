using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    public int HP;
    public static int enemyDamage;

    public Animator animator;
    WaveSpawn spawner;


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

        // Spawn a health pack
        Vector3 spawnPosition = transform.position + new Vector3(0, 1, 0); // Adjust the spawn position as needed

        // Destroy the GameObject
        if (spawner != null) spawner.currentMonster.Remove(this.gameObject);
        Destroy(gameObject);
    }

    public void SetSpawner(WaveSpawn _spawner)
    {
        spawner = _spawner;
    }
}
