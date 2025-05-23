using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class Pagesmanager_script : MonoBehaviour
{
    public bookmanager_script book;
    public Canvas canvas;
    public void openThisFuckingPage()
    {
        canvas.enabled = true;
    }

    public void closePageStupid() 
    {
        canvas.enabled = false; 
    }
}
