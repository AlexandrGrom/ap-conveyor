using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class UIManager : MonoBehaviour
{
    protected Dictionary<GameState, UIScreen> Screens;

    protected void Awake()
    {
        List<UIScreen> screensList = GetComponentsInChildren<UIScreen>(true).ToList();
        Screens = screensList.ToDictionary(screen => screen.State, screen => screen);

        GameStateManager.OnGameStateChange += HandleGameStateChange;

        GameStateManager.CurrentState = GameState.Menu;
    }

    protected void HandleGameStateChange(GameState uiState)
    {
        if (Screens.ContainsKey(uiState))
        {
            foreach (KeyValuePair<GameState, UIScreen> screen in Screens)
            {
                if (uiState == screen.Key)
                {
                    Screens[uiState].transform.SetSiblingIndex(transform.childCount - 1);
                    Screens[uiState].EnablingAnimation();
                }
                else
                {
                    screen.Value.DisablingAnimation();
                }
            }
        }
    }

    protected void OnDestroy()
    {
        GameStateManager.OnGameStateChange -= HandleGameStateChange;
    }
}
