using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Weapon : MonoBehaviour
{
    //GunStats
    public int dmg;
    public float timeBetweenShooting, range, timeBetweenShots, spread;
    public int magaZineSize, bulletPetTap;
    public bool allowButtonHold;
    public int currentBullet, bulletsShot;
    Enemy enemy;
    EnemyShoot enemyShoot;

    //bools
    bool shooting, readyToShoot, reload;

    //Reference
    public Vector3 startPosition;
    public Camera fpsCam;
    public RaycastHit rayHit;
    public LayerMask IsEnemy;

    //Graphics
    public TextMeshProUGUI text;
    public GameObject muzzleEffect;
    private Animator animator;

    //Audio
    AudioSource sound;
    public AudioClip shootSound;

    private void Start()
    {
        currentBullet = magaZineSize;
        startPosition = transform.position;
        readyToShoot = true;
        
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();
    }

    private void Update()
    {
        GetInput();
        //SetText
        text.SetText(currentBullet.ToString());
    }

    private void GetInput()
    {
        if (Time.timeScale != 0) // ถ้าเกมไม่ได้ถูกพัก
        {
            if(allowButtonHold)shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);

            //shoot
            if(readyToShoot && shooting && currentBullet > 0)
            {
                bulletsShot = bulletPetTap;
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        muzzleEffect.GetComponent<ParticleSystem>().Play();
        animator.SetTrigger("Shoot");

        sound.PlayOneShot(shootSound);

        readyToShoot = false;

        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 direction = fpsCam.transform.forward + new Vector3(x, y, 0);

        //RayCast
        if (Physics.Raycast(fpsCam.transform.position, direction, out rayHit, range, IsEnemy))
        {
            Debug.Log(rayHit.collider.name);

            if (rayHit.collider.CompareTag("Enemy"))
            {
                Enemy enemy = rayHit.collider.GetComponent<Enemy>();
                EnemyShoot enemyShoot = rayHit.collider.GetComponent<EnemyShoot>();
                if (enemy != null)
                    enemy.TakeDamage(dmg);
                else if (enemyShoot != null)
                    enemyShoot.TakeDamage(dmg);
            } 
        }
        currentBullet--;
        bulletsShot--;

        Invoke("ResetShot", timeBetweenShooting);

        if(bulletsShot > 0 && currentBullet > 0)
        Invoke("Shoot", timeBetweenShots);
    }

    private void ResetShot()
    {
        readyToShoot = true;
    }

    public void Reload(int amount)
    {
        currentBullet += amount;
    }

}
