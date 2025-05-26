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

    public List<GameObject> pages = new List<GameObject>();
    public List<GameObject> btns = new List<GameObject>();
    private int index = 0;

    public void Open()
    {
        Debug.Log("Open Triggered!");
        pages[index].SetActive(true);
        foreach (GameObject b in btns)
        {
            b.SetActive(true);
        }
    }
    
    public void CloseAll()
    {
       foreach (GameObject page in pages)
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
        pages[prev_i].SetActive(false);
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

    public void AddPage(GameObject newPage)
    {
        Debug.Log($"{newPage.name} added");
        pages.Add(newPage);
    }

}
