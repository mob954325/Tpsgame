using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HitEffect : MonoBehaviour
{
    private Player player;
    public Image image;
    public AnimationCurve hitAnimCurve;

    private float maxActiveTime = 1.5f;
    private float timer = -1f;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();
        image = GetComponent<Image>();
    }

    private void Start()
    {
        player.OnHitAction += (_) =>
        {
            timer = maxActiveTime;
        };
    }

    public void Update()
    {
        if (timer >= 0f)
        {
            timer -= Time.deltaTime;
            image.color = new Color(image.color.r, image.color.g, image.color.b, hitAnimCurve.Evaluate(timer / maxActiveTime));
            Debug.Log(timer / maxActiveTime);
        }
    }
}