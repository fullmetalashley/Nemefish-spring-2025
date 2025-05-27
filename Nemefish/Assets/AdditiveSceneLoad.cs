using UnityEngine;
using UnityEngine.SceneManagement;

public class AdditiveSceneLoad : MonoBehaviour
{
    [SerializeField] private string[] _sceneToLoad;
   
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (var t in _sceneToLoad)
        {
            SceneManager.LoadScene(t, LoadSceneMode.Additive);
        }
    }
}
