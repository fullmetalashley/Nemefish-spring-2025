using UnityEngine;

public class Interaction : MonoBehaviour
{
    private UIManager _uiManager;

    void Start()
    {
        _uiManager = FindAnyObjectByType<UIManager>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().SetInteractionIcon(true);
            other.GetComponent<PlayerController>().withinInteractionSpace = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().SetInteractionIcon(false);
            other.GetComponent<PlayerController>().withinInteractionSpace = false;
        }
    }
}
