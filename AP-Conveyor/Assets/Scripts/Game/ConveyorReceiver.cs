using UnityEngine;
using DG.Tweening;
using System;
using ManagerPooling;

public class ConveyorReceiver : MonoBehaviour
{
    public static Action<ReceivingBox> OnSortElement; 

    [SerializeField] private ReceivingBox[] recevingBoxes;
    [SerializeField] private float movingSpeedModifier = 0.2f;
    [SerializeField] private float scalingSpeed = 0.2f;

    private Sweetness receivable;

    private void Awake()
    {
        OnSortElement += SortElement;
    }

    private void SortElement(ReceivingBox recevingBox)
    {
        if (receivable == null) return;

        if (recevingBox.Type == receivable.Type)
        {
            //Debug.Log("increment");
            MoveReceivable(recevingBox.BoxPosition);
        }
        else
        {
            //Debug.Log("lose");
            MoveReceivable(recevingBox.DropPosition);
        }
    }

    private void OnDestroy()
    {
        OnSortElement -= SortElement;
    }

    private void MoveReceivable(Vector3 position)
    {
        if (receivable != null)
        {
            Sweetness receivableClone = receivable;
            float duration = Vector3.Distance(position, receivableClone.Transform.position);
            receivableClone.Transform.DOMove(position, duration * movingSpeedModifier).OnComplete(()=>
            {
                receivableClone.Transform.DOScale(Vector3.zero, scalingSpeed).OnComplete(() => 
                {
                    PoolManager.BackToPool(receivableClone.gameObject, receivableClone.GetType());
                    receivableClone.Recive();
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
            OnSortElement.Invoke(recevingBoxes[index]);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var recivable = other.GetComponent<Sweetness>();
        if (recivable != null)
        {

            this.receivable = recivable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        var recivable = other.GetComponent<Sweetness>();
        if (recivable != null)
        {
            this.receivable = null;
        }
    }
}
