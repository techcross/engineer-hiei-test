using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeController : MonoBehaviour
{
    public Text timerText;

    public float totalTime;
    int seconds;
    private bool isCount;

    // Update is called once per frame
    void Update()
    {
        if(isCount)
        {
            // totalTime = Time.frameCount;
            // var num = Mathf.CeilToInt(totalTime);
            var num = Time.frameCount - totalTime;
            timerText.text = num.ToString();
        }
    }

   void Start()
    {
        totalTime = Time.frameCount;
        isCount = true;
    }

   public void StopCount()
   {
       isCount = false;
   }
}
