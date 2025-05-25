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

    private int prevTarget;
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
            //This means our target is the index we are MOVING to. We have not yet hit our target.
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position,
                movementPoints[targetIndex].transform.position, moveSpeed);
            
           if (Vector2.Distance(this.gameObject.transform.position, movementPoints[targetIndex].transform.position) <=
                1)
           {
               //We have hit our target, so we can set the bounds to this object! 
               movementPoints[targetIndex].GetComponent<FishMovementTarget>().currentFish =
                   this.GetComponent<BoundsDetection>();
                this.GetComponent<BoundsDetection>().UpdatingCorners();
                prevTarget = targetIndex;
                waiting = true;

                
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                //We are leaving, so tell this movement spot it's unoccupied. 
                movementPoints[prevTarget].GetComponent<FishMovementTarget>().currentFish = null;
                
                //Now, we need to pick our next target. 
                targetIndex = CheckForOpening();
                if (targetIndex == -1) targetIndex = 0; //If we have a fish here, don't go there. 
                
                waitTimer = waitTimerBase;
                waiting = false;
                direction *= -1;
                //We can tell this movement spot that it's not occupied anymore
            }
        }
    }

    private int CheckForOpening()
    {
        List<int> openIndex = new List<int>();
        for (int i = 0; i < movementPoints.Count; i++)
        {
            if (movementPoints[i].GetComponent<FishMovementTarget>().currentFish == null)
            {
                //This is an open spot. We can add it to the list. 
                openIndex.Add(i);
            }
        }

        if (openIndex.Count >= 1)
        {
            int randomOpenIndex = Random.Range(0, openIndex.Count);
            return openIndex[randomOpenIndex];
        }
        return -1;
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
