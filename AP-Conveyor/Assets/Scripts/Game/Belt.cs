using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] private ConveyorPart conveyorPartPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float offset = -2.82f;
    [SerializeField] private int piecesCount = 6;
    private Queue<ConveyorPart> parts;

    private void Awake()
    {
        parts = new Queue<ConveyorPart>();

        for (int i = 0; i < piecesCount; i++)
        {
            ConveyorPart conveyorPart = Instantiate
            (
                conveyorPartPrefab,  
                transform
            );

            conveyorPart.transform.SetParent(transform);
            conveyorPart.transform.localPosition = new Vector3(0, offset * i, 0);
            parts.Enqueue(conveyorPart);
        }
    }

    private void FixedUpdate()
    {
        int i = 0;
        foreach (var part in parts)
        {
            part.transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);

            if (part.transform.localPosition.y <= offset * parts.Count)
            {
                part.transform.localPosition = Vector3.zero;
                if (i % 2 == 0)
                {
                    part.Create();
                }
            }
            i++;
        }
    }
}
