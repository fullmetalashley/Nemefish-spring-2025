using UnityEngine;

public class ToolSwap : MonoBehaviour
{
    public Gun _gun;
    public SlingshotCast _fishingRod;

    public GameObject fishingRod;
    public GameObject gun;
    public GameObject lineRenderer;
    
    void Start()
    {
        _gun = FindAnyObjectByType<Gun>();
        _fishingRod = FindAnyObjectByType<SlingshotCast>();
    }
    
    // Update is called once per frame
    void Update()
    {
        // Get the scroll wheel input
        float scrollInput = Input.GetAxis("Mouse ScrollWheel");

        // Example: Zoom the camera
        if (scrollInput != 0)
        {
            SwapItem(scrollInput);
        }
    }

    public void SwapItem (float scrollInput)
    {
        if (scrollInput >= 0)
        {
            if (_fishingRod.rodActive) return;
            //This means it's a scroll up, so this should be the rod
            _gun.gunActive = false;
            _fishingRod.rodActive = true;
            gun.SetActive(false);
            fishingRod.SetActive(true);
        }
        else
        {
            if (_gun.gunActive) return;
            //This means it's a scroll down, so this should be the gun
            _gun.gunActive = true;
            _fishingRod.rodActive = false;
            gun.SetActive(true);
            fishingRod.SetActive(false);
        }
        lineRenderer.SetActive(_fishingRod.rodActive);
    }
}
