using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Belt : MonoBehaviour
{
    [SerializeField] private ConveyorPart conveyorPartPrefab;
    [SerializeField] private float offset = -2.82f;
    [SerializeField] private int piecesCount = 6;

    private Queue<ConveyorPart> parts;
    private static float speed;

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
            parts.Enqueue(conveyorPart);
        }
        SetTilePositions();
        speed = ConveyorManager.gamePlaySettings.conveyorSpeed;

        GameStateManager.OnGameStateChange += HandleState;
    }

    public static void IncrementSpeed()
    {
        speed *= ConveyorManager.gamePlaySettings.multiplyer;
    }

    private void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= HandleState;
    }

    private void HandleState(GameState state)
    {
        if (state == GameState.Game)
        {
            StartCoroutine(nameof(UpdateData));
        }
        else
        {
            StopCoroutine(nameof(UpdateData));
        }
    }

    int startvalue = 0;
    private IEnumerator UpdateData()
    {
        while (true)
        {
            yield return null;
            foreach (var part in parts)
            {
                part.transform.Translate(Vector3.down * Time.deltaTime * speed);
            }
            int i = startvalue;
            foreach (var part in parts)
            {
                if (part.transform.localPosition.y <= offset * parts.Count)
                {

                    parts.Enqueue(parts.Dequeue());
                    startvalue++;
                    part.Reinitialize();
                    if (i % 2 == 0)
                    {
                        part.Create();
                    }
                    SetTilePositions();
                    break;

                }
                i++;
            }
        }

    }

    private void SetTilePositions()
    {
        int i = 0;
        foreach (var part in parts)
        {
            part.transform.localPosition = new Vector3(0, offset * (piecesCount - i - 1), 0);
            i++;
        }
    }
}
