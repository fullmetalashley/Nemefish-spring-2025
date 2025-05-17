using UnityEngine;

public class SpriteBillboard : MonoBehaviour
{
    private SpriteRenderer theSR;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(PlayerController.instance.transform.position);
    }
}
