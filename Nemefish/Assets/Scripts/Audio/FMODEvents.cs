using UnityEngine;
using FMODUnity;

public class FMODEvents : MonoBehaviour
{
    [field: Header("Ambience")]
    [field: SerializeField] public EventReference forestAmbience { get; private set; }
    [field: SerializeField] public EventReference coastAmbience { get; private set; }
    [field: SerializeField] public EventReference ptoWalla { get; private set;  }

    [field: Header("Music")]
    [field: SerializeField] public EventReference score { get; private set; }
    
    [field: Header("Player SFX")]
    [field: SerializeField] public EventReference playerFootstepsDefault { get; private set; }
    [field: SerializeField] public EventReference injuryGrunt { get; private set; }

    [field: Header("FOHI SFX")]
    [field: SerializeField] public EventReference fohiFootstepsDefault { get; private set; }
    
    [field: Header("Minigame SFX")]
    [field: SerializeField] public EventReference wormGunStandard { get; private set; }
    [field: SerializeField] public EventReference filetFish { get; private set; }

    [field: Header("Inventory SFX")]
    [field: SerializeField] public EventReference addItemToInventory { get; private set; }
    [field: SerializeField] public EventReference openCloseInventory { get; private set; }
    [field: SerializeField] public EventReference menuChangeSelection { get; private set; }
    [field: SerializeField] public EventReference menuOptionSelect { get; private set; }
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
