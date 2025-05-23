using System.Runtime.CompilerServices;
using Radishmouse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("UI Elements")]
    public GameObject fishingPanel;
    public bool inUI;
    public Slider chargeBar;
    public Image sliderFill;
    public TextMeshProUGUI playerHPText;

    public float moveSpeed;
    public float horizontalMovement = 0f; // Allow for movement in horizontal direction
    public GameObject moveableBackground;

    public float leftX;
    public float rightX;

    [Header("Animators")]
    public Animator rodAnimator;
    
    //TODO: Figure this stupid thing out
    private UILineRenderer _uiLineRenderer;

    //TODO: Get this out of here and into the player controller
    public int playerHealth = 10;

    //Script refs
    private FishSpawner _fishSpawner;
    private FishingRod _fishingRod;
    private PlayerController _playerController;

    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _playerController = FindAnyObjectByType<PlayerController>();
        playerHPText.text = "HP: " + playerHealth;
        _uiLineRenderer = FindAnyObjectByType<UILineRenderer>();
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
    }

    // Update is called once per frame
    private void Update()
    {
        //This is if the player character is in the world space
        if (Input.GetKeyDown(KeyCode.E) && _playerController.withinInteractionSpace)
        {
            ToggleFishing();
            _playerController.canFish = false;
        }
        
        /*
        sliderFill.color = chargeBar.value switch
        {
            >= 0 and <= 500 => Color.green,
            > 500 and <= 800 => Color.yellow,
            > 800 and <= 1000 => Color.red,
            _ => sliderFill.color
        };
        */
        // Check for horizontal movement (Left and Right arrow keys)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (moveableBackground.transform.position.x >= leftX)
            {
                return;
            }
            horizontalMovement = 1f;
        }
        
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (moveableBackground.transform.position.x <= rightX)
            {
                return;
            }
            horizontalMovement = -1f;
        }
        else {
            horizontalMovement = 0f;
        }

        
        // Update the UI object's position
        Vector3 newPosition = moveableBackground.transform.position;
        newPosition.x += horizontalMovement * moveSpeed * Time.deltaTime;
        moveableBackground.transform.position = newPosition;
    }

    public void UpdateUIText()
    {
        playerHPText.text = "HP: " + playerHealth;
    }
    
    //Turn the panel on and off, generally based on button clicks.
    public void ToggleFishing()
    {
        fishingPanel.SetActive(!fishingPanel.activeSelf);
        inUI = !inUI;
        _playerController.canFish = !fishingPanel.activeSelf;
        _fishSpawner.UIClose();
    }
}
