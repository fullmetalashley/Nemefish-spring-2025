using Unity.VisualScripting;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;



public class bookmanager_script : MonoBehaviour
{
    private int curr_page = 0;
    public Button backBtn, fwdBtn;
    public List<Pagesmanager_script> pages;
    private bool is_Open = false;
    UnityEvent<bool> bookOpen;
    UnityEvent<int>  pageOpen, pageClose;
    public Pagesmanager_script testPage;
    
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        curr_page = 0; //set to first page on load 
        pages.Add(testPage);
    }

    void Open()
    {
        curr_page = 0;
        enabled = true;
        is_Open = true;
        OpenPage(curr_page);
        bookOpen.Invoke(true);
    }
    
    void Close()
    {
        is_Open = false;
        pages[curr_page].closePageStupid();
    }



    public void OpenPage(int page_offset)
    {
        if (is_Open)
        {
            try
            {
                pages[curr_page + page_offset].openThisFuckingPage();
                pages[curr_page].closePageStupid();
                curr_page += page_offset;
            }
            catch
            {
                //do nothing bc this code is bad
            }
        }
    } 
 

    // Update is called once per frame
    void Update()
    {
        if (!is_Open)
        {
            enabled = false;
        }

    }
}
