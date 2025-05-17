using Radishmouse;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject fishingPanel;
    public bool inUI;

    public Slider chargeBar;
    public Image sliderFill;

    public GameObject fishingMap;

    public GameObject fishingRod;
    private FishingRod _fishingRod;

    public TextMeshProUGUI playerHPText;
    public int playerHP;

    public GameObject filletPanel;

    public Animator rodAnimator;

    private PlayerController _playerController;

    private UILineRenderer _uiLineRenderer;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _playerController = FindAnyObjectByType<PlayerController>();
        playerHPText.text = "HP: " + _playerController.playerHealth;
        _uiLineRenderer = FindAnyObjectByType<UILineRenderer>();
//        _uiLineRenderer.SetAllDirty();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _playerController.withinInteractionSpace)
        {
            ToggleFishing();
            _playerController.canFish = false;
        }
        
        
        if (chargeBar.value >= 0 && chargeBar.value <= 3)
        {
            sliderFill.color = Color.green;
        }

        if (chargeBar.value > 3 && chargeBar.value <= 4)
        {
            sliderFill.color = Color.yellow;
        }
        
        if (chargeBar.value > 4 && chargeBar.value <= 5)
        {
            sliderFill.color = Color.red;
        }
    }

    public void UpdateUIText()
    {
        playerHPText.text = "HP: " + +_playerController.playerHealth;
    }

 /*   public void ToggleFillet()
    {
        ToggleFishing();
        inUI = true;
        filletPanel.SetActive(!filletPanel.activeSelf);
    }
    */
    
    public void ToggleFishing()
    {
//        rodAnimator.enabled = false;
        fishingPanel.SetActive(!fishingPanel.activeSelf);
        Debug.Log("in UI: " + inUI);
        inUI = !inUI;
        Debug.Log("in UI pt 2: " + inUI);
        _playerController.canFish = !fishingPanel.activeSelf;
        //       fishingRod.SetActive(!fishingRod.activeSelf);
        //       fishingMap.SetActive(!fishingMap.activeSelf);


    }
}
