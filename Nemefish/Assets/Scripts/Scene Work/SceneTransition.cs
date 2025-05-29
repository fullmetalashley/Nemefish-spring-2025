using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public float spawnPointX;
    public float spawnPointY;
    public float spawnPointZ;

    public float fohiSpawnX;
    public float fohiSpawnY;
    public float fohiSpawnZ;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNext();
        }

    }

    public void LoadNext()
    {
        Debug.Log("Transitioning to scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);

        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];

        if (player != null)
        {
            player.GetComponent<Rigidbody>().position = new Vector3(spawnPointX, spawnPointY, spawnPointZ);
        }

        GameObject fohi = GameObject.FindGameObjectsWithTag("FOHI")[0];

        if (fohi != null)
        {
            fohi.GetComponent<Rigidbody>().position = new Vector3(fohiSpawnX, fohiSpawnY, fohiSpawnZ);
        }
    }
}
