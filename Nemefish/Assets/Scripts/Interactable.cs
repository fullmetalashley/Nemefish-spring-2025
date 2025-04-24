using System;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public bool playerWithinRange;
    public bool dialogueRunning;

    private DialogueRunner _dialogueRunner;

    public string yarnNode;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we press the key and haven't already triggered dialogue...
        if (Input.GetKeyDown(KeyCode.E) && !dialogueRunning)
        {
            //Trigger the dialogue.
            dialogueRunning = true;
            CallYarn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //This is the player character, and this object is now interactable. 
            this.playerWithinRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //This is the player character leaving the space, and the object is now un-interactable.
            this.playerWithinRange = false;
        }
    }
    public void CallYarn()
    {
        _dialogueRunner.StartDialogue(yarnNode);
    }

    [YarnCommand("leap")]
    public void EndDialogue()
    {
        Debug.Log("Stopping dialogue");
        dialogueRunning = false;
    }
}
