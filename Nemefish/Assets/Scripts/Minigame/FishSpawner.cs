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

    public int activeFishLimit;

    public List<SpawnPoint> spawnPositions;
    public List<GameObject> allFishMovementPoints;

    public GameObject regularFishPrefab;

    public GameObject environment;

    //These need to be set based on the area they're in
    public bool regularFishSpawningAllowed;
    public bool mutantFishSpawningAllowed;

    public float regularSpawnTimer;
    public float regularSpawnTimerBase;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
    }
    
    // Update is called once per frame
    private void Update()
    {
        if (!mutantActive)
        {
            spawnTimer -= Time.deltaTime;
            if (!(spawnTimer <= 0)) return;
            
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
        /*
        mutantActive = false;
        spawnTimer = spawnTimerBase;
        damageTimer = damageTimerBase;
        */
    }

    //A fish has been shot! We need to handle mutants a little different, so check for mutants
    public void ShotFish(BoundsDetection shotFish)
    {
        fishBounds.Remove(shotFish);
        CheckForMutants();
        Destroy(shotFish.gameObject);
    }
    
    //Spawn a regular fish
    public void SpawnFish()
    {
        int randomSpawnSpot = Random.Range(0, spawnPositions.Count);
        var newFish = Instantiate(regularFishPrefab, environment.transform);
        newFish.transform.position = spawnPositions[randomSpawnSpot].gameObject.transform.position;

        int randomFish = Random.Range(0, fish.Count);
        newFish.GetComponent<BoundsDetection>().fish = fish[randomFish];

        //We need to set some random movement points 
        int movementPoints = Random.Range(0, allFishMovementPoints.Count);

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

    public void RoomToSpawn()
    {
        /*
        int fishCount = 0;
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            if (spawnPositions[i]._currentBounds.fish != null)
            {
                //We have a fish here. Up the count.
                fishCount++;
            }
        }

        if (fishCount < activeFishLimit)
        {
            Debug.Log("We can spawn another fish");
        }
        */
    }
  }
