using UnityEngine;

public class BasicAnimator : MonoBehaviour
{
    [SerializeField] protected Animator thisAnimator;
    protected Vector3 oldPos = Vector3.zero;
    protected Vector3 deltaPos = Vector3.zero;

    public virtual void SetWalking(bool val)
    {
        thisAnimator.SetBool("Walking", val);
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

    protected virtual void DeltaMovement()
    {
        deltaPos = transform.position - oldPos;

        if (deltaPos.sqrMagnitude > .001f * Time.deltaTime)
            SetWalking(true);
        else
            SetWalking(false);

        oldPos = transform.position;
    }

}
