using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    private Button lobbyBtn;
    private LocalManager manager;

    private void Awake()
    {
        lobbyBtn = transform.GetChild(1).GetComponent<Button>();
        lobbyBtn.onClick.AddListener(Exit);

        manager = FindAnyObjectByType<LocalManager>();
        manager.Player.OnDieAction += AciveObj;

        this.gameObject.SetActive(false);
    }

    private void Start()
    {
    }

    public void AciveObj()
    {
        this.gameObject.SetActive(true);
    }

    private void Exit()
    {
        SceneManager.LoadScene(0);
    }
}