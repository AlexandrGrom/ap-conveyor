using UnityEngine;


public class ConveyorEnd : ReceivingObject
{
    private void OnTriggerEnter(Collider other)
    {
        var recivable = other.GetComponent<Sweetness>();
        if (recivable != null)
        {
            //recivable
            //Debug.Log("end");
        }
    }
}
