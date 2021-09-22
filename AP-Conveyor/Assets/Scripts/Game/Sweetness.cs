using ManagerPooling;
using UnityEngine;

public class Sweetness : MonoBehaviour, IReceivable
{
    public Transform Transform => transform;

    public void Recive()
    {
        PoolManager.BackToPool(gameObject, this.GetType());
    }

    public void Reinitialize()
    {
        transform.localScale = Vector3.one;
    }
}
