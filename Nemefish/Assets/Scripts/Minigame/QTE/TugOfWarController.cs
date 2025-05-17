using UnityEngine;

public class TugOfWarController : MonoBehaviour
{
    
    public Transform failZone; // Reference to the starting point on the LEFT, fish side
    public RectTransform safeZone; // Reference to the safe zone RectTransform
    public float moveSpeed = 100f; // Speed of the pointer movement
    public float playerMoveSpeed = 150f;
 
    public RectTransform pointerTransform;
    private Vector3 targetPosition;

    private Vector3 startingPosition;

    //Script refs
    private FishingRod _fishingRod;
    private QuicktimeManager _quicktimeManager;
    private UIManager _uiManager;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        targetPosition = failZone.position;
        startingPosition = pointerTransform.position;
        
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _uiManager = FindAnyObjectByType<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        // Move the pointer towards the target position
 
        // Check for input
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("You pressed it!");
            pointerTransform.position = new Vector3(pointerTransform.position.x + playerMoveSpeed,
                pointerTransform.position.y, pointerTransform.position.z);
            
            //TODO: We need to check and see if, after this, the pointer is out of the bounds of the bar. And if it is, they win. 
        }
        
        
        // Check if the pointer is within the safe zone
        if (RectTransformUtility.RectangleContainsScreenPoint(safeZone, pointerTransform.position, null))
        {
            Debug.Log("Success!");
            //We caught the fish, and need to add it to our inventory.
            _quicktimeManager.WonQuicktime();
            pointerTransform.position = startingPosition;
        }
        
        if (Vector3.Distance(pointerTransform.position, failZone.position) < 0.1f)
        {
            Debug.Log("You lost!");
            _quicktimeManager.LostQuicktime();
            pointerTransform.position = startingPosition;

        }
        
        pointerTransform.position = Vector3.MoveTowards(pointerTransform.position, targetPosition, moveSpeed * Time.deltaTime);

    }
}
