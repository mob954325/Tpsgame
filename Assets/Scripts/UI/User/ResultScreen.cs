using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResultScreen : MonoBehaviour
{
    private Button lobbyBtn;

    private void Awake()
    {
        lobbyBtn = transform.GetChild(1).GetComponent<Button>();
        lobbyBtn.onClick.AddListener(Exit);

        this.gameObject.SetActive(false);
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