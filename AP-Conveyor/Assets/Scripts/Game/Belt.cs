using UnityEngine;

public class Belt : MonoBehaviour
{
    private ConveyorPart[] parts;
    [SerializeField] private ConveyorPart conveyorPartPrefab;
    [SerializeField] private float speed;
    [SerializeField] private float offset = -2.82f;

    private void Awake()
    {
        parts = new ConveyorPart[6];

        for (int i = 0; i < parts.Length; i++)
        {
            ConveyorPart conveyorPart = Instantiate
            (
                conveyorPartPrefab,  
                transform
            );

            conveyorPart.transform.SetParent(transform);
            conveyorPart.transform.localPosition = new Vector3(0, offset * i, 0);
            parts[i] = conveyorPart;
            if (i % 2 == 0)
            {
                conveyorPart.Reinitialize();
            }
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < parts.Length; i++)
        {
            ConveyorPart part = parts[i];
            part.transform.Translate(Vector3.down * Time.fixedDeltaTime * speed);

            if (part.transform.localPosition.y <= offset * parts.Length)
            {
                part.transform.localPosition = Vector3.zero;
                if (i%2 ==0)
                {
                    part.Reinitialize();
                }
            }
        }
    }
}
