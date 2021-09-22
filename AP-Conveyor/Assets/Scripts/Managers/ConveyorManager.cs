using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private ReceivingBox[] boxes;

    private static SweetnessData[] sweetnessData;
    private static float maxrepeatTime = 3;
    private static float f = 1f / maxrepeatTime;
    private static float chanceOfPart = f;

    private void Awake()
    {
        List<SweetnessData> sweetnessDataArray = 
            Resources.LoadAll<SweetnessData>("Sweetness").ToList();

        sweetnessData = new SweetnessData[boxes.Length];
        for (int i = 0; i < boxes.Length; i++)
        {
            sweetnessData[i] = sweetnessDataArray[Random.Range(0, sweetnessDataArray.Count)];
            sweetnessDataArray.Remove(sweetnessData[i]);
            boxes[i].Initialize(sweetnessData[i]);
        }
    }


    public static SweetnessData GetRandomData()
    {
        float side = Random.Range(0f, 1f);
        if (side > chanceOfPart)
        {
            chanceOfPart += f;
            return sweetnessData[0];
        }

        chanceOfPart -= f;
        return sweetnessData[1];
    }
}
