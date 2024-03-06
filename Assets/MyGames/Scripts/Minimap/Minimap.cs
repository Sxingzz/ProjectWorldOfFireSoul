using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform player;


    void LateUpdate()
    {
        //Vector3 newPosition = transform.position;
        //newPosition.y = transform.position.y;
        //transform.position = newPosition;
        //transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

        Vector3 playerPosition = player.position;
        transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z);
        transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);

    }
}

