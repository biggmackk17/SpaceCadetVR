using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Credits : MonoBehaviour
{
    [SerializeField]
    GameObject[] _credits;
    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject slide in _credits)
        {
            slide.SetActive(false);
        }
        StartCoroutine("PlayCredits");
    }

    IEnumerator PlayCredits()
    {
        _credits[0].SetActive(true);
        yield return new WaitForSeconds(5);
        for(int i=1; i<_credits.Length; i++)
        {
            _credits[i - 1].SetActive(false);
            _credits[i].SetActive(true);
            yield return new WaitForSeconds(8);
        }
        SceneManager.LoadScene("MainMenu");
    }
}
