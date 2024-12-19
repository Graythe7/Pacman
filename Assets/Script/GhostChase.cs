using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostChase : GhostBehavior
{
    private void OnDisable()
    {
        this.ghost.scatter.Enable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled && !this.ghost.frighten.enabled)
        {
            Vector2 direction = Vector2.zero;
            float minDistance = float.MaxValue;

            //loop through possibles paths till find the min path to chase the target 
            foreach(Vector2 availableDirection in node.availableDirection)
            {

                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance < minDistance)
                {
                    direction = availableDirection;
                    minDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
