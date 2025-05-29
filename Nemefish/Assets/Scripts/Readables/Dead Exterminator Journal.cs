using NUnit.Framework;
using UnityEngine;
using UnityEngine.UIElements;

public class DeadExterminatorJournal : MonoBehaviour
{
    [SerializeField] float pageSpeed = 0.5f;

    int index = -1;


    public void RotateNext()
    {
        index++;
        float angle = 180;

    }
}
