using System;
using UnityEngine;
using Yarn.Unity;

public class Interactable : MonoBehaviour
{
    public bool triggersOnProximity;
    public bool triggersOnMouseClick;
    public bool triggersOnInteractButton;
    public bool isSingleUse;

    public bool hasDialogue;
    public string yarnNode;

    public bool teleportPlayerAfterUse;
    public float teleportX;
    public float teleportY;
    public float teleportZ;

    public string tagOfItemGivenOnUse;

    public bool playerWithinRange = false;
    public bool dialogueRunning = false;
    public bool hasBeenUsed = false;
    private DialogueRunner _dialogueRunner;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _dialogueRunner = FindAnyObjectByType<DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        //If we press the key and haven't already triggered dialogue...
        if (triggersOnInteractButton && Input.GetKeyDown(KeyCode.E) && !dialogueRunning && playerWithinRange)
        {
            //Trigger the dialogue.
            RunBehavior();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //This is the player character, and this object is now interactable. 
            this.playerWithinRange = true;

            if (triggersOnProximity)
                RunBehavior();
        }
    }

    void OnClick()
    {
        if (triggersOnMouseClick)
            RunBehavior();
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //This is the player character leaving the space, and the object is now un-interactable.
            this.playerWithinRange = false;
        }
    }

    private void RunBehavior()
    {
        if (isSingleUse && hasBeenUsed)
            return;

        if (hasDialogue)
        {
            Debug.Log("Entering Yarn node " + yarnNode);

            if (!_dialogueRunner)
            {
                Debug.Log("No dialogue runner found for Interactible with yarn node " + yarnNode);
                return;
            }

            dialogueRunning = true;

            _dialogueRunner.StartDialogue(yarnNode);
        }
        else
        {
            EndRunBehavior();
        }
    }

    [YarnCommand("leap")]
    public void EndDialogue()
    {
        Debug.Log("Exiting dialogue for yarn node " + yarnNode);
        dialogueRunning = false;
        EndRunBehavior();
    }
    
    private void EndRunBehavior()
    {
        if (teleportPlayerAfterUse)
        {
            Vector3 destination = new Vector3(teleportX, teleportY, teleportZ);
            GameObject player = GameObject.FindWithTag("Player");

            Debug.Log("Teleporting player to " + destination);
            player.GetComponent<Rigidbody>().position = destination;
        }

        if (tagOfItemGivenOnUse != "")
        {
            Debug.Log("Giving player item with tag " + tagOfItemGivenOnUse);

            // Code to give player item, such as worm
        }

        hasBeenUsed = true;
    }
}
