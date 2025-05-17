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

    [Header("Animators")]
    public Animator rodAnimator;
    
    //TODO: Figure this stupid thing out
    private UILineRenderer _uiLineRenderer;

    //TODO: Get this out of here and into the player controller
    public int playerHealth = 10;

    //Script refs
    private CombatController _combatController;
    private FishingRod _fishingRod;
    private PlayerController _playerController;

    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _playerController = FindAnyObjectByType<PlayerController>();
        playerHPText.text = "HP: " + playerHealth;
        _uiLineRenderer = FindAnyObjectByType<UILineRenderer>();
        _combatController = FindAnyObjectByType<CombatController>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _playerController.withinInteractionSpace)
        {
            ToggleFishing();
            _playerController.canFish = false;
        }
        
        sliderFill.color = chargeBar.value switch
        {
            >= 0 and <= 500 => Color.green,
            > 500 and <= 800 => Color.yellow,
            > 800 and <= 1000 => Color.red,
            _ => sliderFill.color
        };
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
        _combatController.UIClose();
    }
}
