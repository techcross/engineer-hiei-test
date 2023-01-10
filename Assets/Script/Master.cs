using System;

using System.Collections.Generic;
using UnityEngine;


public class Master : MonoBehaviour
{
    private const string easy = "easy";
    private const string normal = "normal";
    private const string hard = "hard";

    private readonly List<SetsunaMaster> EasyList = new List<SetsunaMaster>();
    private readonly List<SetsunaMaster> NormalList = new List<SetsunaMaster>();
    private readonly List<SetsunaMaster> HardList = new List<SetsunaMaster>();

    public GameState state;

    public enum Mode
    {
        easy,
        normal,
        hard,
        doubles,
        extra
    }

    public Mode Difficulty;

    public int gameCount = 5;

    public int resultCount = 0;

    public int LeftCount = 0;
    public int RightCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        CSVReader.ReadLines("master", ReadCallBack);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReadCallBack(string[] lineValues, int lineNumbers)
    {
        if(lineNumbers > 0)
        {
            var stage = Convert.ToSingle(lineValues[1]);
            var minSet = Convert.ToSingle(lineValues[2]);
            var maxSet = Convert.ToSingle(lineValues[3]);
            var minEne = Convert.ToSingle(lineValues[4]);
            var maxEne = Convert.ToSingle(lineValues[5]);
            var critical = Convert.ToSingle(lineValues[6]);

            var item = new SetsunaMaster(stage,minSet, maxSet, minEne, maxEne, critical);
            //格納する配列を決めてから、リストに追加する
            switch (lineValues[0])
            {
                case easy:
                    EasyList.Add(item);
                    break;
                case normal:
                    NormalList.Add(item);
                    break;
                case hard:
                    HardList.Add(item);
                    break;
            }
        }
    }

    public List<SetsunaMaster> GetList()
    {
        switch (Difficulty)
        {
            case Mode.easy:
                return EasyList;
            case Mode.normal:
                return NormalList;
            case Mode.hard:
                return HardList;
        }

        return null;
    }

    public List<SetsunaMaster> GetAllList()
    {
        var allList = new List<SetsunaMaster>(EasyList);
        allList.AddRange(NormalList);
        allList.AddRange(HardList);
        return allList;
    }
 
    public int GetGameCount()
    {
        return gameCount;
    }

    public void SetResultCount(int num)
    {
        resultCount = num;
    }

    public void SetLeftCount()
    {
        LeftCount++;
    }

    public void SetRightCount()
    {
        RightCount++;
    }

    public void GetState(GameState game)
    {
        state = game;
    }

    public void SetState(Mode mode)
    {
        Difficulty = mode;
    }
}
