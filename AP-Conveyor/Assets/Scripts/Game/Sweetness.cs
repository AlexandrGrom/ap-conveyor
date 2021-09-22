using ManagerPooling;
using UnityEngine;
using DG.Tweening;

public class Sweetness : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private SpriteRenderer spriteOutline;
    private SweetnessType type;
    public SweetnessType Type => type;

    public Transform Transform => transform;

    public void EnableOutline()
    {
        spriteOutline.DOFade(1, 0.1f);
    }

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
