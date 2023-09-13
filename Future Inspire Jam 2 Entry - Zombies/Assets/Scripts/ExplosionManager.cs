using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionManager : MonoBehaviour
{
    public static int increaseRadius;
    [SerializeField] ZombieSpawner zombieSpawner;
    [SerializeField] PlayerController playerController;
    bool canDamage;
    bool flag;

    private void OnEnable()
    {
        StartCoroutine(DamageZone());
        if (increaseRadius > 0)
        {
            flag = true;
            increaseRadius--;
            transform.localScale = Vector3.one * 14;
        }
        else if(flag)
        {
            flag = false;
            playerController.buffText.text = string.Empty;
            transform.localScale = Vector3.one * 7;
        }
    }
    IEnumerator DamageZone()
    {
        canDamage = true;
        yield return new WaitForSeconds(0.1f);
        canDamage = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (canDamage)
        {
            if (collision.CompareTag("Zombie"))
            {
                zombieSpawner.DeleteZombie(collision.gameObject);
            }
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(collision.GetComponent<PlayerController>().TakeDamage(collision));
            }
        }
    }
}
