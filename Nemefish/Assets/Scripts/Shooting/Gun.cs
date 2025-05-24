using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    
    public float zoomSpeed = 0.1f;
    public float scrollSpeed = 10f;
    public float bulletSpeed = 10;

    public Vector3 direction;

    public bool gunActive;

    private FishSpawner _fishSpawner;

    public GameObject crosshair;

    void Start()
    {
        _fishSpawner = FindAnyObjectByType<FishSpawner>();
    }

    void Update()
    {
        if (!gunActive) return;
        if (Input.GetMouseButtonDown(0))
        {
            if (_fishSpawner.CalculateDistance(crosshair))
            {
                Debug.Log("Fish in bounds");
                _fishSpawner.ShotFish(_fishSpawner._currentBounds);
            }
            else
            {
                _fishSpawner.FishScatter();
            }
        }
    }
}