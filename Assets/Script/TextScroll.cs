using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{
    public float speed = 1.0f;
    [SerializeField] private Text text;
    private float time;

    void Start()
    {
        text = this.gameObject.GetComponent<Text>();
    }

    void Update()
    {
        text.color = GetAlphaColor(text.color);
    }

    private Color GetAlphaColor(Color color)
    {
        time += Time.deltaTime * 5.0f * speed;
        color.a = Mathf.Sin(time);
        return color;
    }
}