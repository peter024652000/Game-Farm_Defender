using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour {

    void Update()
    {
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

    }

    public IEnumerator LoadGameScene()
    {
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene(1);
    }

    public IEnumerator QuitGame()
    {
        yield return new WaitForSeconds(0.2f);
        Application.Quit();
    }
}
