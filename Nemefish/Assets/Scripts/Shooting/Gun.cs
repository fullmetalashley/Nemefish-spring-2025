using UnityEngine;

public class Gun : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;

    public Vector3 direction;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody>().linearVelocity = direction * bulletSpeed;
        }
    }

    public void SetSpawnPoint(Transform spawnPoint, int dir)
    {
        bulletSpawnPoint = spawnPoint;
        direction = bulletSpawnPoint.forward * dir;
    }
}
