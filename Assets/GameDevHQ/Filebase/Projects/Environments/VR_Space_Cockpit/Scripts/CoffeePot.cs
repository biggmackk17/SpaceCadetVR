using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoffeePot : MonoBehaviour
{
    [SerializeField]
    float delay = .25f;
    float pourtime;
    [SerializeField]
    GameObject _coffee;
    [SerializeField]
    Transform _spawnPoint;
    [SerializeField]
    bool _upsideDown;
    [SerializeField]
    float _yTransform;
    int _coffeeCount = 20;
    [SerializeField]
    AudioClip _coffeClip;
    AudioSource _audioSource;


    // Start is called before the first frame update
    void Start()
    {
        _coffeeCount = 0;
        _audioSource = transform.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_upsideDown && Time.time > pourtime && _coffeeCount > 0)
              {
              Instantiate(_coffee, _spawnPoint.position, Quaternion.identity, transform.parent);
            _audioSource.PlayOneShot(_coffeClip,.5f);
              pourtime = Time.time + delay;
            _coffeeCount--;
              }
              
        _yTransform = transform.up.y;
        if (transform.up.y < .8f)
        {
            _upsideDown = true;
            
        }
        else _upsideDown = false;

    }

    public void Refill()
    {
        _coffeeCount = 20;
    }
}
