using TMPro;
using UnityEngine;

//Update the HUD, and provide interaction elements. 
public class MainGameHUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText;

    [SerializeField] private GameObject mainMenuPanel;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        //Update the health text
        //Update all inventory
    }

    public void ToggleMainMenu()
    {
        mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
    }
}
