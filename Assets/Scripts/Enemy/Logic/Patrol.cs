using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Patrol : MonoBehaviour
{
    private Transform[] patrolPositions;
    private int curPosition = 0;

    private void Awake()
    {
        patrolPositions = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            patrolPositions[i] = transform.GetChild(i);
        }

        curPosition = 0;
    }

    public Vector3 GetPatrolPosition()
    {
        Vector3 pos = patrolPositions[curPosition % patrolPositions.Length].localPosition;
        curPosition++;

        return pos;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Transform[] pos;
        pos = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            pos[i] = transform.GetChild(i);
        }

        Handles.color = Color.blue;
        for(int i = 0; i < pos.Length; i++)
        {
            Handles.Label(pos[i].localPosition, $"{i}");

            if (i < pos.Length - 1)
            { 
                Handles.DrawDottedLine(pos[i].localPosition, pos[i + 1].localPosition, 2f);
            }
            else
            {
                Handles.DrawDottedLine(pos[i].localPosition, pos[0].localPosition, 2f);
            }

        }
    }
#endif
}