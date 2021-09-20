using UnityEngine;


public class RecevingBox : MonoBehaviour
{
    [SerializeField] private ConveyorReciver conveyorReciver;
    [SerializeField] private Transform boxPosition;
    public Transform BoxPosition => boxPosition;


    private void OnMouseDown()
    {
        conveyorReciver.MoveReceivable(boxPosition.position);
        Debug.Log("hui");
    }
}
