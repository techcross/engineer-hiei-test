using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleTimeCount : MonoBehaviour
{
    public Text battleTimes;

    private int count;
    private GameObject obj;
    private Master master;
    // Start is called before the first frame update
    void Start()
    {
        count = 5;
        battleTimes.text = count.ToString();
        obj = GameObject.Find("Master");
        master = obj.GetComponent<Master>();
    }

    public void BattleTimeCountUp()
    {
        if (count < 99)
        {
            count++;
            battleTimes.text = count.ToString();
            master.gameCount = count;
        }
    }

    public void BattleTimeCountDown()
    {
        if (count > 1)
        {
            count--;
            battleTimes.text = count.ToString();
            master.gameCount = count;
        }
    }
}
