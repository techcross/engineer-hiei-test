using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Judge : MonoBehaviour
{
    private AsukaState aska;
    private ResultState result = ResultState.none;
    private float critical;
    // Start is called before the first frame update
    public  AsukaState CheckResult(float setTime, float leftTime, float cri)
    {
        var left = leftTime - setTime;
        if (left < 0)
        {
            return AsukaState.miss;
        }
        critical = cri;
        aska = SetState(left);
        return aska;
    }

    private AsukaState SetState(float left)
    {
        result = ResultState.win;
        if (left < critical)
        {
            return AsukaState.critical;
        }
        else
        {
            return AsukaState.win;
        }
        
    }

    public bool SetResult()
    {
        return result == ResultState.win;
    }


    
    
}
