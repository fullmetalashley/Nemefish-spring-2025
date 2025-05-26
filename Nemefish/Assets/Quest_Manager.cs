using UnityEngine;
using System.Collections.Generic;
using Yarn.Unity;

public class Quest_Manager : MonoBehaviour
{
    private int day = 0;
    private bookmanager_script journal;
    public InMemoryVariableStorage variableStorage;
    bool Day1 = false;

    void Start()
    {

        //init journal 
        journal = GameObject.FindWithTag("Journal").GetComponent<bookmanager_script>();
        if (journal == null)
        {
            Debug.LogError("Journal Not Found Null Object");
        }

        //get var storage from yarn spinner
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();

        if (variableStorage == null)
        {
            Debug.Log("Variable Storage not found");
        }
    }
}
