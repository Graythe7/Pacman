using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public LayerMask obstacleLayer;
    public List<Vector2> availableDirection { get; private set; }

    private void Start()
    {
        this.availableDirection = new List<Vector2>();

        CheckAvailableDirection(Vector2.up);
        CheckAvailableDirection(Vector2.right);
        CheckAvailableDirection(Vector2.down);
        CheckAvailableDirection(Vector2.left);
    }

    private void CheckAvailableDirection(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.5f, 0.0f, direction, 1.0f, this.obstacleLayer);

        if(hit.collider == null) 
        {
            this.availableDirection.Add(direction);
        }
    }
}
