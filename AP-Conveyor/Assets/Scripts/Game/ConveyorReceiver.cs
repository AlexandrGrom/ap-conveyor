using UnityEngine;
using DG.Tweening;
using System;
using ManagerPooling;

public class ConveyorReceiver : MonoBehaviour
{
    public static Action<ReceivingObject> OnSortElement; 

    [SerializeField] private ReceivingBox[] recevingBoxes;
    [SerializeField] private float movingSpeedModifier = 0.2f;
    [SerializeField] private float scalingSpeed = 0.2f;

    private Sweetness receivable;

    private void Awake()
    {
        OnSortElement += SortElement;
    }

    private void SortElement(ReceivingObject recevingBox)
    {
        if (receivable == null) return;


        float duration = Vector3.Distance(recevingBox.DropPosition, receivable.Transform.position);
        Sweetness receivableClone = receivable;
        if (recevingBox.Type != receivableClone.Type)
        {
            GameStateManager.CurrentState = GameState.LevelFailed;
        }
        receivableClone.Transform.DOMove(recevingBox.DropPosition, duration * movingSpeedModifier).OnComplete(() =>
        {
            if (recevingBox.Type == receivableClone.Type)
            {
                receivableClone.Transform.DOScale(Vector3.zero, scalingSpeed).OnComplete(() =>
                {
                    FillerManager.OnIncrementCount.Invoke(receivableClone.Transform.position);
                    PoolManager.BackToPool(receivableClone.gameObject, receivableClone.GetType());
                    receivableClone.Recive();
                    receivable = null;
                });
            }
            else
            {
                if (recevingBox is ConveyorEnd)
                {
                    return;
                }
                Vector2 position = (recevingBox as ReceivingBox).OutsidePosition;
                receivableClone.Transform.DOJump(position, 2f, 1, scalingSpeed).OnComplete(() =>
                {
                    PoolManager.BackToPool(receivableClone.gameObject, receivableClone.GetType());
                    receivableClone.Recive();
                    receivable = null;
                });
            }
        });
    }

    private void OnDestroy()
    {
        OnSortElement -= SortElement;
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
            recivable.EnableOutline();
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
