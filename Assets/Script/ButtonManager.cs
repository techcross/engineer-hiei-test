using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameObject obj;
    private StartManager startManager;
    private GameObject counter;
    private BattleTimeCount timeCount;

   
    
    // Start is called before the first frame update
    void Awake()
    {
        obj = GameObject.Find("Manager");
        startManager = obj.GetComponent<StartManager>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if (startManager.gameState == GameState.doublesWait)
        {
            counter = GameObject.Find("counting");
            timeCount = counter.GetComponent<BattleTimeCount>();
        }
        
    }

    public void OnClickShingle()
    {
        startManager.gameState = GameState.Shingle;
    }

    public void OnclickEasy()
    {
        startManager.gameState = GameState.Easy;
    }
    
    public void OnclickNormal()
    {
        startManager.gameState = GameState.Normal;
    }
    
    public void OnclickHard()
    {
        startManager.gameState = GameState.Hard;
    }
    
    public void OnclickSelect()
    {
        startManager.gameState = GameState.Select;
    }

    public void OnclickDouble()
    {
        startManager.gameState = GameState.doubles;
    }

    public void OnEnterClick()
    {
        startManager.gameState = GameState.doubleSelect;
    }

    public void OnClickCountUp()
    {
        timeCount.BattleTimeCountUp();
    }

    public void OnClickCountDown()
    {
        timeCount.BattleTimeCountDown();
    }


    public void ReturnToSelectType()
    {
        startManager.gameState = GameState.Return;
    }

}
