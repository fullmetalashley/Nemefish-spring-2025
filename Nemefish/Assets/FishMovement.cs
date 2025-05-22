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
                Vector3 currentRotation = this.GetComponent<RectTransform>().eulerAngles;
                currentRotation.y = (180 * direction);
                this.GetComponent<RectTransform>().eulerAngles = currentRotation;
                this.GetComponent<BoundsDetection>().UpdatingCorners();
            }
        }
    }
}
