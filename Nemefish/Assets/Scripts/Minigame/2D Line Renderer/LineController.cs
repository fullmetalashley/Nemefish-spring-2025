using UnityEngine;

public class LineController : MonoBehaviour
{
    private LineRenderer lr;

    private Transform[] points;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < points.Length; i++)
        {
            lr.SetPosition(i, points[i].position);
        }
    }

    private void Awake()
    {
        lr = GetComponent<LineRenderer>();
    }

    //Sets number of points to the length of the passed array
    public void SetupLine(Transform[] points)
    {
        lr.positionCount = points.Length;
        this.points = points;
    }
    
}
