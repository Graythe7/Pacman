using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostHome : GhostBehavior
{
    public Transform inside;
    public Transform outside;

    private void OnEnable()
    {
        StopAllCoroutines();
    }

    private void OnDisable()
    {
        if (this.gameObject.activeSelf)
        {
            StartCoroutine(ExitTransition());
        }
    }

    // to make ghost bounce up and down inside the home 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Obstacle")) // layer 9 = Obstacle
        {
            this.ghost.movement.SetDirection(-this.ghost.movement.direction);
        }
    }

    private IEnumerator ExitTransition()
    {
        this.ghost.movement.SetDirection(Vector2.up, true);
        this.ghost.movement.rigidbody.isKinematic = true; // kinda turn off the physics on objects and prevents collision when the ghost is exiting
        this.ghost.movement.enabled = false; //temporarly turning off the movement script

        // animating the exit transition
        Vector3 position = this.transform.position;

        float duration = 0.5f;
        float elapsed = 0.5f;

        while(elapsed < duration) //inside transition
        {
            Vector3 newPosition = Vector3.Lerp(position, this.inside.position, elapsed/duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        elapsed = 0.0f; //reseting the elapsed 

        while (elapsed < duration) //outside transition
        {
            Vector3 newPosition = Vector3.Lerp(this.inside.position, this.outside.position, elapsed / duration);
            newPosition.z = position.z;
            this.ghost.transform.position = newPosition;
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Turning the conditions to their initial ones 
        this.ghost.movement.SetDirection(new Vector2(Random.value < 0.5f ? -1.0f: 1.0f, 0.0f), true);
        this.ghost.movement.rigidbody.isKinematic = false;
        this.ghost.movement.enabled = true;
    }
}
