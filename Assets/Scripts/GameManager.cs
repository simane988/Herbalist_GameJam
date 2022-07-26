using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool loose = false;
    public bool win = false;
    // Start is called before the first frame update
    void Start()
    {
        loose = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (loose)
        {
            StartCoroutine("LooseGame");
        }
        if (win)
        {
            StartCoroutine("WinGame");
        }
    }

    IEnumerator LooseGame()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }

    IEnumerator WinGame()
    {
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene("Menu");
    }
}
