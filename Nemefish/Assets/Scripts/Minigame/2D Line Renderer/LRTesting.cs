using UnityEngine;

public class LRTesting : MonoBehaviour
{
    [SerializeField] private Transform[] points;

    [SerializeField] private LineController line;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        line.SetupLine(points);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
