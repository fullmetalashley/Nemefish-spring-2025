using System.Collections.Generic;
using UnityEngine;

public class SlingshotCast : MonoBehaviour
{
    [Header("Rod Metrics")]
    [SerializeField] private float maxCharge;
    private int directional = 1;
    
    [Header("Targeting Elements")]
    public GameObject castTarget;   //This is the crosshair position
    public GameObject cursor;   //This is the crosshair object

    [Header("Fish spots")]
    public List<BoundsDetection> fishingSpots;

    //Script refs
    private UIManager _uiManager;
    private QuicktimeManager _quicktimeManager;
    private FishSpawner _fishSpawner;

    public GameObject pullStart;
    private Vector2 pullCurrent;
    private bool isCharging;
    private bool isCasting;

    public GameObject bobber;

    public float castPower;
    public float maxPullDistance;

    public float castDestination;

    public bool rodActive;

    public Animator rodAnimator;

    void Start()
    {
        bobber.transform.position = pullStart.transform.position;
        castTarget.transform.position = pullStart.transform.position;

        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
        _uiManager = FindAnyObjectByType<UIManager>();
    }
    
    public void Update()
    {
        if (!rodActive) return;
        //At the start of RMB, we begin the charge
        if (Input.GetMouseButtonDown(1))
        {
            bobber.transform.position = pullStart.transform.position;
            castTarget.transform.position = pullStart.transform.position;
            isCharging = true;
            
            castTarget.SetActive(true);
            bobber.SetActive(false);

            rodAnimator.SetBool("isReeling", true);
        }
        //While holding the RMB down, we're charging
        if (Input.GetMouseButton(1) && isCharging)
        {
            pullCurrent = Input.mousePosition;
        }
        //Release! Apply the cast
        if (Input.GetMouseButtonUp(1) && isCharging)
        {
            Vector2 pullVector = new Vector2(pullStart.transform.position.x, pullStart.transform.position.y) - pullCurrent;
            Cast(pullVector);
            isCasting = true;
            isCharging = false;
            rodAnimator.SetBool("isReeling", false);
        }

        if (isCharging)
        {
            //Same as what we'll ultimately do to the bobber, but show it updating in real time for the cast prediction
            Vector2 pullVector = new Vector2(pullStart.transform.position.x, pullStart.transform.position.y) - pullCurrent;
            Vector2 launchDirection = pullVector.normalized;
        
            float power = Mathf.Clamp(pullVector.magnitude, 0, maxPullDistance);
     
            Vector2 newVelocity = launchDirection * power * castPower;
            newVelocity.y += castTarget.transform.position.y * Time.deltaTime;
            castTarget.transform.position = new Vector3(castTarget.transform.position.x, newVelocity.y,
                castTarget.transform.position.z);
        }

        if (!isCasting) return;
        castTarget.SetActive(false);
        bobber.SetActive(true);
        if (bobber.transform.position.y <= castDestination)
        {
            // Update the UI object's position
            Vector3 newPosition = bobber.transform.position;
            newPosition.y += newPosition.y * Time.deltaTime;
            bobber.transform.position = newPosition;   
        }
        else
        {
            //We've landed, so see if we hit something
            CalculateDistance();
            isCasting = false;
        }
    }


    //Calculate the power we need to have this work 
    public void Cast(Vector2 pullVector)
    {
        Vector2 launchDirection = pullVector.normalized;
        
        float power = Mathf.Clamp(pullVector.magnitude, 0, maxPullDistance);
     
        Vector2 newVelocity = launchDirection * power * castPower;
        castDestination = newVelocity.y;
    }
    
    
    //See how far between the cast point and the bobber, and check if we've landed on something
    public void CalculateDistance()
    {
        if (_fishSpawner.CalculateDistance(bobber))
        {
            Debug.Log("In bounds");
            //This means that we are within bounds of the fish. 
            _quicktimeManager.currentBounds = _fishSpawner._currentBounds;
            _quicktimeManager.TriggerQuicktime(true);
        }
        else
        {
            _fishSpawner.FishScatter(); //Fish need to scatter if it's a miss
        }
    }

    public void CanCastAgain()
    {
        
    }
}
