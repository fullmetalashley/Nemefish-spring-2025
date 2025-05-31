using System;
using UnityEngine;
using Yarn.Compiler;
using FMOD.Studio;
using UnityEngine.EventSystems;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    #region Enums
    private enum Directions
    {
        UP,
        DOWN,
        LEFT,
        RIGHT,
        STAND_UP,
        STAND_DOWN,
        STAND_LEFT,
        STAND_RIGHT,
        IDLE
    }
    #endregion

    #region Editor Data
    [Header("Dependencies")]
    [SerializeField] Animator animator;
    [SerializeField] SpriteRenderer spriteRenderer;
    public static PlayerController instance;

    [Header("Attributes")]
    public float speed;
    public float groundDist;
    public bool canMove;
    public bool canFish;

    public LayerMask terrainLayer;

    public Rigidbody rigidBody;

    // UI Access
    [Header("UI Access")] public bool withinInteractionSpace;
    public GameObject interactionIcon;

    public GameObject leftSpawnPoint;
    public GameObject rightSpawnPoint;
    #endregion

    #region Internal Data
    private Directions facingDirection = Directions.RIGHT;

    private Gun _gun;
    private PlayerRaycasting raycast;
    #endregion


    #region Audio Vaariables
    private EventInstance playerFootsteps;
    #endregion



    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        _gun = this.gameObject.GetComponent<Gun>();
        raycast = this.gameObject.GetComponent<PlayerRaycasting>();

        if (AudioManager.instance != null)
        {
            playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstepsDefault);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!canMove) return;
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

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveDir = new Vector3(y * -1, 0, x);

        if (x * x + y * y > 1)
            moveDir.Normalize();

        x = moveDir.x;
        y = moveDir.z;
        rigidBody.linearVelocity = moveDir * speed;
        float mag = moveDir.sqrMagnitude;

        UpdateSound();
        CalculateFacingDirection(x, y);
        UpdateAnimation();
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

    public void SetInteractionIcon(bool state)
    {
        interactionIcon.SetActive(state);
    }

    #region Utility
    public BasicAnimator GetAnimator() => GetComponent<BasicAnimator>();
    #endregion

    #region Animation Logic
    private void CalculateFacingDirection(float x, float y)
    {
        // Check if player is holding still. If so, use a standing animation. Else, use a walking animation
        if (x == 0 && y == 0)
            facingDirection = Directions.IDLE;

        // Check if left-right movement is more significant than up-down movement
        else if (Math.Abs(x) >= Math.Abs(y))
        {
            // East, else West
            if (x > 0)
            {
                facingDirection = Directions.RIGHT;
                spriteRenderer.flipX = false;
            }
            else if (x < 0)
            {
                facingDirection = Directions.LEFT;
                spriteRenderer.flipX = true;
            }
        }
            else
            {
                // North, else South
                if (y > 0)
                    facingDirection = Directions.UP;
                else if (y < 0)
                    facingDirection = Directions.DOWN;
            }

        // Debug.Log(facingDirection);

    }

    private void UpdateAnimation()
    {
        if (facingDirection == Directions.IDLE)
        {
                GetAnimator().SetIdle(true);
                GetAnimator().SetWalkSide(false);
                GetAnimator().SetWalkUp(false);
                GetAnimator().SetWalkDown(false);
        }
        else
        {
            if (facingDirection == Directions.LEFT || facingDirection == Directions.RIGHT)
            {
                GetAnimator().SetIdle(false);
                GetAnimator().SetWalkSide(true);
                GetAnimator().SetWalkUp(false);
                GetAnimator().SetWalkDown(false);
            }
            else if (facingDirection == Directions.UP)
            {
                GetAnimator().SetIdle(false);
                GetAnimator().SetWalkSide(false);
                GetAnimator().SetWalkUp(true);
                GetAnimator().SetWalkDown(false);
            }
            else if (facingDirection == Directions.DOWN)
            {
                GetAnimator().SetIdle(false);
                GetAnimator().SetWalkSide(false);
                GetAnimator().SetWalkUp(false);
                GetAnimator().SetWalkDown(true);
            }
        }
        
    }

    private void UpdateAnimation2(float x, float y)
    {
        // Check if player is holding still. If so, use a standing animation. Else, use a walking animation
        if (x == 0 && y == 0)
        {
            // If player is currently in a walking animation, switch to the standing animation of the same direction
            if (facingDirection == Directions.UP)
                facingDirection = Directions.STAND_UP;
            if (facingDirection == Directions.LEFT)
                facingDirection = Directions.STAND_LEFT;
            if (facingDirection == Directions.RIGHT)
                facingDirection = Directions.STAND_RIGHT;
            if (facingDirection == Directions.DOWN)
                facingDirection = Directions.STAND_DOWN;
        }
        // Check if left-right movement is more significant than up-down movement
        else if (Math.Abs(x) >= Math.Abs(y))
        {
            // East, else West
            if (x > 0)
                facingDirection = Directions.RIGHT;
            else if (x < 0)
                facingDirection = Directions.LEFT;
        }
        else
        {
            // North, else South
            if (y > 0)
                facingDirection = Directions.UP;
            else if (y < 0)
                facingDirection = Directions.DOWN;
        }

        // facingDirection is calculated, code to set animator based on direction goes here
    }

    #endregion

}
