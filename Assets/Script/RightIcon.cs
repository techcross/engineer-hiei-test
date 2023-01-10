using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RightIcon : MonoBehaviour
{
    private float wave = 0.05f;
    public Sprite Icon;
    public Sprite Win;
    public Sprite Lose;
    public Image myPhoto;
    
    void Awake () {
        myPhoto = GetComponent<Image>();   
    }
    void Update()
    {
        if(transform.position.x >= 6.5)
            transform.position -= new Vector3(wave, 0, 0);
       

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
