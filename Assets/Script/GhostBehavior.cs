using UnityEngine;


[RequireComponent(typeof(Ghost))]
public abstract class GhostBehavior : MonoBehaviour 
{
    //this scripts is made to enable and disabling functionalities and turning on and off other scripts

    public Ghost ghost { get; private set; }
    public float duration;

    private void Awake()
    {
        this.ghost = GetComponent<Ghost>();
        this.enabled = false; //initial state everything is off 
    }

    public void Enable()
    {
        Enable(this.duration);
    }

    public virtual void Enable(float duration)
    {
        this.enabled = true;

        CancelInvoke();
        Invoke(nameof(Disable), duration);
    }

    public virtual void Disable()
    {
        this.enabled = false;

        CancelInvoke();
    }

}
