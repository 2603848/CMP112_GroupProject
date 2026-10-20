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
