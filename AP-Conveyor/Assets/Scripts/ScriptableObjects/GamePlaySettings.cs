using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
public class GamePlaySettings : ScriptableObject
{
    public float conveyorSpeed = 1;
    public float multiplyer = 1.2f;
    public int maxCount = 100;
    public int itemsBeforeIncrement = 5;
    public int income = 10;
}
