using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reactor : MonoBehaviour
{
    [SerializeField]
    bool _on;
    bool _connected;
    [SerializeField]
    bool _broken;
    Rigidbody _rb;
    Collider _collider;
  
 



    public bool BrokenCheck()
    {
        return _broken;
    }

  

   

   

    public void TurnOff()
    {
        _connected = false;
    }

    private void OnEnable()
    {
        //EventManager.OnPowerDown += BreakReactor;
        if (_broken)
        {
            _connected = true;
            StartCoroutine(ReactorFlash());
        }
        else

        transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");

    }
    

   

    IEnumerator ReactorFlash()
    {

        while (_connected)
        {
            transform.GetComponent<MeshRenderer>().material.EnableKeyword("_EMISSION");
            yield return new WaitForSeconds(Random.Range(0, .25f));
            transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION");
            yield return new WaitForSeconds(Random.Range(0, .25f));
        }
        transform.GetComponent<MeshRenderer>().material.DisableKeyword("_EMISSION"); 
    }


}
