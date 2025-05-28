using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject whiteOutPanel;
    public void OnPlayButton()
    {
        whiteOutPanel.SetActive(true);
        StartCoroutine(loadIntoScene());
    }
    IEnumerator loadIntoScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void OnQuitButton()
    {
        Application.Quit();
    }
}