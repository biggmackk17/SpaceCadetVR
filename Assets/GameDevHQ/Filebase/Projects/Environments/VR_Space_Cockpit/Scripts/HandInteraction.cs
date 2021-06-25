using System.Collections; 
using System.Collections.Generic;
using UnityEngine;

public class HandInteraction : MonoBehaviour
{

    private float indexTrigger;
    private bool indexTouch;
    private float gripTrigger;
    private float gripTouch;
    float RightTriggerFloat;
    public enum HandState
    {
        open, 
        grab, 
        point, 
        pinch, 
       
    }
    public enum Hand
    {
        left,
        right,
    }
    [SerializeField]
    private Hand _hand;
    [SerializeField]
    private GameObject _handMeshGO;
    private SkinnedMeshRenderer _handMesh;
   
  

    public HandState handState;


    private void Start()
    {
        
        _handMesh = _handMeshGO.GetComponent<SkinnedMeshRenderer>();
    }
    void Update()
    {
        InputUpdate();
        StateChange();





    }


    void InputUpdate()
    {
        if (_hand == Hand.left)
        {
            gripTrigger = (OVRInput.Get(OVRInput.RawAxis1D.LHandTrigger));
            indexTrigger = (OVRInput.Get(OVRInput.RawAxis1D.LIndexTrigger));
            indexTouch = (OVRInput.Get(OVRInput.RawTouch.LIndexTrigger));

        }

        if (_hand == Hand.right)
        {
            gripTrigger = (OVRInput.Get(OVRInput.RawAxis1D.RHandTrigger));
            indexTrigger = (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger));
            indexTouch = (OVRInput.Get(OVRInput.RawTouch.RIndexTrigger));
          
        }
    }
    void StateChange()
    {

        if (gripTrigger < .5 && indexTrigger < .5f)
        {
            handState = HandState.open;
         //   Debug.Log(_hand + " STATE: " + handState);

        }

        else if (gripTrigger > .5 && indexTouch == true)
        {
            handState = HandState.grab;
           // Debug.Log(_hand + " STATE: " + handState);

        }

        else if (indexTouch == false)
        {
            handState = HandState.point;
          //  Debug.Log(_hand + " STATE: " + handState);
        }

        else if (gripTrigger < .5f && indexTrigger > .5f)
        {
            handState = HandState.pinch;
           // Debug.Log(_hand + " STATE: " + handState);
        }

        else handState = HandState.open;

    }
    private void OnTriggerEnter(Collider other)
    {
       
        var switchable = other.GetComponent<Switches>();

        if (switchable != null && handState == HandState.point)
        {
          
            switchable.Switch();
        }
    }

    public void ShowHand(bool visible)
    {
        _handMesh.enabled = visible;
    }


}
