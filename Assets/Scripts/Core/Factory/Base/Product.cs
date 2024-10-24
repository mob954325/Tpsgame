using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Product : MonoBehaviour
{
    public Action OnDeactive;

    private void OnDisable()
    {
        OnDeactive?.Invoke();
    }
}