using UnityEngine;

public abstract class UIScreen : MonoBehaviour
{
    [SerializeField] protected GameState state;
    public GameState State => state;

    public abstract void EnablingAnimation();
    public abstract void DisablingAnimation();
}
