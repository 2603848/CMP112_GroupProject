using JetBrains.Annotations;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public enum State
    {
        PAUSED,
        IN_MENU,
        IN_GAME
    }
    private State _GameState = State.IN_GAME;

    private static int _WorldBoundsSize = 10000;
    public static int WorldBoundsSize { 
        get { return _WorldBoundsSize; } 
        set { _WorldBoundsSize = value;  WorldBounds = new Bounds(new Vector3(), new Vector3(value, value, value)); } 
    }
    public static Bounds WorldBounds = new Bounds(new Vector3(),new Vector3(_WorldBoundsSize, _WorldBoundsSize, _WorldBoundsSize));
    
    public State GameState { 
        get {
            return GameState;
        }
        set
        {
            ChangeState(GameState, value);
        }
    }

    private void ChangeState(State from, State to)
    {
        // Implement pause logic
        if (to == State.PAUSED)
        {

        }
        // Implement menu triggering logic
        if (to == State.IN_MENU)
        {

        }
        // Implement transitioning to game logic
        if (to == State.IN_GAME)
        {

        }

        _GameState = to;
    }
}
