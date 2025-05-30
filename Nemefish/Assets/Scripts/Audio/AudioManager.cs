using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System.Dynamic;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine.SceneManagement;
using JetBrains.Annotations;

public class AudioManager : MonoBehaviour
{
    private List<EventInstance> eventInstances;
    private List<StudioEventEmitter> eventEmitters;
   
    private EventInstance ambienceEventInstance;
    private EventInstance musicEventInstance;

    public static AudioManager instance { get; private set; }
    public EventInstance ptoWalla;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // Debug.LogError("Found more than one Audio Manager in the scene.");
            Destroy(gameObject);
        }


    }

    private void Start()
    {
        InitializeScore(FMODEvents.instance.score);
        InitializeAmbience(FMODEvents.instance.coastAmbience);
        InitializeAmbience(FMODEvents.instance.forestAmbience);
        ptoWalla = instance.CreateEventInstance(FMODEvents.instance.ptoWalla);
       
    }

    void Update()
    {
        var activeScene = SceneManager.GetActiveScene();

        if (activeScene.name == "Player Camp")
        {
            PLAYBACK_STATE playbackState;
            ptoWalla.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                ptoWalla.start();
            }
        }
        else
        {
            ptoWalla.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
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
