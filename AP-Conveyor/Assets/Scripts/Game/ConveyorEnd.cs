using UnityEngine;


public class ConveyorEnd : ReceivingObject
{
    private void Awake()
    {
        Type = SweetnessType.none;
    }
    private void OnTriggerEnter(Collider other)
    {
        var recivable = other.GetComponent<Sweetness>();
        if (recivable != null)
        {
            ConveyorReceiver.OnSortElement.Invoke(this);
        }
    }
}
