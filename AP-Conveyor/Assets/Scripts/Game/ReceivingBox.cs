using UnityEngine;

public class ReceivingBox : ReceivingObject
{
    [SerializeField] private Transform boxPosition;
    [SerializeField] private Transform outsidePosition;
    [SerializeField] private SpriteRenderer typeSprite;

    public Vector3 BoxPosition => boxPosition.position;
    public Vector3 OutsidePosition => outsidePosition.position;

    public void Initialize(SweetnessData data)
    {
        typeSprite.sprite = data.spriteShadow;
        Type = data.type;
    }

    private void OnMouseDown()
    {
        ConveyorReceiver.OnSortElement(this);
    }
}
