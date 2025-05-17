using System.Collections.Generic;
using UnityEngine;

public class CombatController : MonoBehaviour
{

    public int rounds;

    public GameObject mutantFish;
    public GameObject position;

    public float spawnTimerBase;
    public float spawnTimer;

    public bool mutantActive;

    public float damageTimerBase;
    public float damageTimer;

    public int damageDealt;

    public List<GameObject> mutants;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!mutantActive)
        {
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                mutantActive = true;
                var newMutant = Instantiate(mutantFish, new Vector3(0, 0, 0), Quaternion.identity);
                mutants.Add(newMutant);
                spawnTimer = spawnTimerBase;
            }
        }
        else
        {
            damageTimer -= Time.deltaTime;
            if (damageTimer <= 0)
            {
                FindAnyObjectByType<UIManager>().playerHP -= 10;
                FindAnyObjectByType<UIManager>().UpdateUIText();
                damageTimer = damageTimerBase;
            }
        }
    }

    public void RemoveMutant(GameObject mutant)
    {
        mutantActive = false;
        mutants.Remove(mutant);
        Destroy(mutant);
    }
    
    public void CheckTarget()
    {
        Vector2 checkPoint = (Vector2)Input.mousePosition;
    }
}
