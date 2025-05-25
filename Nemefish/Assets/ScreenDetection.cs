using UnityEngine;

public class ScreenDetection : MonoBehaviour
{
    private Vector3[] _corners = new Vector3[4];
    private RectTransform _rectTransform;

    public GameObject leftSide;
    public GameObject rightSide;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < _corners.Length; i++)
        {
            Debug.Log("Corner " + i + ": " + _corners[i]);
        }
        
        leftSide.transform.position = new Vector3(0f, 0f, 0f);
        rightSide.transform.position = new Vector3((Screen.width), 0f, 0f);

    }

    public void SetCorners(GameObject screenImage)
    {
        screenImage.GetComponent<RectTransform>().GetWorldCorners(_corners);
    }
    
    // ReSharper disable Unity.PerformanceAnalysis
    public bool CanMove(GameObject screenImage, int direction)
    {
        SetCorners(screenImage);
        //corner 0 and 1 are the left. corner 2 and 3 are the right
        switch (direction)
        {
            case 1:
                //We are trying to move to the right. 
                return (_corners[0].x <= leftSide.transform.position.x);
            case -1:
                //We are trying to move to the left. This means our RIGHT side needs to be greater than the right point in order to move left.
                return (_corners[2].x >= rightSide.transform.position.x);
        }
        return false;
    }
}