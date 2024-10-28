using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAmmoUI : MonoBehaviour
{
    Player player;
    TextMeshProUGUI text;

    int maxAmmo = -1;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        text = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        maxAmmo = player.Weapon.GetMaxAmmo();
        player.Weapon.Controller.OnAmmoReduce = SetAmmoText;

        SetAmmoText(maxAmmo);
    }

    private void SetAmmoText(int curAmmo)
    {
        text.text = $"{curAmmo} / {maxAmmo}";
    }
}
