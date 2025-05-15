using System;
using UnityEngine;
using Yarn.Compiler;
using FMOD.Studio;

public class FohiController : MonoBehaviour
{
    public static FohiController instance;
    
    public float speed;

    public float groundDist;

    public float trackingDistance;

    public LayerMask terrainLayer;

    public Rigidbody rigidBody;

    public SpriteRenderer spriteRendererStandStill;
    public SpriteRenderer spriteRendererStandUp;
    public SpriteRenderer spriteRendererStandDown;
    public SpriteRenderer spriteRendererLeftRight;
    public SpriteRenderer spriteRendererUp;
    public SpriteRenderer spriteRendererDown;

    public GameObject trackedPlayer;

    private Rigidbody trackedRigidBody;

    private PlayerRaycasting raycast;
    private SpriteRenderer activeSpriteRenderer;

    private double trackingDistanceSquared;
    private int animationSleepFrameCounter;

    // Audio
    private EventInstance playerFootsteps;

    private enum AnimationKey
    {
        STAND_NORTH,
        STAND_EAST,
        STAND_SOUTH,
        STAND_WEST,
        WALK_NORTH,
        WALK_EAST,
        WALK_SOUTH,
        WALK_WEST
    }

    AnimationKey currentAnimation;
    AnimationKey lastFrameAnimation;

    private void Awake()
    {
        instance = this;
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        //playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstepsDefault);

        spriteRendererStandStill.enabled = true;
        spriteRendererStandUp.enabled = false;
        spriteRendererStandDown.enabled = false;
        spriteRendererLeftRight.enabled = false;
        spriteRendererUp.enabled = false;
        spriteRendererDown.enabled = false;
        activeSpriteRenderer = spriteRendererStandStill;

        currentAnimation = AnimationKey.STAND_EAST;
        lastFrameAnimation = AnimationKey.STAND_EAST;
        animationSleepFrameCounter = 0;

        trackedRigidBody = trackedPlayer.GetComponent<Rigidbody>();
        trackingDistanceSquared = Math.Pow(trackingDistance, 2);
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position;
        castPos.y += 1;
        //Shoot a line down, that will only detect the terrain layer
        if (Physics.Raycast(castPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        // Calculate distance to player
        float x = 0;
        float y = 0;
        Vector3 targetOffset = trackedRigidBody.position - rigidBody.position;

        if (Math.Pow(targetOffset.x, 2) + Math.Pow(targetOffset.z, 2) > trackingDistanceSquared)
        {
            if (trackingDistanceSquared > 1)
                targetOffset.Normalize();
            x = targetOffset.x;
            y = targetOffset.z;
        }

        Vector3 moveDir = new Vector3(x, 0, y);

        rigidBody.linearVelocity = moveDir * speed;

        // Don't update the animator for 30 frames after the last update, to avoid flickering
        if (animationSleepFrameCounter < 30)
        {
            animationSleepFrameCounter++;
        }
        else
        {
            animationSleepFrameCounter = 0;
            lastFrameAnimation = currentAnimation;

            // Check if FOHI is holding still
            if (x == 0 && y == 0)
            {
                // If FOHI was walking last frame, switch to the matching standing animation
                if (lastFrameAnimation == AnimationKey.WALK_NORTH)
                    currentAnimation = AnimationKey.STAND_NORTH;
                if (lastFrameAnimation == AnimationKey.WALK_EAST)
                    currentAnimation = AnimationKey.STAND_EAST;
                if (lastFrameAnimation == AnimationKey.WALK_SOUTH)
                    currentAnimation = AnimationKey.STAND_SOUTH;
                if (lastFrameAnimation == AnimationKey.WALK_WEST)
                    currentAnimation = AnimationKey.STAND_WEST;
            }
            // Check if Left/Right movement is more significant than Up/Down
            else if (Math.Abs(x) >= Math.Abs(y))
            {
                // East, else West
                if (x > 0)
                    currentAnimation = AnimationKey.WALK_EAST;
                else if (x < 0)
                    currentAnimation = AnimationKey.WALK_WEST;
            }
            else
            {
                // North, else South
                if (y > 0)
                    currentAnimation = AnimationKey.WALK_NORTH;
                else if (y < 0)
                    currentAnimation = AnimationKey.WALK_SOUTH;
            }

            // The FOHI moved differently, so update their animation
            if (currentAnimation != lastFrameAnimation)
            {
                switch (currentAnimation)
                {
                    case AnimationKey.STAND_NORTH:
                        SwitchSpriteRenderer(spriteRendererStandUp);
                        break;
                    case AnimationKey.WALK_NORTH:
                        SwitchSpriteRenderer(spriteRendererUp);
                        break;
                    case AnimationKey.STAND_EAST:
                        SwitchSpriteRenderer(spriteRendererStandStill);
                        activeSpriteRenderer.flipX = true;
                        break;
                    case AnimationKey.WALK_EAST:
                        SwitchSpriteRenderer(spriteRendererLeftRight);
                        activeSpriteRenderer.flipX = true;
                        break;
                    case AnimationKey.STAND_SOUTH:
                        SwitchSpriteRenderer(spriteRendererStandDown);
                        break;
                    case AnimationKey.WALK_SOUTH:
                        SwitchSpriteRenderer(spriteRendererDown);
                        break;
                    case AnimationKey.STAND_WEST:
                        SwitchSpriteRenderer(spriteRendererStandStill);
                        activeSpriteRenderer.flipX = false;
                        break;
                    case AnimationKey.WALK_WEST:
                        SwitchSpriteRenderer(spriteRendererLeftRight);
                        activeSpriteRenderer.flipX = false;
                        break;
                }
            }
        }



        UpdateSound();
    }

    public void FlipSprite(int dir)
    {
        switch (dir)
        {
            case -1:
                spriteRendererLeftRight.flipX = true;
                break;
            case 1:
                spriteRendererLeftRight.flipX = false;
                break;
            default:
                break;
        }
    }

    private void SwitchSpriteRenderer(SpriteRenderer newRenderer)
    {
        activeSpriteRenderer.enabled = false;
        newRenderer.enabled = true;
        activeSpriteRenderer = newRenderer;
    }

    private void UpdateSound()
    {
        // Start footsteps event if the player has a velocity
        if (rigidBody.linearVelocity.x != 0 || rigidBody.linearVelocity.z != 0)
        {
            // Get the playback state
            PLAYBACK_STATE playbackState;
            playerFootsteps.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerFootsteps.start();
            }
        }
        // Otherwise, stop the footstep event
        else
        {
            playerFootsteps.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }

}
