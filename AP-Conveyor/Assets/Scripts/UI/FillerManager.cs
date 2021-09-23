using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class FillerManager : MonoBehaviour
{
    [SerializeField] private UIProgressBar uiProgressBar;
    [SerializeField] private TextMeshProUGUI incomeText;
    [SerializeField] private TextMeshProUGUI current;
    [SerializeField] private RectTransform textRect;
    private int counter = 0;

    public static Action<Vector2> OnIncrementCount;
    public static int CurentAmount { get; private set; }

    private void Awake()
    {
        CurentAmount = 0;
        OnIncrementCount += IncrementCount;
        UpdateData();
    }


    private void IncrementCount(Vector2 position)
    {
        CurentAmount += ConveyorManager.gamePlaySettings.income;
        if (CurentAmount >= ConveyorManager.gamePlaySettings.maxCount)
        {
            GameStateManager.CurrentState = GameState.LevelComplete;
        }
        UpdateData();

        counter++;
        if (counter == ConveyorManager.gamePlaySettings.itemsBeforeIncrement)
        {
            Belt.IncrementSpeed();
            counter = 0;
        }

        incomeText.DOFade(1, 0f);
        textRect.transform.position = Camera.main.WorldToScreenPoint(position);

        incomeText.text = $"+ {ConveyorManager.gamePlaySettings.income}";
        textRect.DOAnchorPos(textRect.anchoredPosition + Vector2.up * 70, 0.5f);
        incomeText.DOFade(0, 0.5f);
    }

    private void UpdateData()
    {
        current.text = $"{CurentAmount}/{ConveyorManager.gamePlaySettings.maxCount}";

        uiProgressBar.FillAmount = (float)CurentAmount
            / ConveyorManager.gamePlaySettings.maxCount;

        if (CurentAmount >= ConveyorManager.gamePlaySettings.maxCount)
        {
            GameStateManager.CurrentState = GameState.LevelComplete;
        }
    }

    private void OnDestroy()
    {
        OnIncrementCount -= IncrementCount;
    }
}
