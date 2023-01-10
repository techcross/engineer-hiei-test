using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCounter : MonoBehaviour
{
    public Text battleText;

    private int count = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetBattleCount(int num)
    {
        count = num;
        battleText.text = count.ToString();
    }
}
