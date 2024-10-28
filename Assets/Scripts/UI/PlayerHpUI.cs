using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerHpUI : MonoBehaviour
{
    Player player;
    TextMeshProUGUI text;

    private float maxHealth;
    private float curHealth;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();

        player.OnHitAction = SetHpText;
    }

    private void Start()
    {
        maxHealth = player.MaxHealth;
        curHealth = maxHealth;
        SetHpText(0f);        
    }

    private void SetHpText(float reduceValue)
    {
        curHealth = Mathf.Clamp(curHealth - reduceValue, 0f, maxHealth);
        text.text = $"Health : {curHealth}";
    }
}