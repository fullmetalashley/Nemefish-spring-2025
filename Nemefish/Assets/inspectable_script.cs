using UnityEngine;

public class inspectable_script : MonoBehaviour
{
    public GameObject inspectable;

    public void OpenInspectable()
    {
        inspectable.SetActive(true);
    }

    public void CloseInspectable()
    {
        inspectable.SetActive(false);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
