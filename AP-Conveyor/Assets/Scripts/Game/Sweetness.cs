using ManagerPooling;
using UnityEngine;

public class Sweetness : MonoBehaviour, IReceivable
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer spriteOutline;
    private SweetnessType type;

    public Transform Transform => transform;

    public void Recive()
    {
        PoolManager.BackToPool(gameObject, this.GetType());
    }

    public void Reinitialize()
    {
        SweetnessData data = ConveyorManager.GetRandomData();
        sprite.sprite = data.sprite;
        spriteOutline.sprite = data.spriteOutline;
        spriteOutline.color = new Color(1,1,1,0);
        type = data.type;

        transform.localScale = Vector3.one;
    }
}
