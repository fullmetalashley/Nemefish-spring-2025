using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNext();
        }
    }
    public void LoadNext()
    {
        System.Console.WriteLine("Transitioning to scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
