using UnityEngine;

public class AmbienceChangeTrigger : MonoBehaviour
{
    [Header("Parameter Change")]
    [SerializeField] private string parameterName;
    [SerializeField] private float parameterValue;

    private void OgerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            AudioManager.instance.SetAmbienceParameter(parameterName, parameterValue);
        }
    }
}
