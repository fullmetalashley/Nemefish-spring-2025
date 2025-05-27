using System;
using UnityEngine;
using Yarn.Compiler;
using FMOD.Studio;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    
    public float speed;

    public float groundDist;

    public LayerMask terrainLayer;

    public Rigidbody rigidBody;
    
    //UI Access
    [Header("UI Access")] public bool withinInteractionSpace;
    public GameObject interactionIcon;
    public bool canFish;
    
  
    public GameObject leftSpawnPoint;
    public GameObject rightSpawnPoint;

    private Gun _gun;

    private PlayerRaycasting raycast;


    public bool canMove;
    // Audio
    private EventInstance playerFootsteps;




    private void Awake()
    {
        instance = this;
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
        Vector3 moveDir = new Vector3(x, 0, y);

        if (x * x + y * y > 1)
            moveDir.Normalize();

        x = moveDir.x;
        y = moveDir.z;
        rigidBody.linearVelocity = moveDir * speed;
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
}
