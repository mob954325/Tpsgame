using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScoreUI : MonoBehaviour
{
    private LocalManager localManager;
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        localManager = FindAnyObjectByType<LocalManager>();
        localManager.OnScoreChange += SetText;
    }

    public void SetText(float value)
    {
        text.text = $"Score : {value}";
    }
}