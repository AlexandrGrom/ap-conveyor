using System;
using UnityEngine;

public static class SaveManager
{
    public static void SaveIntoJson(int newScore)
    {
        SaveData saveData = new SaveData();
        SaveData oldData = LoadData();

        for (int i = 0; i < saveData.score.Length; i++)
        {
            saveData.score[i] = oldData.score[i];
            saveData.datetime[i] = oldData.datetime[i];
        }
        for (int i = 0; i < saveData.score.Length; i++)
        {
            if (oldData.score[i] < newScore)
            {
                saveData.score[i] = newScore;
                saveData.datetime[i] = DateTime.Now.ToString();
                for (int j = i+1; j < saveData.score.Length-1; j++)
                {
                    saveData.score[j] = oldData.score[j-1];
                    saveData.datetime[j] = oldData.datetime[j-1];
                }
                break;
            }
        } 

        string score = JsonUtility.ToJson(saveData);

        System.IO.File.WriteAllText(Application.persistentDataPath + "/score.json", score);
    }

    public static SaveData LoadData()
    {
        if (System.IO.File.Exists(Application.persistentDataPath + "/score.json"))
        {
            string text = System.IO.File.ReadAllText(Application.persistentDataPath + "/score.json");
            return JsonUtility.FromJson<SaveData>(text);
        }
        SaveData data = new SaveData();
        for (int i = 0; i < 5; i++)
        {
            data.score[i] = -1;
            data.datetime[i] = DateTime.Now.ToString();
        }
        return data;
    }

    public class SaveData
    {
        public int[] score = new int[5];
        public string[] datetime = new string[5];
    }
}
