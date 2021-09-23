using UnityEngine;

public class ReceivingObject : MonoBehaviour
{
    [SerializeField] protected Transform dropPosition;
    public Vector3 DropPosition => dropPosition.position;


    public SweetnessType Type { get; protected set; }

}