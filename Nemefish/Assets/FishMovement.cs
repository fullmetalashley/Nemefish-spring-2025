using System.Collections.Generic;
using UnityEngine;

public class FishMovement : MonoBehaviour
{
    public List<GameObject> movementPoints;
    public int targetIndex;
    public float moveSpeed;

    public float waitTimer;
    public float waitTimerBase;

    public bool waiting;
    public int direction = 1;
    


    // Update is called once per frame
    void Update()
    {
        if (movementPoints == null || movementPoints.Count == 0) return;
        if (!waiting)
        {
            this.gameObject.transform.position = Vector2.MoveTowards(this.gameObject.transform.position,
                movementPoints[targetIndex].transform.position, moveSpeed);
            
           if (Vector2.Distance(this.gameObject.transform.position, movementPoints[targetIndex].transform.position) <=
                1)
            {
                this.GetComponent<BoundsDetection>().UpdatingCorners();
                targetIndex++;
                if (targetIndex >= movementPoints.Count)
                {
                    targetIndex = 0;
                }
                waiting = true;
            }
        }
        else
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                waitTimer = waitTimerBase;
                waiting = false;
                direction *= -1;
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
