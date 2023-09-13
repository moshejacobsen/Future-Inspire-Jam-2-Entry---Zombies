using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieManager : MonoBehaviour
{
    public float moveSpeed;
    public Transform playerPosition;
    float xLength;
    float yLength;
    float rotationDegree;
    private Vector3 currentPosition;

    private void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
    }
    private void Update()
    {
        xLength = playerPosition.position.x - transform.position.x;
        yLength = playerPosition.position.y - transform.position.y;
        rotationDegree = Mathf.Rad2Deg * (Mathf.Atan(Mathf.Abs(xLength) / Mathf.Abs(yLength)));
        if (yLength <= 0)
            rotationDegree = 180 - rotationDegree;
        if (xLength >= 0)
            rotationDegree *= -1;
        transform.rotation = Quaternion.Euler(Vector3.forward * rotationDegree);
        transform.Translate(moveSpeed * Time.deltaTime * Vector2.up);

        
        /*if (Vector2.Distance(Vector2.zero, transform.position) < 20)
            currentPosition = transform.position;
        else transform.position = currentPosition;*/
    }
    public IEnumerator GoBack()
    {
        moveSpeed = -0.2f * moveSpeed;
        yield return new WaitForSeconds(1);
        moveSpeed = -5 * moveSpeed;
    }
}
