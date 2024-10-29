using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalManager : MonoBehaviour
{
    private FactoryManager factroyManager;
    private Player player;

    public Player Player 
    {
        get
        {
            if(player == null)
            {
                player = FindAnyObjectByType<Player>();
            }

            return player;
        }
    }

    public FactoryManager FactoryManager 
    { 
        get
        {
            if (factroyManager == null)
            {
                factroyManager = FindAnyObjectByType<FactoryManager>();
            }

            return factroyManager;
        }
    }

    public bool isGameStart = false;
    public bool IsGameStart 
    { 
        get => isGameStart;
        private set
        {
            isGameStart = value;
            OnGameEnd?.Invoke();
        }
    }

    private float score = -1f;
    public float Score
    {
        get => score;
        private set
        {
            score = value;
            OnScoreChange?.Invoke(score);
        }
    }

    /// <summary>
    /// 스코어 변화 시 호출되는 델리게이트 (param : 현재 점수)
    /// </summary>
    public Action<float> OnScoreChange;

    public Action OnGameEnd;

    private void Start()
    {
        Score = 0f;
        IsGameStart = true;

        Player.OnDieAction += () => { IsGameStart = false; };

#if UNITY_EDITOR
        if(isTest)
        {
            IsGameStart = false;
        }
#endif
    }

    public void SetScore(float setScore)
    {
        Score += setScore;
    }

#if UNITY_EDITOR

    [Header("test")]
    public bool isTest = false;
    
    private void Test_Setting()
    {
        isGameStart = false;
    }
#endif
}