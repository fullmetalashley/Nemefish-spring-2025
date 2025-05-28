using TMPro;
using UnityEngine;

//Update the HUD, and provide interaction elements. 
public class MainGameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject mainMenuPanel;

    private PlayerInventory _playerInventory;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerInventory = FindAnyObjectByType<PlayerInventory>();
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        //Update the health text
        //Update all inventory
        healthText.text = "" + _playerInventory.playerHealth;
    }

    public void ToggleMainMenu()
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
}
