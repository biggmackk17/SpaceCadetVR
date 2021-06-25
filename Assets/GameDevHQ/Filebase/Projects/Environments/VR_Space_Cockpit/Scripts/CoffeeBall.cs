using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeeBall : MonoBehaviour
{
    

    [SerializeField]
    AudioClip _sipClip;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(this.gameObject, 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            AudioManager.Instance.PlayEffect(_sipClip, .5f);
            Destroy(this.gameObject);
        }
    }

}
