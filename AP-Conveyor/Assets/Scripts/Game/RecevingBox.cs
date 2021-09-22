using UnityEngine;


public class RecevingBox : MonoBehaviour
{
    [SerializeField] private ConveyorReciver conveyorReciver;
    [SerializeField] private Transform boxPosition;

    [SerializeField] private SpriteRenderer typeSprite;
    private SweetnessType type;

    public Transform BoxPosition => boxPosition;

    public void Initialize(SweetnessData data)
    {
        typeSprite.sprite = data.spriteShadow;
        type = data.type;
    }

    private void OnMouseDown()
    {
        conveyorReciver.MoveReceivable(boxPosition.position);
    }
}
