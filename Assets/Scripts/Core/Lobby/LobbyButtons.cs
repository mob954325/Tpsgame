using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyButtons : MonoBehaviour
{
    Button startBtn;
    Button endBtn;

    private void Awake()
    {
        Transform child = transform.GetChild(0);
        startBtn = child.GetComponent<Button>();
        child = transform.GetChild(1);
        endBtn = child.GetComponent<Button>();

        startBtn.onClick.AddListener(() => { StartGame(); });
        endBtn.onClick.AddListener(() => { ExitGame(); });
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ExitGame()
    {
        Application.Quit();
    }
}
