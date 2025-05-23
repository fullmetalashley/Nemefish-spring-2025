using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadNext()
    {
        System.Console.WriteLine("Transitioning to scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
