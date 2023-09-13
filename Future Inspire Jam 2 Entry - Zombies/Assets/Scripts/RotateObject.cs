using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(speed * Time.deltaTime * transform.forward);
    }
}
