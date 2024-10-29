using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    private Button lobbyBtn;
    private LocalManager manager;
    private TextMeshProUGUI resultText;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        lobbyBtn = child.GetComponent<Button>();
        lobbyBtn.onClick.AddListener(Exit);

        child = transform.GetChild(2);
        resultText = child.GetComponent<TextMeshProUGUI>();

        manager = FindAnyObjectByType<LocalManager>();
        manager.OnGameEnd += AciveObj;

        this.gameObject.SetActive(false);
    }

    public void AciveObj()
    {
        this.gameObject.SetActive(true);
        resultText.text = $"Result : {manager.Score}";
    }

    private void Exit()
    {
        SceneManager.LoadScene(0);
    }
}