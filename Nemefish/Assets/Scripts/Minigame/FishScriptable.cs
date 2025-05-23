using UnityEngine;

[CreateAssetMenu(fileName = "New Fish", menuName = "Fish")]
public class FishScriptable : ScriptableObject
{
    public new string name;
    public string _fishType;
    public int _damageDealt;
    public int _health;
    public Item _successItem;

    public float _tugStrength;

    public string _behavior;    //TODO: Replace this with a set of speeds, and a set of positions, for when the fish scatters after a cast
    public float _swimSpeed;
}
