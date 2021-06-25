using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIPointer : MonoBehaviour
{
    LineRenderer _line;



    private void Start()
    {
        _line = transform.GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        RaycastHit ray;
        _line.SetPosition(0, transform.position + transform.forward/20);
        _line.SetPosition(1, transform.forward * 100);
        Physics.Raycast(transform.position, transform.forward,out ray);

        if (ray.collider != null)
        {
            Debug.Log(ray.collider.name);
            var button = ray.collider.gameObject.GetComponent<Button>();
            if (button != null)
            {
                button.Select();

                if (OVRInput.Get(OVRInput.RawAxis1D.RIndexTrigger)>.5f)
                {

                    button.onClick.Invoke();
                    Debug.Log("CLICKED");
                }


            }

        }
        
    }


    public void StartGame()
    {
        
        SceneManager.LoadScene("VR_Cockpit", LoadSceneMode.Single);

    }

    public void StartCredits()
    {
        SceneManager.LoadScene("EndCredits" , LoadSceneMode.Single);
       // Scene scene = SceneManager.GetSceneAt(2);
        //SceneManager.SetActiveScene(scene);
        //Debug.Log("Credits Should Start");
    }

    IEnumerator Load(int SceneIndex)
    {
        AsyncOperation load = SceneManager.LoadSceneAsync(SceneIndex);
        //eneable load text;
        yield return null;
       
    }
}
