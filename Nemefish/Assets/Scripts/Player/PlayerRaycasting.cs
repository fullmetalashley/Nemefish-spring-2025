using Unity.Mathematics;
using UnityEngine;

public class PlayerRaycasting : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private LayerMask _layerMask;

    public Transform raycastPoint;

    //0, 1, 2, 3 as these values
    public enum Direction {North, East, South, West};

    public Direction playerDirection;
    
    void Start()
    {
        playerDirection = Direction.East;
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward));
        
        if (Physics.Raycast(ray, out RaycastHit hitinfo,
                100f, _layerMask))
        {
            Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward) * hitinfo.distance, Color.red);
        }
        else
        {
            Debug.DrawRay(raycastPoint.position, raycastPoint.TransformDirection(Vector3.forward) * 20f, Color.green);
        }
    }

    public void ChangeDirection(int dir)
    {
        switch (dir)
        {
            case 0:
                playerDirection = Direction.North;
                raycastPoint.transform.rotation = Quaternion.Euler(0f, 0f, 0f);
                break;
            case 1:
                playerDirection = Direction.East;
                raycastPoint.transform.rotation = Quaternion.Euler(0f, 90f, 0f);

                break;
            case 2:
                playerDirection = Direction.South;
                raycastPoint.transform.rotation = Quaternion.Euler(0f, 180f, 0f);

                break;
            case 3:
                playerDirection = Direction.West;
                raycastPoint.transform.rotation = Quaternion.Euler(0f, -90f, 0f);

                break;
            default:
                break;
        }
    }
}
