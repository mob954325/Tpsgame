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
    public bool IsGameStart { get => isGameStart; }

    private float score = -1f;
    private float Score
    {
        get => score;
        set
        {
            score = value;
            OnScoreChange?.Invoke(score);
        }
    }

    /// <summary>
    /// 스코어 변화 시 호출되는 델리게이트 (param : 현재 점수)
    /// </summary>
    public Action<float> OnScoreChange;

    private void Start()
    {
        Score = 0f;
        isGameStart = true;
    }

    public void SetScore(float setScore)
    {
        Score += setScore;
    }

#if UNITY_EDITOR
    private void Test_Setting()
    {
        isGameStart = false;
    }
#endif
}