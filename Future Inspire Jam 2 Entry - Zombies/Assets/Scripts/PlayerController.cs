using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    float horizontalDirection;
    float verticalDirection;
    Vector2 mousePosition;
    Vector2 currentPosition;
    float xLength;
    float yLength;
    float rotationDegree;
    [Header("Health")]
    [SerializeField] HealthManager healthManager;
    [Header("Weapon")]
    [SerializeField] Transform weapon;
    [Header("Buffs")]
    public TextMeshProUGUI buffText;
    public bool sliceBuff;
    public static bool magnetic;
    [Header("SFX")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip buffSFX;
    private void Start()
    {
        buffText.text = string.Empty;
    }
    private void Update()
    {
        //Look twards mouse
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        xLength = mousePosition.x - transform.position.x;
        yLength = mousePosition.y - transform.position.y;
        rotationDegree = Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(xLength) / Mathf.Abs(yLength)));
        if (yLength <= 0)
            rotationDegree = 180 - rotationDegree;
        if (xLength >= 0)
            rotationDegree *= -1;
        //Arrow movement
        horizontalDirection = Input.GetAxis("Horizontal");
        verticalDirection = Input.GetAxis("Vertical");
        
        //Throw weapon
        if (Input.GetMouseButtonDown(0) && weapon.IsChildOf(transform) && PauseManager.canThrow)
        {
            weapon.GetComponent<ThrowableManager>().Throw();
            magnetic = true;
        }
        //Magnetic buff
        else if (Input.GetMouseButtonDown(1) && !weapon.IsChildOf(transform) && magnetic)
        {
            weapon.GetComponent<ThrowableManager>().ReturnToPlayer();
            magnetic = false;
        }
    }
    private void FixedUpdate()
    {

        transform.rotation = Quaternion.Euler(Vector3.forward * rotationDegree);

        //Use transform.forwords
        //transform.Translate(moveSpeed * Time.deltaTime * new Vector2(horizontalDirection, verticalDirection));
        transform.position += moveSpeed * Time.deltaTime * new Vector3(horizontalDirection, verticalDirection, 0);

        //Check border
        CheckBorder();
    }
    public void CheckBorder()
    {
        /*if(Vector2.Distance(Vector2.zero, transform.position) < 20)
            currentPosition = transform.position;
        else transform.position = currentPosition;*/
        if (transform.position.y >= 10)
            transform.position = new Vector3(transform.position.x, 9.8f, transform.position.z);
        else if (transform.position.y <= -10)
            transform.position = new Vector3(transform.position.x, -9.8f, transform.position.z);
        else if (transform.position.x >= 10)
            transform.position = new Vector3(9.8f, transform.position.y, transform.position.z);
        else if (transform.position.x <= -10)
            transform.position = new Vector3(-9.8f, transform.position.y, transform.position.z);
    }
    public IEnumerator TakeDamage(Collider2D collision)
    {
        healthManager.RemoveLife(1, collision);
        yield return new WaitForSeconds(.5f);
        healthManager.canBeDamaged = true;
    }
    IEnumerator ActivateBuff(Buffs buff)
    {
        print(buff.ToString());
        buffText.text = buff.ToString();
        switch (buff)
        {
            case Buffs.SPEED:
                moveSpeed = 15;
                break;
            /*case Buffs.RANGE:
                ThrowableManager.maxThrowingDistance = 10;
                break;*/
            case Buffs.HEALTH:
                healthManager.AddLife();
                break;
            case Buffs.EOA:
                ExplosionManager.increaseRadius = 3;
                break;
            case Buffs.SLICE:
                sliceBuff = true;
                break;
            /*case Buffs.DAMAGE: 
                
                break;*/
            /*case Buffs.MAGNETIC:
                magneticBuff = true;
                break;*/
        }
        yield return new WaitForSeconds(7);
        moveSpeed = 10;
        //ThrowableManager.maxThrowingDistance = 5;
        sliceBuff = false;
        if(buff != Buffs.EOA)
            buffText.text = string.Empty;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Buff"))
        {
            StartCoroutine(ActivateBuff(collision.GetComponent<BuffManager>().buffState));
            collision.gameObject.SetActive(false);
            audioSource.PlayOneShot(buffSFX);
        }
        if (collision.CompareTag("Zombie"))
        {
            healthManager.zombieAttack = true;
            StartCoroutine(TakeDamage(collision));
        }
        if (collision.CompareTag("Throwable") && weapon.GetComponent<ThrowableManager>().returnOn)
        {
            weapon.GetComponent<ThrowableManager>().SnapToPlayer();
        }
    }
}
