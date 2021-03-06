using UnityEngine;

[CreateAssetMenu(fileName = "Sweetness", menuName = "SweetnessData")]
public class SweetnessData : ScriptableObject
{
    public SweetnessType type;
    public Sprite sprite;
    public Sprite spriteShadow;
    public Sprite spriteOutline;
}

public enum SweetnessType
{
    none,
    ear,
    cake,
    heart,
    pretzel,
    orange,
    star,
    jelleyCake,
    pudding,
}
