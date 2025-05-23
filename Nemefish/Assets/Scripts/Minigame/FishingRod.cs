using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using Cursor = UnityEngine.Cursor;

public class FishingRod : MonoBehaviour
{
    [Header("Rod Metrics")]
    public float charge;
    public float baseRodDistance;
    public float chargeMod;
    public float marginOfError;
    [SerializeField] private float maxCharge;
    private int directional = 1;
    
    [Header("Targeting Elements")]
    public GameObject bobber;
    public GameObject castTarget;   //This is the crosshair position
    public GameObject cursor;   //This is the crosshair object
    public GameObject lineStart;
    public GameObject shotTarget;
    public GameObject fishingRodObject;

    [Header("Fish spots")]
    public List<BoundsDetection> fishingSpots;

    //Script refs
    private UIManager _uiManager;
    private QuicktimeManager _quicktimeManager;
    private FishSpawner _fishSpawner;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiManager = FindAnyObjectByType<UIManager>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_uiManager.inUI) return;

        if (Input.GetMouseButtonDown(0))
        {
            shotTarget.transform.position = Input.mousePosition;
            CalculateGunshotDistance();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            cursor.GetComponent<CrosshairFollow>().enabled = false;
            castTarget.transform.position = Input.mousePosition;    //Position locks at RMB down
            fishingRodObject.SetActive(false);
        }
        
        if (Input.GetMouseButton(1))    //On a continued hold, the charge value increases
        {
            //bobber.SetActive(false);
            if (charge >= maxCharge) directional = -1;
            if (charge <= 0) directional = 1;
            
            charge+= (directional * chargeMod * Time.deltaTime);
            _uiManager.chargeBar.value = charge;
            castTarget.transform.position = Input.mousePosition;
            VisualizeCastPoint();
        }

        if (Input.GetMouseButtonUp(1))  //On RMB up, the rod is cast
        {
            //Cast!
            bobber.SetActive(true);
            CalculateDistance();
        }
    }

    public void VisualizeCastPoint()
    {
        //Distances
        float baseDistance = Vector2.Distance(lineStart.transform.position, castTarget.transform.position);
        float distanceTraveled = baseRodDistance * (charge / maxCharge);
        float difference = baseDistance - distanceTraveled;
        
        //Setting up the vectors
        Vector3 direction = (castTarget.transform.position - lineStart.transform.position).normalized;
        Vector3 displacement = direction * distanceTraveled;
        Vector3 targetPoint = lineStart.transform.position + displacement;
        
        bobber.transform.position = targetPoint;
    }

    public void CalculateGunshotDistance()
    {
        //Check the fishing spots to see if the bobber landed in one of them. 
        foreach (BoundsDetection fishSpot in fishingSpots)
        {
            if (fishSpot.fish._fishType == "Mutant")
            {
                if (fishSpot.PointWithinCorners(shotTarget.transform.position))
                {
                    //_fishSpawner.RemoveMutant(fishSpot.gameObject);
                    return;
                }
            }
        }
    }
    
    //See how far between the cast point and the bobber, and check if we've landed on something
    public void CalculateDistance()
    {
        //Distances
        float baseDistance = Vector2.Distance(lineStart.transform.position, castTarget.transform.position);
        float distanceTraveled = baseRodDistance * (charge / maxCharge);
        float difference = baseDistance - distanceTraveled;
        
        //Setting up the vectors
        Vector3 direction = (castTarget.transform.position - lineStart.transform.position).normalized;
        Vector3 displacement = direction * distanceTraveled;
        Vector3 targetPoint = lineStart.transform.position + displacement;
        
        bobber.transform.position = targetPoint;

        //If the bobber is within the margin of error for the cast, we can set the position to the cast target. 
        if ((difference < marginOfError) && (difference > (marginOfError * -1)))
        {
            bobber.transform.position = castTarget.transform.position; 
        }
        
        //Check the fishing spots to see if the bobber landed in one of them. 
        foreach (BoundsDetection fishSpot in fishingSpots)
        {
            if (fishSpot.PointWithinCorners(bobber.transform.position))
            {
                //This means that we are within bounds of the fish. 
                _quicktimeManager.currentBounds = fishSpot;
                _quicktimeManager.TriggerQuicktime(true);
            }
        }
        CanCastAgain();
    }

    //Set things to cast again. 
    public void CanCastAgain()
    {
        /*//Reset UI elements
        cursor.GetComponent<CrosshairFollow>().enabled = true;
        fishingRodObject.SetActive(true);
        bobber.SetActive(true);
        castTarget.SetActive(true);
        
        //Reset the charge value
        charge = 0f;
        _uiManager.chargeBar.value = charge;
        */
    }
}
