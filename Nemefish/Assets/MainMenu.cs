
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private string _sceneToLoad;
    
    //Called by the button
    public void QuitGame()
    {
        Application.Quit();
    }

    //Called by the button
    public void LoadScene()
    {
        SceneManager.LoadScene(_sceneToLoad);
    }

    public void InputFieldChanged()
    {
        Debug.Log("Changed");
    }
}
