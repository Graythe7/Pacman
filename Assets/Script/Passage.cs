using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Passage : MonoBehaviour
{

    public Transform connection; // the connection is a seperate place were the pacman is spawned 

    private void OnTriggerEnter2D(Collider2D other)
    {
        Vector3 position = other.transform.position;
        position.x = this.connection.position.x;
        position.y = this.connection.position.y;
        other.transform.position = position;
    }
}
