using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostFrighten : GhostBehavior
{
    public SpriteRenderer body;
    public SpriteRenderer eyes;
    public SpriteRenderer white;
    public SpriteRenderer blue;

    public bool eaten { get; private set; }

    public override void Enable(float duration)
    {
        base.Enable(duration);

        //the normal body get deactivated
        this.body.enabled = false;
        this.eyes.enabled = false;
        this.blue.enabled = true;
        this.white.enabled = false;

        //when half of the duration is left the ghost starts changing color 
        Invoke(nameof(Flash), duration/2.0f);
    }

    // do the reverse of Enable
    public override void Disable()
    {
        base.Disable();

        this.body.enabled = true;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void Flash()
    {
        if (!this.eaten)
        {
            this.blue.enabled = false;
            this.white.enabled = true;
            this.white.GetComponent<AnimatedSprite>().Restart();
        }
    }

    private void Eaten()
    {
        this.eaten = true;

        Vector3 position = this.ghost.home.inside.position;
        position.z = this.ghost.transform.position.z;
        this.ghost.transform.position = position;

        this.ghost.home.Enable(this.duration); //make sure do not leave the home till frighten mood is over

        this.body.enabled = false;
        this.eyes.enabled = true;
        this.blue.enabled = false;
        this.white.enabled = false;
    }

    private void OnEnable()
    {
        this.ghost.movement.speedMultiplier = 0.5f;
    }

    private void OnDisable()
    {
        this.ghost.movement.speedMultiplier = 1.0f;
        this.eaten = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) // layer6 = pacman
        {
            if (this.enabled) 
            {
                Eaten();
            }
        }
    }

    //exact same code as chase but kinda the reverse 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Node node = other.GetComponent<Node>();

        if (node != null && this.enabled)
        {
            Vector2 direction = Vector2.zero;
            float maxDistance = float.MinValue;

            //loop through possibles paths till find the min path to chase the target 
            foreach (Vector2 availableDirection in node.availableDirection)
            {

                Vector3 newPosition = transform.position + new Vector3(availableDirection.x, availableDirection.y);
                float distance = (this.ghost.target.position - newPosition).sqrMagnitude;

                if (distance > maxDistance)
                {
                    direction = availableDirection;
                    maxDistance = distance;
                }
            }

            this.ghost.movement.SetDirection(direction);
        }
    }
}
