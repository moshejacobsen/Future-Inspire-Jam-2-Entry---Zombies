using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using Unity.VisualScripting;

public class ThrowableManager : MonoBehaviour
{
    public static int maxThrowingDistance;
    [SerializeField] ZombieSpawner zombieSpawner;
    [SerializeField] GameObject explosion;
    public float moveSpeed;
    public Transform playerTransform;
    public Transform weaponPosition;
    Vector2 startPosition;
    float xLength;
    float yLength;
    float rotationDegree;
    bool isThrown;
    public bool returnOn;

    bool attachToPlayer;

    [SerializeField] TextMeshProUGUI explosionTimerText;
    int timer;
    bool timerIsRunning;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip explosionSFX;
    private void Start()
    {
        attachToPlayer = false;
        maxThrowingDistance = 5;
        timer = 5;
        explosionTimerText.text = timer.ToString();
        StartCoroutine(ExplosionTimer());
    }
    private void Update()
    {
        if (isThrown)
        {
            transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);
            if(maxThrowingDistance <= Vector2.Distance(startPosition, transform.position))
            {
                isThrown = false;
            }
        }
        else if (returnOn)
        {
            xLength = playerTransform.position.x - transform.position.x;
            yLength = playerTransform.position.y - transform.position.y;
            rotationDegree = Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(xLength) / Mathf.Abs(yLength)));
            if (yLength <= 0)
                rotationDegree = 180 - rotationDegree;
            if (xLength >= 0)
                rotationDegree *= -1;
            transform.rotation = Quaternion.Euler(Vector3.forward * rotationDegree);
            transform.Translate(moveSpeed * 2 * Time.deltaTime * Vector2.up);
        }

    }
    IEnumerator ExplosionTimer()
    {
        timerIsRunning = true;
        yield return new WaitForSeconds(1);
        timer--;
        explosionTimerText.text = timer.ToString();
        if (timer > 0)
            StartCoroutine(ExplosionTimer());
        else if (timer <= 0)
        {
            StartCoroutine(Explode());
            timer = 5;
            explosionTimerText.text = timer.ToString();
            timerIsRunning = false;
        }
    }
    public IEnumerator Explode()
    {
        /*isThrown = false;
        returnOn = false;*/
        if (transform.IsChildOf(playerTransform))
        {
            attachToPlayer = true;
            transform.SetParent(null);
        }
        explosion.SetActive(true);
        audioSource.PlayOneShot(explosionSFX);
        yield return new WaitForSeconds(1f);
        explosion.SetActive(false);
        if (attachToPlayer)
        {
            SnapToPlayer();
            attachToPlayer = false;
        }
    }
    public void Throw()
    {
        transform.SetParent(null);
        startPosition = transform.position;
        isThrown = true;
    }
    public void ReturnToPlayer()
    {
        isThrown = false;
        returnOn = true;
    }
    public void SnapToPlayer()
    {
        isThrown = false;
        returnOn = false;
        transform.SetParent(playerTransform);
        transform.SetPositionAndRotation(weaponPosition.position, weaponPosition.rotation);
        if(!timerIsRunning)
            StartCoroutine(ExplosionTimer());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Slice buff
        if (collision.CompareTag("Zombie") && playerTransform.GetComponent<PlayerController>().sliceBuff)
        {
            zombieSpawner.DeleteZombie(collision.gameObject);
        }
        /*//Player picks-up the bomb on collision
        if (collision.CompareTag("Player"))
        {
            SnapToPlayer();
        }*/
    }
}
