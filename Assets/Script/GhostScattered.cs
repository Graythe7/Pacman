using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostScattered : GhostBehavior
{

    private void OnDisable()
    {
        this.ghost.chase.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if(node != null && this.enabled && !this.ghost.frighten.enabled)
        {
            int index = Random.Range(0, node.availableDirection.Count);

            // just chooses a random direction each time 
            if (node.availableDirection[index] == -this.ghost.movement.direction && node.availableDirection.Count > 1)
            {
                index++;

                if(index >= node.availableDirection.Count)
                {
                    index = 0;
                }
            }

            this.ghost.movement.SetDirection(node.availableDirection[index]);

        }
    }
}
