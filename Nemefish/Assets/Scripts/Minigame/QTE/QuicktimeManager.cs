using UnityEngine;

public class QuicktimeManager : MonoBehaviour
{
    public GameObject quicktimeBar;

    public BoundsDetection currentBounds;
    
    private PlayerInventory _playerInventory;

    void Start()
    {
        _playerInventory = FindAnyObjectByType<PlayerInventory>();
    }    
    public void TriggerQuicktime(bool status)
    {
        quicktimeBar.SetActive(status);
    }

    public void WonQuicktime()
    {
        quicktimeBar.SetActive(false);
        _playerInventory.AddItem(currentBounds.fish);
        
    }
    
    public void LostQuicktime()
    {
        quicktimeBar.SetActive(false);
    }
}
