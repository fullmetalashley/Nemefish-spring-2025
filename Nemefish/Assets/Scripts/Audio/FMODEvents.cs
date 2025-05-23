using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference forestAmbience { get; private set; }
    [field: SerializeField] public EventReference coastAmbience { get; private set; }
    [field: SerializeField] public EventReference ptoWalla { get; private set; }
    
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootstepsDefault { get; private set; }
    [field: SerializeField] public EventReference injuryGrunt { get; private set; }
    
    [field: Header("Gun SFX")]
    [field: SerializeField] public EventReference wormGunStandard { get; private set; }
    public static FMODEvents instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one FMOD Events instance in the scene.");
        }
        instance = this;
    }

}
