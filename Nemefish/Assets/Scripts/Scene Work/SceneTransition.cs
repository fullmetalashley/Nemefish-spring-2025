using UnityEngine;
using UnityEngine.SceneManagement;
using FMOD.Studio;

public class SceneTransition : MonoBehaviour
{
    public string sceneToLoad;

    private EventInstance ptoWalla;
    // private AudioManager audioManager;

    public void Start()
    {
        // audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        ptoWalla = AudioManager.instance.CreateEventInstance(FMODEvents.instance.ptoWalla);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            LoadNext();
        }
        if (sceneToLoad == "Player Camp")
        {
            // audioManager.InitializeAmbience(FMODEvents.instance.ptoWalla);
            PLAYBACK_STATE playbackState;
            ptoWalla.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                ptoWalla.start();
            }
        }
        else
        {
            ptoWalla.stop(STOP_MODE.ALLOWFADEOUT);
        }
    }
    public void LoadNext()
    {
        System.Console.WriteLine("Transitioning to scene " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }
}
