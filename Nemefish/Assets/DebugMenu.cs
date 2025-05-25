using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TextMeshProUGUI data;

    private FishSpawner _fishSpawner;

    public List<GameObject> environments;
    private int areaTracker;
    
    
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
