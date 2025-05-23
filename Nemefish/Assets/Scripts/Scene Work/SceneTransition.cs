using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;
    FMOD.Studio.Bus ambienceBus;

    public void Start()
    {
        // Stops all ambience events currently playing - needs to be updated once buses are added
        ambienceBus = FMODUnity.RuntimeManager.GetBus("Bus:/");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNext();
        }
    }
    public void LoadNext()
    {
        ambienceBus.stopAllEvents(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        System.Console.WriteLine("Transitioning to scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
