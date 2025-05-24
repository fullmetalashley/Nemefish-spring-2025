using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FishSpawner : MonoBehaviour
{
    [Header("Fish lists")]
    public List<FishScriptable> fish;
    public List<FishScriptable> mutantFish;

    public List<BoundsDetection> fishBounds;
   
    public GameObject spawnPosition;
    public GameObject mutantPrefab;

    [Header("Spawn metrics")]
    public float spawnTimerBase;
    public float spawnTimer;
    public bool mutantActive;

    [Header("Damage metrics")]
    public float damageTimerBase;
    public float damageTimer;

    private FishingRod _fishingRod;

    public BoundsDetection _currentBounds;

    private QuicktimeManager _quicktimeManager;
    private UIManager _uiManager;

    public int activeFishLimit;
    public int activeMutantLimit;

    public List<SpawnPoint> spawnPositions;
    public List<GameObject> allFishMovementPoints;

    public GameObject regularFishPrefab;

    public GameObject environment;

    //These need to be set based on the area they're in
    public bool regularFishSpawningAllowed;
    public bool mutantFishSpawningAllowed;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _uiManager = FindAnyObjectByType<UIManager>();
        
        if (regularFishSpawningAllowed)
        {
            SpawnFish();
        }
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!_uiManager.inUI) return;   //If we're not in the UI, we can't do it
        //If we can spawn fish, keep doing it until we can't.
        if (regularFishSpawningAllowed)
        {
            if (CheckFishLimit("Normal"))
            {
                SpawnFish();
            }
            else
            {
                regularFishSpawningAllowed = false;
            }
        }

        if (!mutantFishSpawningAllowed) return; //If we can't spawn mutants, don't spawn them.
        
        if (!mutantActive)
        {
            spawnTimer -= Time.deltaTime;
            if (!(spawnTimer <= 0)) return;
            SpawnMutant();
        }
        else
        {
            damageTimer -= Time.deltaTime;
            if (!(damageTimer <= 0)) return;
            
            //Timer has ended, and player takes damage.
            FindAnyObjectByType<UIManager>().playerHealth -= _currentBounds.fish._damageDealt;
            FindAnyObjectByType<UIManager>().UpdateUIText();
            damageTimer = damageTimerBase;
        }
    }

    public void SpawnMutant()
    {
        if (!CheckFishLimit("Mutant")) return;    //We have hit the limit of fish on the screen, so we will not spawn more.

        //If we hit spawn time, instantiate the mutant prefab. Only one prefab for now. 
        //TODO: Make this a list of mutants, and spawn them at random?
        var newMutant = Instantiate(mutantPrefab, environment.transform);
        newMutant.transform.position = environment.transform.position;

        //Add the detection spot to the list
        fishBounds.Add(newMutant.GetComponent<BoundsDetection>());
        _currentBounds = newMutant.GetComponent<BoundsDetection>();
        mutantActive = true;
        spawnTimer = spawnTimerBase;
    }
    
    public bool CalculateDistance(GameObject thingToCheck)
    {
        //Check the fishing spots to see if the object (bullet or bobber) landed in one of them. 
        foreach (BoundsDetection fishSpot in fishBounds)
        {
            if (fishSpot.PointWithinCorners(thingToCheck.transform.position))
            {
                //This means that we are within bounds of the fish. 
                _currentBounds = fishSpot;
                return true;
            }
        }
        return false;
    }

    //Reset all of this once the UI has closed
    public void UIClose()
    {
        mutantActive = false;
        spawnTimer = spawnTimerBase;
        damageTimer = damageTimerBase;
        
        //Erase the lists of active fish
        for (int i = 0; i < fishBounds.Count; i++)
        {
            Destroy(fishBounds[i].gameObject);
        }
        fishBounds.Clear();
    }

    //A fish has been shot! We need to handle mutants a little different, so check for mutants
    public void ShotFish(BoundsDetection shotFish)
    {
        fishBounds.Remove(shotFish);
        CheckForMutants();
        Destroy(shotFish.gameObject);

        FishScatter();
    }

    public void FishScatter()
    {
        for (int i = 0; i < fishBounds.Count; i++)
        {
            if (fishBounds[i].fish._fishType == "Mutant") break; 
            fishBounds[i].gameObject.GetComponent<FishMovement>().scatter = true;
        }
    }

    public bool CheckFishLimit(string fishType)
    {
        int count = 0;
        for (int i = 0; i < fishBounds.Count; i++)
        {
            if (fishBounds[i].fish._fishType == fishType)
            {
                count++;
            }
        }

        if (fishType == "Mutant")
        {
            return (count < activeMutantLimit);
        }
        return (count < activeFishLimit);
    }

    public int CheckFishCount(string fishType)
    {
        int count = 0;
        for (int i = 0; i < fishBounds.Count; i++)
        {
            if (fishBounds[i].fish._fishType == fishType)
            {
                count++;
            }
        }
        return count;
    }
    
    //Spawn a regular fish
    public void SpawnFish()
    {
        if (!CheckFishLimit("Normal")) return;    //We have hit the limit of fish on the screen, so we will not spawn more.
        
        int randomSpawnSpot = Random.Range(0, spawnPositions.Count);
        var newFish = Instantiate(regularFishPrefab, environment.transform);
        newFish.transform.position = spawnPositions[randomSpawnSpot].gameObject.transform.position;

        int randomFish = Random.Range(0, fish.Count);
        newFish.GetComponent<BoundsDetection>().fish = fish[randomFish];

        //We need to set some random movement points 
        int movementPoints = Random.Range(2, allFishMovementPoints.Count);

        List<GameObject> newPoints = new List<GameObject>();
        for (int i = 0; i < movementPoints; i++)
        {
            newPoints.Add(allFishMovementPoints[i]);
        }

        newPoints = ShuffleList(newPoints);
        newFish.GetComponent<FishMovement>().SetMovementPoints(newPoints);
        
        fishBounds.Add(newFish.GetComponent<BoundsDetection>());
    }
    
    public List<GameObject> ShuffleList(List <GameObject> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            GameObject temp = list[i];
            list[i] = list[j];
            list[j] = temp;
        }
        return list;
    }

    //Right now, we can only have one mutant at a time.
    public void CheckForMutants()
    {
        foreach (BoundsDetection bounds in fishBounds)
        {
            if (bounds.fish._fishType == "Mutant")
            {
                mutantActive = true;
                return;
            }
        }
        mutantActive = false;
    }

    public void RemoveFish(BoundsDetection fishToRemove)
    {
        fishBounds.Remove(fishToRemove);
        CheckForMutants();
        Destroy(fishToRemove.gameObject);
    }
  }
