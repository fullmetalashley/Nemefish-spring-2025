using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float life = 3;

    //Destroys the bullet after X amount of time.
    void Awake()
    {
        Destroy(gameObject, life);
    }

    //Destroy an object if you shoot it
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag != "Terrain")
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
