using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLineManager : MonoBehaviour
{
    private static TimeLineManager _instance;
    public static TimeLineManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The TimeLineManager is Null");
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }


    [SerializeField]
    GameObject _takeOff;
    [SerializeField]
    GameObject _fleet;
    [SerializeField]
    GameObject _warp;
    [SerializeField]
    GameObject _astroidFieldTL;
    [SerializeField]
    GameObject _astroidFieldGO;
    [SerializeField]
    GameObject _Repair;

    private void Start()
    {
        _fleet.SetActive(true);
        _astroidFieldGO.SetActive(false);
    }

    public void TakeOff()
    {
        _takeOff.SetActive(true);
        AudioManager.Instance.PlayMusic(1);
        GameStateManager.Instance.currentState = GameStateManager.GameState.Warp1;
    }
    public void Warp()
    {
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp1)
        {
            _fleet.SetActive(false);
            _warp.SetActive(true);
        }
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp2)
        {
            _astroidFieldGO.SetActive(false);
            _warp.SetActive(false);
            _warp.SetActive(true);
            


        }

        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp3)
        {
            _warp.SetActive(false);
            _warp.SetActive(true);
           
          


        }




    }


    public void Repair()
    {
        _Repair.SetActive(true);
    }

    [SerializeField]
    Transform _shipRotation;
    [SerializeField]
    Transform _cockpit;
    [SerializeField]
    Transform _freeflightStart;
    public void WarpFinish()
    {
        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp1)
        {
            // AudioManager.Instance.PlayMusic(2);
            AudioManager.Instance.StopMusic();
            _astroidFieldGO.SetActive(true);
            _astroidFieldTL.SetActive(true);
            GameStateManager.Instance.currentState = GameStateManager.GameState.Astroid;
        }

        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp2)
        {
           
            GameStateManager.Instance.currentState = GameStateManager.GameState.Repair;
            AudioManager.Instance.StopMusic();
            _astroidFieldTL.SetActive(false);
            Repair();
            AudioManager.Instance.StopMusic();
          
        }

        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Warp3)
        {

           
            GameStateManager.Instance.currentState = GameStateManager.GameState.FreeFlight;
            _cockpit.position = _freeflightStart.position;
            _cockpit.rotation = _freeflightStart.rotation;
            _shipRotation.rotation = _freeflightStart.rotation;
            AudioManager.Instance.PlayMusic(3);
            _fleet.SetActive(true);
            AudioManager.Instance.PlayDialogue(14, 1);
            EventManager.Instance.FreeFly();//disables autopilot 
            
            
            //tell event manager to release auto pilot. 

        }

    }


    public void OnAstroidFinish()
    {
        GameStateManager.Instance.currentState = GameStateManager.GameState.Warp2;
        _warp.SetActive(false);
        EventManager.Instance.EnableWarpButton();
    }


    
}
