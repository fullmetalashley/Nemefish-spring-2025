using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    public bool autoTransition;


    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && autoTransition)
        {
            LoadNextScene();
        }
    }
    
    public void LoadNextScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
        
}
