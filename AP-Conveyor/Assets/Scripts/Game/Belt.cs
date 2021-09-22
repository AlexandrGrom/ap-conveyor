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
            conveyorPart.gameObject.name = i.ToString();
            conveyorPart.transform.localPosition = new Vector3(0, offset * (piecesCount-i-1), 0);
            parts.Enqueue(conveyorPart);
        }
    }

    int startvalue = 0;
    private void FixedUpdate()
    {
        foreach (var part in parts)
        {
            part.transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);
        }
        int i = startvalue;
        foreach (var part in parts)
        {
            if (part.transform.localPosition.y <= offset * parts.Count)
            {
                part.transform.localPosition = Vector3.zero;

                parts.Enqueue(parts.Dequeue());
                startvalue++;
                part.Reinitialize();
                if (i % 2 == 0)
                {
                   part.Create();
                }
                break;

            }
            i++;
        }


    }
}
