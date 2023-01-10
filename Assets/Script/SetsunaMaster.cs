using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetsunaMaster 
{
    public float stageNum { get; set; }
    public float minTime { get; set; }
    public float maxTime { get; set; }
    public float minEnemy { get; set; }
    public float maxEnemy { get; set; }
    public float critical { get; set; }

    public SetsunaMaster(float stage, float setMin, float setMax, float minEne, float maxEne, float cri)
    {
        stageNum = stage;
        minTime = setMin;
        maxTime = setMax;
        minEnemy = minEne;
        maxEnemy = maxEne;
        critical = cri;
    }
}
