using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Monitor : MonoBehaviour
{

    SpriteRenderer _renderer;
    Color _savedColor;
    


    private void Start()
    {
        _renderer = transform.GetComponent<SpriteRenderer>();
    }
    private void OnEnable()
    {
        EventManager.OnPowerDown += PowerDown;
        EventManager.OnPowerUp += PowerUp;
    }
    public void PowerDown()
    {
        _savedColor = _renderer.color;
        _renderer.color = Color.black;
        AudioManager.Instance.StopEffectSource();
    }
   public void PowerUp()
    {
        _renderer.color = _savedColor;
        AudioManager.Instance.PlayEffectSource();
    }
}
