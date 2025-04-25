using TMPro;
using UnityEngine;
using Yarn.Unity;

public class VariableStorage : MonoBehaviour
{
    //VARIABLES
    public int testInt;
    public string testString;
    public float testFloat;

    //UGUI ELEMENTS
    public TextMeshProUGUI testText;
    
    //SCRIPTS
    private DialogueRunner _dialogueRunner;

    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueRunner = FindAnyObjectByType<DialogueRunner>();

    }
}
