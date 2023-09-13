using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ZombieSpawner : MonoBehaviour
{
    //For the end scene
    public static int wave;
    public static int kills;
    public static int bestWave;
    public static int bestKills;
    [Header("Wave")]
    [SerializeField] TextMeshProUGUI waveText;
    [SerializeField] TextMeshProUGUI enemyNumberText;
    int enemyNumber;
    int spawnCounter;
    int waveNumber;
    float nextWaveDelay;
    [Header("Spawner")]
    [SerializeField] GameObject zombiePrefab;
    [SerializeField] Transform playerTransform;
    int minX, minY, maxX, maxY;
    Vector2 spawnPosition;
    float spawnDelay;
    [Header("Buff")]
    [SerializeField] GameObject buff;
    [Header("SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip waveSFX;
    [SerializeField] AudioClip deathSFX;
    private void Start()
    {
        //End scene
        wave = 0;
        kills = 0;
        //Spawner
        minX = -10;
        minY = -10;
        maxX = 10;
        maxY = 10;
        //Wave
        nextWaveDelay = 1;
        StartCoroutine(NextWave());
    }
    IEnumerator NextWave()
    {
        audioSource.PlayOneShot(waveSFX);
        yield return new WaitForSeconds(nextWaveDelay);
        waveNumber++;
        waveText.text = "Wave " + waveNumber;
        enemyNumber = Random.Range(3, 7) * waveNumber;
        enemyNumberText.text = "Enemies: " + enemyNumber;
        spawnCounter = enemyNumber;
        StartCoroutine(SpawnZombie());
        buff.transform.position = new Vector3(Random.Range(minX, maxX + 1), Random.Range(minY, maxY + 1));
        buff.SetActive(true);
        wave = waveNumber;
    }
    IEnumerator SpawnZombie()
    {
        spawnDelay = Random.Range(1, 3f);
        yield return new WaitForSeconds(spawnDelay);
        spawnPosition.x = Random.Range(minX, maxX + 1);
        spawnPosition.y = Random.Range(minY, maxY + 1);
        while (Vector2.Distance(spawnPosition, playerTransform.position) < 10)
        {
            spawnPosition.x = Random.Range(minX, maxX + 1);
            spawnPosition.y = Random.Range(minY, maxY + 1);
        }
        if(spawnCounter > 0)
        {
            spawnCounter--;
            Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(SpawnZombie());
        }
    }
    public void DeleteZombie(GameObject zombie)
    {
        kills++;
        audioSource.PlayOneShot(deathSFX);
        zombie.GetComponent<ZombieManager>().moveSpeed = 0;
        zombie.GetComponent<BoxCollider2D>().enabled = false;
        zombie.GetComponent<Animator>().SetBool("IsDead", true);
        Destroy(zombie, 0.5f);
        enemyNumber--;
        enemyNumberText.text = "Enemies: " + enemyNumber;
        if(enemyNumber <= 0)
            StartCoroutine(NextWave());
    }
}
