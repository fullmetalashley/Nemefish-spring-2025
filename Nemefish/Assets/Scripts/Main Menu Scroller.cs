using Unity.VisualScripting;
using UnityEngine;

public class MainMenuSCroller : MonoBehaviour
{
    public float maxX = 1909f;
    public float speed = 0.04f;
    public float currentPositionX = 0f;

    // Update is called once perf frame
    private void Update()
    {
        currentPositionX += speed;
        this.gameObject.transform.position = new Vector3(0, currentPositionX, 0);
        if (currentPositionX > maxX) ;
        {
            this.gameObject.transform.position = new Vector3(0, 0, 0);
            currentPositionX = 0;
        }
    }

}
    
