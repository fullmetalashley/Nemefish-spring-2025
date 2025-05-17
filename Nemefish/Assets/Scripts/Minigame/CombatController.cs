using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{
    [Header("Fish lists")]
    public List<FishScriptable> fish;
    public List<FishScriptable> mutantFish;
    
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
  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
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
            var newMutant = Instantiate(mutantPrefab, spawnPosition.transform);
            newMutant.transform.position = spawnPosition.transform.position;

            //Add the detection spot to the list
            _fishingRod.fishingSpots.Add(newMutant.GetComponent<BoundsDetection>());
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
    
    //Mutant has been shot and can be removed from the list. 
    public void RemoveMutant(GameObject mutant)
    {
        mutantActive = false;
        _fishingRod.fishingSpots.Remove(mutant.GetComponent<BoundsDetection>());
        Destroy(mutant);
    }

    //Reset all of this once the UI has closed
    public void UIClose()
    {
        mutantActive = false;
        spawnTimer = spawnTimerBase;
        damageTimer = damageTimerBase;
    }
  }
