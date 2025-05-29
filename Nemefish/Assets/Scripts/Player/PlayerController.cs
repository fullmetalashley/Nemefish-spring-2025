using System;
using UnityEngine;
using Yarn.Compiler;
using FMOD.Studio;
using UnityEngine.EventSystems;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    #region Enums
    private enum Directions { UP, DOWN, LEFT, RIGHT }
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
        // playerFootsteps = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerFootstepsDefault);


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
        Vector3 moveDir = new Vector3(x, 0, y);

        if (x * x + y * y > 1)
            moveDir.Normalize();

        x = moveDir.x;
        y = moveDir.z;
        rigidBody.linearVelocity = moveDir * speed;

        // CalulateFacingDirection();
        UpdateSound();
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

    /*/private void CalulateFacingDirection()
    {
        if (moveDir.x != 0)
        {
            // for Moving Right
            if (moveDir.x > 0)
            {
                facingDirection = Directions.RIGHT;
            }
            // for Moving Left
            else if (moveDir.x < 0)
            {
                facingDirection = Directions.LEFT;
            }
        }
    }/*/
}
