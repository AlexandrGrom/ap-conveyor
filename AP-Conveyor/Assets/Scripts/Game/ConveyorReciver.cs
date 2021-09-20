using UnityEngine;
using DG.Tweening;

public class ConveyorReciver : MonoBehaviour
{
    [SerializeField] private RecevingBox[] recevingBoxes;
    private IReceivable receivable;
    private bool listen;

    public void MoveReceivable(Vector3 position)
    {
        if (receivable != null)
        {
            float duration = Vector3.Distance(position, receivable.Transform.position);
            receivable.Transform.DOMove(position, duration * 0.25f).OnComplete(()=>
            {
                receivable.Transform.DOScale(Vector3.zero, 0.2f).OnComplete(() => 
                {
                    receivable.Recive();
                    receivable = null;
                });
            });
        }
    }

    public void Swipe(float sign)
    {
        if (receivable != null)
        {
            sign = Mathf.Sign(sign);
            int index = ((int)sign + 1) / 2;
            MoveReceivable(recevingBoxes[index].BoxPosition.position);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var recivable = other.GetComponent<IReceivable>();
        if (recivable != null)
        {
            this.receivable = recivable;
        }
    }
}
