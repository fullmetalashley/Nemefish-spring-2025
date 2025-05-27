using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoad : MonoBehaviour
{
    [SerializeField] private string[] _sceneToLoad;
    [SerializeField] private string[] _conditionalSceneToLoad;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var t in _sceneToLoad)
        {
            SceneManager.LoadScene(t, LoadSceneMode.Additive);
        }
    }

    public void LoadSpecific(string _specificScene)
    {
        SceneManager.LoadScene(_specificScene, LoadSceneMode.Additive);
    }

    public void UnloadSpecific(string _specificScene)
    {
        SceneManager.UnloadSceneAsync(_specificScene);
    }
}
