using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Scriptable Objects/GameEvent")]
public class GameEvent : ScriptableObject
{
    List<GameEventListener> listeners =
        new List<GameEventListener>();

    public void TiggerEvent()
    {
        foreach (GameEventListener l in listeners)
        {
            l.OnEventTriggered();
        }
    }

    public void AddListener(GameEventListener l)
    {
        listeners.Add(l);
    }

    public void RemoveListener(GameEventListener l)
    {
        listeners.Remove(l);
    }
}
