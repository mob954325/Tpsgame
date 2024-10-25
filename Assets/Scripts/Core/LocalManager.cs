using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    private Player player;

    public Player Player { get => player; }

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
    }
}