using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using Color = UnityEngine.Color;
using Image = UnityEngine.UI.Image;

public class LineCastTracking : MonoBehaviour
{

    public RectTransform lineRectTransform; // The UI line image
    public RectTransform canvasRectTransform; // The canvas RectTransform
    public RectTransform originPoint; // The fixed origin point
    public Image lineImage; // The Image component for color gradient

    private FishingRod _fishingRod;

    void Start()
    {
        _fishingRod = FindAnyObjectByType<FishingRod>();
    }

    void Update()
    {
        Vector2 originLocal, mouseLocal;

        // Convert screen points to local points in the canvas
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, originPoint.position, null, out originLocal);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvasRectTransform, Input.mousePosition, null, out mouseLocal);

        //Determine what the direction is for the line to follow, and use magnitude to determine the length of that vector
        Vector2 direction = mouseLocal - originLocal;
        float distance = direction.magnitude;

        // Set position to midpoint
        lineRectTransform.localPosition = originLocal + direction * 0.5f;

        // Set rotation
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        lineRectTransform.localRotation = Quaternion.Euler(0, 0, angle);

        // Set length
        lineRectTransform.sizeDelta = new Vector2(distance, lineRectTransform.sizeDelta.y);
        
        // Set color based on charge
        lineImage.color = Color.Lerp(Color.green, Color.red, _fishingRod.charge * 2f); // Green → Yellow → Red
        if (_fishingRod.charge < 500f)
        {
            lineImage.color = Color.Lerp(Color.green, Color.yellow, _fishingRod.charge * 2f);
        }
        else
        {
            lineImage.color = Color.Lerp(Color.yellow, Color.red, (_fishingRod.charge - 0.5f) * 2f);
        }
    }

}

