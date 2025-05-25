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
    public TextMeshProUGUI gunAmmo;

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
    private ScreenDetection _screenDetection;
    private Gun _gun;

    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _playerController = FindAnyObjectByType<PlayerController>();
        playerHPText.text = "HP: " + playerHealth;
        _uiLineRenderer = FindAnyObjectByType<UILineRenderer>();
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
        _screenDetection = FindAnyObjectByType<ScreenDetection>();
        _gun = FindAnyObjectByType<Gun>();
        
        _screenDetection.SetCorners(moveableBackground);
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

        // Check for horizontal movement (Left and Right arrow keys)
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (!_screenDetection.CanMove(moveableBackground, -1)) return;
            horizontalMovement = -1f;
        }else if (Input.GetKey(KeyCode.RightArrow))
        {
            if(!_screenDetection.CanMove(moveableBackground, 1)) return;
            horizontalMovement = 1f;
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
        if (_gun == null)
        {
            _gun = FindAnyObjectByType<Gun>();
        }
        gunAmmo.text = "Ammo: " + _gun.ammo;
    }
    
    //Turn the panel on and off, generally based on button clicks.
    public void ToggleFishing()
    {
        fishingPanel.SetActive(!fishingPanel.activeSelf);
        inUI = !inUI;
        _playerController.canFish = !fishingPanel.activeSelf;
        _fishSpawner.UIClose();

        if (fishingPanel.activeSelf)
        {
            UpdateUIText();
        }
    }
}
