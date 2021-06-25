using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventManager : MonoBehaviour
{
    #region Singleton Setup
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.Log("The EventManager is Null");
            }
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
    }
    #endregion

    #region Events
    public delegate void GameStart();
    public static event GameStart OnGameStart;

    public delegate void ShipBootState();
    public static event ShipBootState OnBootState;

    public delegate void ShipPowerDown();
    public static event ShipPowerDown OnPowerDown;

    public delegate void ShipPowerUp();
    public static event ShipPowerUp OnPowerUp;


    public delegate void EnableTakeOff();
    public static event EnableTakeOff OnEnableTakeOff;

    public delegate void ReactorInstall();
    public static event ReactorInstall OnReactorInstall;

    public delegate void ReactorRemoval();
    public static event ReactorRemoval OnReactorRemoval;

    public delegate void ReactorPowerOn();
    public static event ReactorPowerOn OnReactorPowerOn; 

    public delegate void EnableWarp();
    public static event EnableWarp OnEnableWarp;

    public delegate void DisableAutoPilot();
    public static event DisableAutoPilot OnDisableAutoPilot;

    public delegate void EnableSeatRotate();
    public static event EnableSeatRotate OnEnableSeatRotate;
    #endregion

    bool _booted = false;
    bool _allBootSwitchesOn;
    int SwitchCount;




    public void FreeFly()
    {
        OnDisableAutoPilot();
    }

    private void Start()
    {
        StartCoroutine(GameStartDelay());
    }

    IEnumerator GameStartDelay()
    {
        yield return new WaitForSeconds(3);
        OnGameStart();
    }

    public void BootUpSequence()
    {

        if (OnBootState != null)
        {
            OnBootState();
        }
        SwitchCount = 0;
        StartCoroutine(BootExtraAudio());


    } // enables boot switches

    IEnumerator BootExtraAudio()
    {
        yield return new WaitForSeconds(10f);
        if (!_booted)
        {
            
            AudioManager.Instance.PlayDialogue(2, 1f);
        }


    }

    public void CountButtonPress()
    {
        SwitchCount += 1;
       
        

            if (GameStateManager.Instance.currentState == GameStateManager.GameState.Takeoff)
            {
            if (SwitchCount >= 16)
            {
                StartCoroutine(PreTakeoffDialogue());
                SwitchCount = 0;
                _booted = true;
            }
            

            }

        if (GameStateManager.Instance.currentState == GameStateManager.GameState.Repair)
        {
            if (SwitchCount >= 4)
            {
                //restart engine, enable warp
                //power up event;
                EnableWarpButton();
                OnReactorPowerOn();
                AudioManager.Instance.PlayDialogue(13, 1);
                SwitchCount = 0;
            }
        }


        


    }// tracks how many boot switches have been pressed

    IEnumerator PreTakeoffDialogue()
    {
        AudioManager.Instance.PlayDialogue(3, 1f);
        while (AudioManager.Instance.DialogueCheck())
        {
            yield return new WaitForSeconds (.25f);
        }
        OnEnableTakeOff();
    }

    public void EnableWarpButton()
    {

        if (OnEnableWarp != null)
        {

            OnEnableWarp();
            
          
        }
      

    }

    public void PowerDown()
    {
        if (OnPowerDown != null)
        {
            OnPowerDown();
        }
    
    }

    public void PowerUp()
    {
        if (OnPowerUp != null)
        {
            OnPowerUp();
        }
      
    }

    public void ReactorInstalled()
    {
        OnReactorInstall();
       
    }

    public void ReactorRemoved()
    {
        OnReactorRemoval();
    }
   
    public void EnableSeatButton()
    {
        if (OnEnableSeatRotate != null)
        {
            OnEnableSeatRotate();
        }
    }

    public void PressAllStartUp()
    {
        BootUpButton[] bootbuttons = FindObjectsOfType<BootUpButton>();
        foreach (BootUpButton button in bootbuttons)

        {
            button.Switch();
        }
    }
    [SerializeField]
    Animator _fadeAnim;
    public void EndGame()
    {
        _fadeAnim.SetTrigger("FadeOut");
        StartCoroutine(EndDelay());
    }
    IEnumerator EndDelay()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("EndCredits");

    }
    public void SkipToWarp1()
    {
        AudioManager.Instance.StopDialogue();
        GameStateManager.Instance.currentState = GameStateManager.GameState.Warp1;
        TimeLineManager.Instance.Warp();
    }
    public void SkipToWarp2()
    {
        AudioManager.Instance.StopDialogue();
        GameStateManager.Instance.currentState = GameStateManager.GameState.Warp2;
        TimeLineManager.Instance.Warp();
    }
    public void SkipToWarp3()
    {
        AudioManager.Instance.StopDialogue();
        GameStateManager.Instance.currentState = GameStateManager.GameState.Warp3;
        TimeLineManager.Instance.Warp();
    }
}
