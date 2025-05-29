using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public virtual void SetWalkSide(bool val)
    {
        thisAnimator.SetBool("WalkSide", val);
    }

    public virtual void SetIdle(bool val)
    {
        thisAnimator.SetBool("Idle", val);
    }

    public virtual void SetWalkUp(bool val)
    {
        thisAnimator.SetBool("WalkUp", val);
    }

    public virtual void SetWalkDown(bool val)
    {
        thisAnimator.SetBool("WalkDown", val);
    }
    public virtual void TriggerFishRodSwing()
    {
        thisAnimator.SetTrigger("Fishing Rod Swing");
    }

    public virtual void TriggerGunShoot()
    {
        thisAnimator.SetTrigger("Worm Gun Shoot");
    }

    public virtual void TriggerDeath()
    {
        thisAnimator.SetTrigger("Death");
    }

    // Set state for yarnspinner poses
    public void Pose(string action_name)
    {
        thisAnimator.SetTrigger("action_name");
    }
    

}
