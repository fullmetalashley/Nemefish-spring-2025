using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public List<GameObject> movementPoints;
    public int targetIndex;
    public float moveSpeed;
    private float doubleSpeed;
    private float baseSpeed;

    public float waitTimer;
    public float waitTimerBase;

    public bool waiting;
    public int direction = 1;

    public bool scatter;
    public float scatterTimer;
    private float scatterTimerBase;

    void Start()
    {
        scatter = false;
        doubleSpeed = moveSpeed * 1.5f;
        baseSpeed = moveSpeed;
        scatterTimerBase = scatterTimer;
    }
    


    // Update is called once per frame
    void Update()
    {
        if (movementPoints == null || movementPoints.Count == 0) return;


        moveSpeed = scatter ? doubleSpeed : baseSpeed;  //If we're supposed to scatter, double our speed.

        if (scatter)
        {
            scatterTimer -= Time.deltaTime;
            waitTimer = 1f;
            waiting = false;
            
            if (scatterTimer <= 0)
            {
                scatter = false;
                scatterTimer = scatterTimerBase;
            }
        }
        
        if (!waiting)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position,
                movementPoints[targetIndex].transform.position, moveSpeed);
            
           if (Vector2.Distance(this.gameObject.transform.position, movementPoints[targetIndex].transform.position) <=
                1)
           {
               movementPoints[targetIndex].GetComponent<FishMovementTarget>().currentFish =
                   this.GetComponent<BoundsDetection>();
                this.GetComponent<BoundsDetection>().UpdatingCorners();

                targetIndex = Random.Range(0, movementPoints.Count);
                waiting = true;

                if (movementPoints[targetIndex].GetComponent<FishMovementTarget>().currentFish != null)
                {
                    //TODO: We need to check the entire list, and see if anything is clear first. 
                }
                
                movementPoints[targetIndex].GetComponent<FishMovementTarget>().currentFish =
                    this.GetComponent<BoundsDetection>();
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                movementPoints[targetIndex].GetComponent<FishMovementTarget>().currentFish = null;
                
                waitTimer = waitTimerBase;
                waiting = false;
                direction *= -1;
                //We can tell this movement spot that it's not occupied anymore
            }
        }
    }

    public void SetMovementPoints(List<GameObject> newPoints)
    {
        movementPoints = new List<GameObject>();
        for (int i = 0; i < newPoints.Count; i++)
        {
            movementPoints.Add(newPoints[i]);
        }
        
        //Also set the fish's moveSpeed to match their scriptable object
        moveSpeed = this.GetComponent<BoundsDetection>().fish._swimSpeed;
    }
}
