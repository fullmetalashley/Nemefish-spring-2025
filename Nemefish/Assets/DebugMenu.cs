using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI data;

    private FishSpawner _fishSpawner;
    
    void Start()
    {
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
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
        data.text += "Changing area to [area]\n";
    }

    public void SpawnMutant()
    {
        _fishSpawner.SpawnMutant();
        data.text += "Spawned mutants: " + _fishSpawner.CheckFishCount("Mutant") + "\n";
    }
}
