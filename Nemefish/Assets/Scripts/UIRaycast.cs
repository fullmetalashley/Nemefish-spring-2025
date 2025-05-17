using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIRaycast : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject testObject;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool IsPointerOverUIElement()
    {
        return IsPointerOverUIElement(GetEventSystemRaycastResults());
    }

    public bool IsPointerOverUIElement(GameObject gameObject)
    {
        List<RaycastResult> eventSystemRaycastResults = GetEventSystemRaycastResults();
        
        for(int index = 0;  index < eventSystemRaycastResults.Count; index ++)
        {
            RaycastResult curRaycastResult = eventSystemRaycastResults [index];
            //This is one of the things we hit. Is it what we want? 
            if (curRaycastResult.gameObject == gameObject)
            {
                Debug.Log("Did it!");
            }
        }
        return false;
    }
    
    ///Returns 'true' if we touched or hovering on Unity UI element.
    public static bool IsPointerOverUIElement(List<RaycastResult> eventSystemRaysastResults )
    {
        for(int index = 0;  index < eventSystemRaysastResults.Count; index ++)
        {
            RaycastResult curRaysastResult = eventSystemRaysastResults [index];

            if (curRaysastResult.gameObject.layer == LayerMask.NameToLayer("UI"))
                return true;
        }

        return false;
    }

    ///Gets all event systen raycast results of current mouse or touch position.
    public List<RaycastResult> GetEventSystemRaycastResults()
    {   
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position =  Input.mousePosition;

        List<RaycastResult> raysastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll( eventData, raysastResults );

        return raysastResults;
    }
}
