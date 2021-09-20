using ManagerPooling;
using UnityEngine;

public class ConveyorPart : MonoBehaviour
{
    [SerializeField] private Sweetness sweetnessPrefab;
    private Sweetness sweetness;

    public void Create()
    {
        sweetness = PoolManager.GetPooledObject(sweetnessPrefab, sweetnessPrefab.GetType())
            .GetComponent<Sweetness>();
        sweetness.transform.parent = transform;
        sweetness.transform.localPosition = Vector3.back;
        sweetness.Reinitialize();
    }

    public void Reinitialize()
    {

    }
}
