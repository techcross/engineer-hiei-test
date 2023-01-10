using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconController : MonoBehaviour
{
    public bool canWave = true;
    private float wave = 0.05f;
    private float fadeSpeed = 0.03f;
    float red, green, blue, alfa;
    private float newAlpha;
    private Image fade;
    
    public Sprite Icon;
    public Sprite Win;
    public Sprite Lose;
    public Image myPhoto;
    
    void Awake () {
        myPhoto = GetComponent<Image>();   
    }
    private void Start()
    {

    }

    // Update is called once per framevar
    void Update()
    {
        if(transform.position.x <= -6.5)
            transform.position += new Vector3(wave, 0, 0);
    }

    public void ShowIcon()
    {
        myPhoto.sprite = Icon;
    }
    
    public void ShowWin()
    {
        myPhoto.sprite = Win;
    }
    
    public void ShowLose()
    {
        myPhoto.sprite = Lose;
    }
}
