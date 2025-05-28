using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    [HideInInspector]
    public int whoWon;
    void Awake()
    {
        DontDestroyOnLoad(this);
    }

    public void endGame(int whichPlayerDied)
    {
        if (whichPlayerDied == 1)
            whoWon = 2;
        else
            whoWon = 1;
        StartCoroutine(swapScene());
    }
    IEnumerator swapScene()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
