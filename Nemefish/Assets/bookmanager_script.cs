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
    private int index = 0;


    public void Open()
    {
        Debug.Log("Open Triggered!");
        pages[index].SetActive(true);
    }
    
    void Close()
    {
        pages[index].SetActive(false);
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

    public void BackPage()
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

    void Start()
    {
    }
    // Update is called once per frame
    void Update()
    {

    }
}
