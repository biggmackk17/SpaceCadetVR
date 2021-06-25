using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{

    #region Singleton Setup
    private static GameStateManager _instance;
    public static GameStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The GameStateManager is Null");
            }
            return _instance;
        }
    }




    private void Awake()
    {
        _instance = this;
    }
    #endregion


    public enum GameState
    {
        Coffee,
        Takeoff,
        Warp1,
        Astroid,
        Warp2,
        Repair,
        Warp3,
        FreeFlight,
    }
    public GameState currentState;

    private void Start()
    {
        currentState = GameState.Coffee;
    }
}


