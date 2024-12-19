using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ghost : MonoBehaviour
{
    public Movement movement { get; private set; }
    public GhostHome home { get; private set; }
    public GhostScattered scatter { get; private set; }
    public GhostFrighten frighten { get; private set; }
    public GhostChase chase { get; private set; }
    public GhostBehavior initialBehvaior;

    public Transform target;

    public int points = 200;

    private void Awake()
    {
        this.movement = GetComponent<Movement>();
        this.home = GetComponent<GhostHome>();
        this.scatter = GetComponent<GhostScattered>();
        this.frighten = GetComponent<GhostFrighten>();
        this.chase = GetComponent<GhostChase>();
    }

    private void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        this.gameObject.SetActive(true);
        this.movement.resetState();

        this.frighten.Disable();
        this.chase.Disable();
        this.scatter.Enable();

        if(this.home != this.initialBehvaior)
        {
            this.home.Disable();
        }

        if(this.initialBehvaior != null)
        {
            this.initialBehvaior.Enable();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //only two scenarios happen when pacman and ghost collides 
        if(collision.gameObject.layer == 6) // layer6 = pacman
        {
            if (this.frighten.enabled) //ghost only can be eaten in frighten mood 
            {
                FindObjectOfType<GameManager>().GhostEaten(this);
            }
            else
            {
                FindObjectOfType<GameManager>().PacmanEaten();
            }
        }
    }
}
