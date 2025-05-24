using UnityEngine;

public class QuicktimeManager : MonoBehaviour
{
    [Header("QTE Bar setup")]
    public GameObject quicktimeBar;
    public BoundsDetection currentBounds;   //Set when the mutant is spawned
    
    //Script refs
    private PlayerInventory _playerInventory;
    private FishingRod _fishingRod;
    private TugOfWarController _qteBar;
    private FishSpawner _fishSpawner;

    private void Start()
    {
        _playerInventory = FindAnyObjectByType<PlayerInventory>();
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _qteBar = quicktimeBar.GetComponent<TugOfWarController>();
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
    }    
    
    //Trigger this to toggle the QTE on and off
    public void TriggerQuicktime(bool status)
    {
        quicktimeBar.SetActive(status);
        _qteBar.moveSpeed *= currentBounds.fish._tugStrength;
    }

    //Determine results of the QTE game
    public void EndQuicktime(bool victory)
    {
        quicktimeBar.SetActive(false);
        _qteBar.moveSpeed = 100f;
        if (victory)
        {
            _playerInventory.AddItem(currentBounds.fish._successItem);
            _fishSpawner.RemoveFish(currentBounds);
        }
        else
        {
            _fishSpawner.FishScatter(); //Fish need to scatter on a miss
        }
    }
}
