using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    public void LoadNext()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
