using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeButton : Switches
{
    [SerializeField]
    AudioClip _coffeeClip;
    [SerializeField]
    Transform[] _rayOrigin;
    [SerializeField]
    GameObject _coffeeBall;
    [SerializeField]
    RaycastHit hit;
    bool _firstPress;

    bool _potDetected;


    protected override void OnEnable()
    {
        base.OnEnable();
        EventManager.OnGameStart += BeginCoffeeSequence;
    }
    protected override void OnDisable()
    {
        base.OnDisable();
        EventManager.OnGameStart -= BeginCoffeeSequence;
    }

    protected override void Start()
    {
        base.Start();
        _interactable = false;
        SetEmissionColor(Color.black);
        _firstPress = true;
       
    }

    public void BeginCoffeeSequence()
    {
        StartCoroutine(CoffeeSequence());
    }

    IEnumerator CoffeeSequence()
    {
        AudioManager.Instance.PlayDialogue(0, 1f);
        while (AudioManager.Instance.DialogueCheck())
        {
          yield  return null;
        }
        EnableButton();


    }


    protected override void OnLogic()
    {
        base.OnLogic();
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.red);
        _interactable = false;
        Physics.Raycast(_rayOrigin[0].position, Vector3.down, out hit,1);
        if (hit.collider != null)
        {

            CoffeePot pot = hit.collider.gameObject.GetComponent<CoffeePot>();
            if (pot != null)
            {

                hit.collider.enabled = false;
                _potDetected = true;
                pot.Refill();
                StartCoroutine("Pouring");
            }

            else
            {
                _potDetected = false;
                StartCoroutine("Pouring");
            }
        }
        else
        {
            _potDetected = false;
            StartCoroutine("Pouring");
        }

        if (_firstPress == true)
        {
            _firstPress = false;
            StartCoroutine(CoffeeTimeDelay());
          
           
        }
    }
    private IEnumerator Pouring()
    {
        AudioManager.Instance.PlayEffect(_coffeeClip,.5f);//play coffee noise
        if (!_potDetected) //if there pot is there spawn 20 coffee balls over the lengeth of the noise
        {
            var i = 0;
            while (i < 20)
            {

                var ball = Instantiate(_coffeeBall, _rayOrigin[Random.Range(0,2)].position, Quaternion.identity);
                ball.GetComponent<Rigidbody>().AddForce(Vector3.down * Random.Range(0, 2f));
                yield return new WaitForSeconds(_coffeeClip.length/20);
                i++;
            }
        }
        else // otherwise wait until nois finishes and turn collider back on 
        {
            yield return new WaitForSeconds(_coffeeClip.length);
            hit.collider.enabled = true;

        }
        OffLogic();
        _on = false;
        _interactable = true; //make button pressable again
        transform.GetComponent<MeshRenderer>().materials[0].SetColor("_EmissionColor", Color.green);
    }

    IEnumerator CoffeeTimeDelay()
    {
        
            yield return new WaitForSeconds(10f);
        GameStateManager.Instance.currentState = GameStateManager.GameState.Takeoff;
        AudioManager.Instance.PlayDialogue(1, 1f);
        while (AudioManager.Instance.GetDialogueTime() <9f)
        {
           
            yield return new WaitForSeconds(.25f);
        }
        EventManager.Instance.BootUpSequence();


    
        
    }

    //voids out normal delay behavior;
    protected override IEnumerator SwitchDelay()
    {
        yield return null;
    }
}
