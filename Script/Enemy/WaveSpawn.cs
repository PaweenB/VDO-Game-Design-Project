using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; // นำเข้าหัวเรื่อง TextMeshPro

public class WaveSpawn : MonoBehaviour
{
    [System.Serializable]
    public class WaveContent
    {
        [SerializeField] GameObject[] monsterSpawn;

        public GameObject[] GetMosterSpawnList()
        {
            return monsterSpawn;
        }
    }

    [SerializeField]
    [NonReorderable] WaveContent[] waves;
    int currentWave = 0;
    float spawnRange = 20;
    public List<GameObject> currentMonster;

    public TextMeshProUGUI waveText; // ประกาศตัวแปร TextMeshPro สำหรับแสดง Wave

    void Start()
    {
        SpawnWave();
        UpdateWaveText(); // เรียกใช้ฟังก์ชันเพื่ออัปเดต Text ของ Wave ทันทีที่เริ่มเกม
    }

    void Update()
    {
        if (currentWave >= waves.Length)
        {
            SceneManager.LoadSceneAsync(3);
            return;
        }

        if (currentMonster.Count == 0)
        {
            currentWave++;
            if (currentWave < waves.Length)
            {
                StartCoroutine(ChangeWaveTextForSeconds("Wave: " + (currentWave + 1), 3f)); // เรียกใช้ Coroutine เพื่อเปลี่ยน Wave Text และหายไปหลังจาก 3 วินาที
                SpawnWave();
            }
        }
    }

    IEnumerator ChangeWaveTextForSeconds(string newText, float duration)
    {
        if (waveText != null)
        {
            waveText.text = newText; // เปลี่ยน Wave Text
            yield return new WaitForSeconds(duration); // รอเป็นเวลาที่กำหนด
            waveText.text = ""; // ล้าง Wave Text
        }
    }

    void SpawnWave()
    {
        for (int i = 0; i < waves[currentWave].GetMosterSpawnList().Length; i++)
        {
            GameObject newspawn = Instantiate(waves[currentWave].GetMosterSpawnList()[i], FindSpawnLocate(), Quaternion.identity);
            currentMonster.Add(newspawn);

            Enemy enemies = newspawn.GetComponent<Enemy>();
            EnemyShoot enemiesShoot = newspawn.GetComponent<EnemyShoot>();
            if (enemies != null)
                enemies.SetSpawner(this);
            else if (enemiesShoot != null)
                enemiesShoot.SetSpawner(this);
            
        }
    }

    Vector3 FindSpawnLocate()
    {
        float xLocate = Random.Range(-spawnRange, spawnRange) + transform.position.x;
        float zLocate = Random.Range(-spawnRange, spawnRange) + transform.position.z;
        float yLocate = transform.position.y;

        Vector3 spawnPos = new Vector3(xLocate, yLocate, zLocate);

        if (Physics.Raycast(spawnPos, Vector3.down, 5))
        {
            return spawnPos;
        }
        else
        {
            return FindSpawnLocate();
        }
    }

    void UpdateWaveText()
    {
        if (waveText != null)
        {
            waveText.text = "Wave: " + (currentWave + 1); // อัปเดต Text ของ TextMeshPro เพื่อแสดง Wave ปัจจุบัน
        }
    }
}