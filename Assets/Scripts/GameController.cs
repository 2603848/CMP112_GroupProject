using JetBrains.Annotations;
using Unity.AI.Navigation.LowLevel;
using UnityEngine;
using UnityEngine.XR;

public class GameController
{
    public enum State
    {
        IN_MENU,
        IN_GAME
    }

    private static bool Paused = false;

    private static State _GameState = State.IN_GAME;

    private static int _WorldBoundsSize = 10000;
    public static int WorldBoundsSize {
        get { return _WorldBoundsSize; }
        set { _WorldBoundsSize = value; WorldBounds = new Bounds(new Vector3(), new Vector3(value, value, value)); }
    }
    public static Bounds WorldBounds = new Bounds(new Vector3(), new Vector3(_WorldBoundsSize, _WorldBoundsSize, _WorldBoundsSize));

    public static State GameState {
        get {
            return _GameState;
        }
        set
        {
            SetState(value);
        }
    }
    // Pauses the game. Pass true to pause, false to unpause
    private static void Pause(bool paused)
    {
        Paused = paused;
    }

    static void SetState(State to)
    {
        ChangeState(GameState, to);
    }

    private static void ChangeState(State from, State to)
    {
        // Implement menu triggering logic
        if (to == State.IN_MENU)
        {
            Pause(true);
            Cursor.lockState = CursorLockMode.Confined;
        }
        // Implement transitioning to game logic
        if (to == State.IN_GAME)
        {
            Pause(false);
            Cursor.lockState = CursorLockMode.Locked;
        }

        _GameState = to;
    }
}