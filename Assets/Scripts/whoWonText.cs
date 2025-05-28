using TMPro;
using UnityEngine;

public class whoWonText : MonoBehaviour
{
    private GameObject manager;
    public TMP_Text text;
    void Start()
    {
        manager = GameObject.Find("GameManager");
        text.text = $"Player {manager.GetComponent<GameManager>().whoWon} won!";
    }
}
