#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Test_08_UI : TestBase
{
    public Player player;

    public float value;

    protected override void OnTest1(InputAction.CallbackContext context)
    {
        player.OnHit(value);
    }

    protected override void OnTest2(InputAction.CallbackContext context)
    {
        player.Health = 0;
    }
}
#endif