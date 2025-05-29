using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Pagesmanager_script : MonoBehaviour
{
    public bookmanager_script book;
    public Canvas canvas;
    public bool open_test = false;

    public void openThisPage()
    {
        enabled = true;
        canvas.enabled = true;
    }

    public void closeThisPage() 
    {
        enabled = false;
        canvas.enabled = false; 
    }

    public void Update()
    {
        if (open_test)
        {
            enabled = open_test;
            openThisPage();
        }
    }
}
