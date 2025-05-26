using UnityEngine;
using Yarn.Unity;
using System.Collections;

public class add_page_test : MonoBehaviour
{
    public GameObject book;
    public InMemoryVariableStorage variableStorage;
    public Quest_Manager questLog;
    public bool testPageAdd = false;

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        variableStorage = GameObject.FindObjectOfType<InMemoryVariableStorage>();

        if (variableStorage == null)
        {
            Debug.Log("Variable Storage not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (testPageAdd)
        {
            variableStorage.SetValue("$LandedSafe", true);
            questLog.UpdateQuestVars();
            new WaitForSeconds(3);
            testPageAdd = false;
        }
    }
}
