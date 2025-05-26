using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using NUnit.Framework;
using UnityEditor;



public class bookmanager_script : MonoBehaviour
{

    public List<GameObject> activePages = new ();
    public List<GameObject> btns = new ();
    public List<GameObject> hiddenPages = new ();
    private int index = 0;

    public void Open()
    {
        Debug.Log("Open Triggered!");
        activePages[index].SetActive(true);
        foreach (GameObject b in btns)
        {
            b.SetActive(true);
        }
    }
    
    public void CloseAll()
    {
       foreach (GameObject page in activePages)
       {
            page.SetActive(false);
       }
       foreach (GameObject b in btns)
        {
            b.gameObject.SetActive(false);
        }
        index = 0;
    }

    void ClosePrev(int prev_i)
    {
        activePages[prev_i].SetActive(false);
    }

    public void NextPage()
    {
        int prev_i = index;
        try
        {  
            index++;
            Open();
            ClosePrev(prev_i);
        }
        catch
        {
            Debug.Log("End of Book");
            index = prev_i;
        }
    }



    public void PreviousPage()
    {
        int prev_i = index;
        try
        {
            index--;
            Open();
            ClosePrev(prev_i);
        }
        catch
        {
            Debug.Log("Start");
            index = prev_i;
        }
    }

    public void AddPage(string pageName)
    {
        Debug.Log($"Get Page Triggered. Looking for {pageName}");

 
        for (int i = 0; i < hiddenPages.Count; i++)
        {
            if (hiddenPages[i].name == pageName)
            {
                Debug.Log($"{pageName} found");
                activePages.Add(hiddenPages[i]);
                break;
            }
        }
        Debug.Log($"{pageName} added");
    }

}
