using System;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public bool playerWithinRange;
    public bool dialogueRunning;
    public bool autoInteract;

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
        if (Input.GetKeyDown(KeyCode.E) && !dialogueRunning && playerWithinRange)
        {
            //Trigger the dialogue.
            CallYarn();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //This is the player character, and this object is now interactable. 
            this.playerWithinRange = true;
            if (autoInteract)
            {
                if (_dialogueRunner == null)
                {
                    _dialogueRunner = FindAnyObjectByType<DialogueRunner>();
                }
                CallYarn();
            }
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
        dialogueRunning = true;
        Debug.Log("Entering Yarn node " + yarnNode);

        if (!_dialogueRunner)
        {
            Debug.Log("No dialogue runner found for Interactible with yarn node " + yarnNode);
            return;
        }

        _dialogueRunner.StartDialogue(yarnNode);
    }

    [YarnCommand("leap")]
    public void EndDialogue()
    {
        Debug.Log("Stopping dialogue");
        dialogueRunning = false;
    }
}
