using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class DebugMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI data;

    private FishSpawner _fishSpawner;

    public List<GameObject> environments;
    private int areaTracker;


    private PlayerInventory _playerInventory;
    private MainGameHUD _mainGameHUD;
    private UIManager _uiManager;

    public TMP_InputField yarnNode;
    public TMP_InputField yarnVariable;

    [SerializeField] private GameObject debugMenu;
    
    void Start()
    {
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
        _playerInventory = FindAnyObjectByType<PlayerInventory>();
        _mainGameHUD = FindAnyObjectByType<MainGameHUD>();
        _uiManager = FindAnyObjectByType<UIManager>();
        
        _mainGameHUD.UpdateText();
    }

    public void CheckYarnVariables()
    {
        data.text = "";
        data.text += "\n" + FindAnyObjectByType<InMemoryVariableStorage>().GetDebugList();
    }

    public void CallYarnNode()
    {
        DialogueRunner _dialogue = FindAnyObjectByType<DialogueRunner>();
        
        _dialogue.StartDialogue(yarnNode.text);
    }

    public void SetYarnVariable()
    {
        DialogueRunner _dialogue = FindAnyObjectByType<DialogueRunner>();
        
        _dialogue.VariableStorage.TryGetValue(yarnVariable.text, out bool currentStatus);
        Debug.Log("Currently " + yarnVariable.text + " is set to " + currentStatus);
        
        _dialogue.VariableStorage.SetValue(yarnVariable.text, !currentStatus);
    }
    
    public void ToggleMenu()
    {
        debugMenu.SetActive(!debugMenu.activeSelf);

        if (debugMenu.activeSelf)
        {
            //Try and get those refs again, just in case. 
            _fishSpawner = FindAnyObjectByType<FishSpawner>();
            _playerInventory = FindAnyObjectByType<PlayerInventory>();
            _mainGameHUD = FindAnyObjectByType<MainGameHUD>();
        }
    }

    public void AddItem(string itemToAdd)
    {
        Item newItem = new Item
        {
            _itemName = itemToAdd,
            _quantity = 1
        };

        _playerInventory.AddItem(newItem);
        _mainGameHUD.UpdateText();
    }

    public void ToggleMinigame()
    {
        _uiManager = FindAnyObjectByType<UIManager>();

        _uiManager.ToggleFishing();
    }

    public void CaughtFish()
    {
        data.text += "Caught a fish: [fish name]\n";
    }

    public void ShotFish()
    {
        data.text += "Shot a fish: [fish name]\n";
    }
    
    public void SpawnFish()
    {
        _fishSpawner.SpawnFish();
        data.text += "Spawned fish: " + _fishSpawner.CheckFishCount("Normal") + "\n";
    }

    public void SwapArea()
    {
        areaTracker++;
        
        data.text += "Changing area to [area] " + areaTracker;

        if (areaTracker >= environments.Count)
        {
            areaTracker = 0;
        }

        for (int i = 0; 0 < environments.Count; i++)
        {
            environments[i].SetActive(i == areaTracker);
        }
    }

    public void SpawnMutant()
    {
        _fishSpawner.SpawnMutant();
        data.text += "Spawned mutants: " + _fishSpawner.CheckFishCount("Mutant") + "\n";
    }
}
