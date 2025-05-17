using UnityEngine;
 
public class PointerController : MonoBehaviour
{
    public Transform pointA; // Reference to the starting point
    public Transform pointB; // Reference to the ending point
    public RectTransform safeZone; // Reference to the safe zone RectTransform
    public float moveSpeed = 100f; // Speed of the pointer movement
 
    private RectTransform pointerTransform;
    private Vector3 targetPosition;

    private FishingRod _fishingRod;
    private QuicktimeManager _quicktimeManager;
 
    void Start()
    {
        pointerTransform = GetComponent<RectTransform>();
        targetPosition = pointB.position;
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
    }
 
    void Update()
    {
        // Move the pointer towards the target position
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);
 
        // Change direction if the pointer reaches one of the points
        if (Vector3.Distance(pointerTransform.position, pointA.position) < 0.1f)
        {
            targetPosition = pointB.position;
        }
        else if (Vector3.Distance(pointerTransform.position, pointB.position) < 0.1f)
        {
            targetPosition = pointA.position;
        }
 
        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckSuccess();
        }
    }
 
    void CheckSuccess()
    {
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");
            //We have caught the fish. Turn off the bar and add HP. 
//            _fishingRod.CaughtFishEffect(10);
        }
        else
        {
            Debug.Log("Fail!");
 //           _fishingRod.CaughtFishEffect(-10);
        }
        _quicktimeManager.TriggerQuicktime(false);
    }
}