using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class backGoround : MonoBehaviour
{
    [SerializeField] private AudioSource AudioSource;
    
    public void OnClickPlay()
    {
        // オーディオを再生します
        AudioSource.Play();
    }

    public void OnClickStop()
    {
        // オーディオを停止します
        AudioSource.Stop();
    }
}
