using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
    public void loadScene()
    {
        SceneManager.LoadScene("Prologue");
    }

    public void quit()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
        Application.Quit();
    }
}
