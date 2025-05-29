using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Dynamic;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
   
    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Audio Manager in the scene.");
        }
        instance = this;

    }

    private void Start()
    {
        var activeScene = SceneManager.GetActiveScene();
        InitializeScore(FMODEvents.instance.score);
        InitializeAmbience(FMODEvents.instance.coastAmbience);
        InitializeAmbience(FMODEvents.instance.forestAmbience);
        if (activeScene.name == "Player Camp")
        {
            InitializeAmbience(FMODEvents.instance.ptoWalla);
        }
    }

    private void InitializeAmbience(EventReference ambienceEventReference)
    {
        ambienceEventInstance = CreateEventInstance(ambienceEventReference);
        ambienceEventInstance.start();
    }

    private void InitializeScore(EventReference musicEventReference)
    {
        musicEventInstance = CreateEventInstance(musicEventReference);
        musicEventInstance.start();
    }

    public void SetAmbienceParameter(string parameterName, float parameterValue)
    {
        ambienceEventInstance.setParameterByName(parameterName, parameterValue);
    }

    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public EventInstance CreateEventInstance(EventReference eventReference)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventReference);
        return eventInstance;
    }

    
}
