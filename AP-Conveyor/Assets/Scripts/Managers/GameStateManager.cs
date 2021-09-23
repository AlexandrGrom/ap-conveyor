using System;

public class GameStateManager
{
	private static GameState currentState;

	public static Action<GameState> OnGameStateChange = delegate { };

	public static GameState CurrentState
	{
		get => currentState;

		set {
			if (currentState == value)
			{
				return;
			}

			currentState = value;

			OnGameStateChange.Invoke(currentState);
		}
	}
}
public enum GameState
{
	None,
	Menu,
	Game,
	LevelComplete,
	LevelFailed,
}
