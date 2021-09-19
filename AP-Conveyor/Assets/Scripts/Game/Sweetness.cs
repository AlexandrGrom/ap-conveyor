using ManagerPooling;
using UnityEngine;

public class Sweetness : MonoBehaviour
{
    public void Reinitialize()
    {
        Debug.Log("reinitialized");
    }

    private void OnMouseDown()
    {
        PoolManager.BackToPool(gameObject, this.GetType());
    }
    
}
