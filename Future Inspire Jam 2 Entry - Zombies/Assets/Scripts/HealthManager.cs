using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{
    [Header("Health")]
    public static List<RawImage> lives = new();
    public bool canBeDamaged;
    public bool zombieAttack;
    [SerializeField] RawImage lifeImage;
    [SerializeField] Transform canvasTransform;
    Vector2 basePosition;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip[] damageSFX = new AudioClip[] { };
    [SerializeField] AudioClip gameOverSFX;
    private void Awake()
    {
        canBeDamaged = true;
        zombieAttack = false;
        lives.Clear();
        for (int i = 0; i < 3; i++)
        {
            lives.Add(Instantiate(lifeImage));
            lives[i].transform.SetParent(canvasTransform);
            basePosition = new Vector2(-350 + 50 * i, 200);
            lives[i].transform.localPosition = basePosition;
        }
    }
    public void AddLife()
    {
        lives.Add(Instantiate(lifeImage));
        lives[^1].transform.SetParent(canvasTransform);
        basePosition = new Vector2(-350 + 50 * (lives.Count - 1), 200);
        lives[^1].transform.localPosition = basePosition;
    }
    public void RemoveLife(int damage, Collider2D zombie)
    {
        print("request to remove life");
        print(canBeDamaged);
        if (canBeDamaged)
        {
            canBeDamaged = false;
            print("request granted");
            for (int i = 0; i < damage; i++)
            {
                Destroy(lives[^1], 0.1f);
                lives.Remove(lives[^1]);
                audioSource.PlayOneShot(damageSFX[Random.Range(0, damageSFX.Length)]);
                if (lives.Count <= 0)
                {
                    StartCoroutine(GameOver());
                }
            }
        }
        if (zombieAttack)
        {
            zombieAttack = false;
            StartCoroutine(zombie.GetComponent<ZombieManager>().GoBack());
        }
    }
    IEnumerator GameOver()
    {
        Time.timeScale = 0;
        audioSource.PlayOneShot(gameOverSFX);
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        SceneManager.LoadScene("EndScene");
    }
}
