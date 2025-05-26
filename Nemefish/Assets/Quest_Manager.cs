using UnityEngine;
using System.Collections.Generic;
using Yarn.Unity; 

public class Quest_Manager : MonoBehaviour
{
    private int day = 0;
    private bookmanager_script journal;
    public List<GameObject> pages = new List<GameObject> ();
    public InMemoryVariableStorage variableStorage;
    bool Day1 = false;


    public void GetPage(string pageName)
    {
        Debug.Log($"Get Page Triggered. Looking for {pageName}");
       
        for (int i = 0; i < pages.Count; i++) 
        {
            if (pages[i].name == pageName)
            {
                Debug.Log($"{pageName} found");
                journal.AddPage(pages[i]);
                break;
            }
        } 
    }

    void Start()
    {   //init journal 
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
        else
        {
            //init day pages in journal from yarn state
            if (variableStorage.TryGetValue ("$LandedSafe", out Day1) ){ GetPage ("Day1");}
        }
    }


    public void UpdateQuestVars()
    {
        variableStorage.TryGetValue("$LandedSafe", out Day1);
        if (Day1)
        {
            GetPage("Day1");
        }    
        
    }
}   
