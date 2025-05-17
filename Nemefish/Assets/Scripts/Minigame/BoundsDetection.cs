using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class BoundsDetection : MonoBehaviour
{
    //Script refs
    private QuicktimeManager _quicktimeManager;
    private FishingRod _fishingRod;
    
    //The refs for the rect we'll be using
    public RectTransform rectTransform;
    private readonly Vector3[] _corners = new Vector3[4];

    public Fish fish;

    void Start()
    {
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _fishingRod = FindAnyObjectByType<FishingRod>();
        
        rectTransform = GetComponent<RectTransform>();
        
        if (rectTransform != null)
        {
            rectTransform.GetWorldCorners(_corners);
        }
        else
        {
            Debug.LogWarning("RectTransform not assigned.");
        }
    }

    //Ash note: For whatever reason, Contains is not working with either the rectTransform or the collider. I have scrapped it for now. This is working. 
    public void PointWithinCorners(Vector2 pointToCheck)
    {
        //First, check the x. 
        if (pointToCheck.x >= _corners[1].x && pointToCheck.x <= _corners[2].x)
        {
            //We are within the X. 
            if (pointToCheck.y >= _corners[0].y && pointToCheck.y <= _corners[1].y)
            {
                Debug.Log("Hit the fishing spot");
                _quicktimeManager.TriggerQuicktime(true);
                _quicktimeManager.currentBounds = this;
                return;
            }
        }
        _fishingRod.CanCastAgain();
    }
}
