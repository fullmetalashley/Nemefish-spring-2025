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
    
    [Header("UI Elements")]
    public GameObject bobber;
    public GameObject castTarget;   //This is the crosshair position
    public GameObject cursor;   //This is the crosshair object
    public GameObject lineStart;

    
    public BoundsDetection actualFishSpot;

    public GameObject fishingRodObject;

    public List<BoundsDetection> fishingSpots;

    //Script refs
    private UIManager _uiManager;
    private QuicktimeManager _quicktimeManager;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _uiManager = FindAnyObjectByType<UIManager>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!_uiManager.inUI) return;

        if (Input.GetMouseButtonDown(0))
        {
            //CalculateGunshot();
        }
        
        if (Input.GetMouseButtonDown(1))
        {
            cursor.GetComponent<CrosshairFollow>().enabled = false;
            castTarget.transform.position = Input.mousePosition;    //Position locks at RMB down
            fishingRodObject.SetActive(false);
        }
        
        if (Input.GetMouseButton(1))    //On a continued hold, the charge value increases
        {
            bobber.SetActive(false);
            if (charge >= maxCharge) return;
            charge+= (chargeMod * Time.deltaTime);
            _uiManager.chargeBar.value = charge;
        }

        if (Input.GetMouseButtonUp(1))  //On RMB up, the rod is cast
        {
            //Cast!
            bobber.SetActive(true);
            CalculateDistance();
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
            fishSpot.PointWithinCorners(bobber.transform.position);
        }
    }

    //Set things to cast again. 
    public void CanCastAgain()
    {
        //Reset UI elements
        cursor.GetComponent<CrosshairFollow>().enabled = true;
        fishingRodObject.SetActive(true);
        bobber.SetActive(true);
        castTarget.SetActive(true);
        
        //Reset the charge value
        charge = 0f;
        _uiManager.chargeBar.value = charge;
    }

    
    /*
    public void CalculateGunshot()
    {
        CombatController combatController = FindAnyObjectByType<CombatController>();
        foreach (GameObject mutantSpot in combatController.mutants)
        {
            //mutantSpot.GetComponent<BoundsDetection>().CheckShootingPointInBounds(Camera.main.ScreenToWorldPoint(shootTarget.transform.position));
        }
    }

    public void CaughtFishEffect(int hpEffect)
    {
        _uiManager.playerHP += hpEffect;
        _uiManager.UpdateUIText();
                
        //Destroy that fish
        fishingSpots.Remove(actualFishSpot);
        Destroy(actualFishSpot.gameObject);

        actualFishSpot = null;
    }
    */
}
