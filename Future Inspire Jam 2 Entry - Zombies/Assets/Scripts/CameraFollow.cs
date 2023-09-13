using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    void Update()
    {
        transform.rotation = Quaternion.identity;
        //transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}
