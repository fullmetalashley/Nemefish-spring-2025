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
    private RectTransform _rectTransform;
    private readonly Vector3[] _corners = new Vector3[4];

    public FishScriptable fish;

    private void Start()
    {
        _quicktimeManager = FindAnyObjectByType<QuicktimeManager>();
        _fishingRod = FindAnyObjectByType<FishingRod>();
        _rectTransform = GetComponent<RectTransform>();
        
        if (_rectTransform != null)
        {
            _rectTransform.GetWorldCorners(_corners);
        }
        else
        {
            Debug.LogWarning("RectTransform not assigned.");
        }


    }

    public void UpdatingCorners()
    {
        _rectTransform.GetWorldCorners(_corners);
    }
    
    //Ash note: For whatever reason, Contains is not working with either the rectTransform or the collider. I have scrapped it for now. This is working. 
    public bool PointWithinCorners(Vector2 pointToCheck)
    {
        UpdatingCorners();
        
        if (!(pointToCheck.x >= _corners[0].x)) return false;
        if (!(pointToCheck.x <= _corners[3].x)) return false;
        if (!(pointToCheck.y >= _corners[0].y)) return false;
        return pointToCheck.y <= _corners[1].y;
    }
}
