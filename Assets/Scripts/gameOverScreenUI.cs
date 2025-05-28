using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScreenUI : MonoBehaviour
{
    public GameObject whiteOutPanel;
    public void replayButton()
    {
        whiteOutPanel.SetActive(true);
        StartCoroutine(loadIntoScene());
    }
    IEnumerator loadIntoScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void QuitButton()
    {
        Application.Quit();
    }
}
