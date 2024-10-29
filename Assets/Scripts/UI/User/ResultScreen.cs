using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class ResultScreen : MonoBehaviour
{
    private Button lobbyBtn;
    private LocalManager manager;
    private TextMeshProUGUI resultText;
    private CanvasGroup canvas;

    private void Awake()
    {
        Transform child = transform.GetChild(1);
        lobbyBtn = child.GetComponent<Button>();
        lobbyBtn.onClick.AddListener(Exit);

        child = transform.GetChild(2);
        resultText = child.GetComponent<TextMeshProUGUI>();

        manager = FindAnyObjectByType<LocalManager>();
        manager.OnGameEnd += AciveObj;

        canvas = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        canvas.alpha = 0f;
        canvas.interactable = false;
        canvas.blocksRaycasts = false;
    }

    public void AciveObj()
    {
        canvas.alpha = 1f;
        canvas.interactable = true;
        canvas.blocksRaycasts = true;

        resultText.text = $"Result : {manager.Score}";
    }

    private void Exit()
    {
        SceneManager.LoadScene(0);
    }
}