using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public float speed = 8.0f;
    public float speedMultiplier = 1;
    public Vector2 initialDirection;
    public LayerMask obstacleLayer; 

    public new Rigidbody2D rigidbody { get; private set; }

    public Vector2 direction { get; private set; }
    public Vector2 nextDirection { get; private set; }

    public Vector3 startingPosition { get; private set; }

    private void Awake()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    private void Start()
    {
        resetState();
    }

    private void Update()
    {
        if (this.nextDirection != Vector2.zero)
        {
            SetDirection(this.nextDirection);
        }
    }

    public void resetState()
    {
        this.speedMultiplier = 1.0f;
        this.direction = this.initialDirection;
        this.nextDirection = Vector2.zero;
        this.transform.position = startingPosition;
        this.rigidbody.isKinematic = false; // specially for Ghost when they can go through walls 
        this.enabled = true;
    }

    private void FixedUpdate()
    {
        Vector2 position = this.rigidbody.position;
        Vector2 translation = this.direction * this.speed * this.speedMultiplier * Time.fixedDeltaTime;
        this.rigidbody.MovePosition(position + translation);
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (forced || !Occupied(direction))
        {
            this.direction = direction;
            this.nextDirection = Vector2.zero;
        }else
        {
            this.nextDirection = direction; // which means when there is an obstacle along the way, pacman can not change direction till it reach a non tile location
        }
    }

    public bool Occupied(Vector2 direction) // this function is important via pacman's unique movement
    {
        RaycastHit2D hit = Physics2D.BoxCast(this.transform.position, Vector2.one * 0.75f, 0.0f,direction, 1.5f, this.obstacleLayer);
        return hit.collider != null;
    }
}
