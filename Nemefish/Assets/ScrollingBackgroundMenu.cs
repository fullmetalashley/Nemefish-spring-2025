using UnityEngine;

public class ScrollingBackgroundMenu : MonoBehaviour

{
    public float maxX = 990f;
    public float speed = 0.04f;
    public float currentPositionX = 165f;

    // Update is called once perf frame
    private void Update()
    {
        currentPositionX += speed;
        this.gameObject.transform.position = new Vector3(currentPositionX, 169, 0);
        if (currentPositionX > maxX)
        {
            this.gameObject.transform.position = new Vector3(165, 169, 0);
            currentPositionX = 0;
        }
    }

}

