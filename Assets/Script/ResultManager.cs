using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultManager : MonoBehaviour
{
   
    [SerializeField] private GameObject LoseResult;
    [SerializeField] private GameObject Winner;
    [SerializeField] private GameObject WinVer;
    
    [Header("SDキャラ生成")]
    [SerializeField] private GameObject Easy;
    [SerializeField] private GameObject Normal;
    [SerializeField] private GameObject Hard;
    [SerializeField] private GameObject Momizi;
    [SerializeField] private GameObject Amaterasu;
    [SerializeField] private GameObject Takiyasya;
    [SerializeField] private GameObject Kiichi;
    [SerializeField] private GameObject Kogitune;
    
    [Header("アイコン生成")]
    [SerializeField] private GameObject IconOfEasy;
    [SerializeField] private GameObject IconOfNormal;
    [SerializeField] private GameObject IconOfHard;
    [SerializeField] private GameObject IconOfMomizi;
    [SerializeField] private GameObject IconOfAmaterasu;
    [SerializeField] private GameObject IconOfTakiyasya;
    [SerializeField] private GameObject IconOfKiichi;
    [SerializeField] private GameObject IconOfKogitune;

    [SerializeField] private Text text;
    [SerializeField] private GameObject command;
    private GameObject LeftController;
    private GameObject RightController;
    private GameObject LeftIcon;
    private GameObject RightIcon;
    private GameObject obj;
    private Master master;
    private GameObject Left;
    private GameObject Right;
    private GameObject parent;
    private AsukaState aska;
    private GameObject loseResult;
    private GameObject LeftIconImage;
    private GameObject RightIconImage;
    private GameObject bgm;
    private Sound sound;

    private GameObject winnerResult;
    // Start is called before the first frame update
    void Start()
    {
        obj = GameObject.Find("Master");
        master = obj.GetComponent<Master>();
        parent = GameObject.Find("ResultManager");
        bgm = GameObject.Find("SoundManager");
        

        if (master.Difficulty == Master.Mode.doubles)
        {
            LeftController = Normal;
            LeftIcon = IconOfNormal;
            RightController = Kogitune;
            RightIcon = IconOfKogitune;

            CreateIcon();
            sound = bgm.GetComponent<Sound>();
            winnerResult = Instantiate(Winner, new Vector3(0, 4f, 0), Quaternion.identity);
            if (master.LeftCount > master.RightCount )
            {
                LeftIconImage.GetComponent<IconController>().ShowWin();
                RightIconImage.GetComponent<RightIcon>().ShowLose();
                aska = AsukaState.win;
                text.text = "アスカ";
            }
            else
            {
                LeftIconImage.GetComponent<IconController>().ShowLose();
                RightIconImage.GetComponent<RightIcon>().ShowWin();
                aska = AsukaState.lose;
                text.text = "小狐丸";
            }
            sound.PlayResult(true);
            SetPlayCharacter();
        }
        else
        {
            if (master.state == GameState.Extra)
            {
                SetResult();
            }
            else if (master.resultCount < 5 )
            {
                //〇人抜きのプレハブ
                SetResult();
            }
            else
            {
                SetCharacter();
                LeftIconImage = Instantiate (LeftIcon, new Vector3(-15.0f,4f,5.0f), Quaternion.identity);
                sound = bgm.GetComponent<Sound>();
                LeftIconImage.GetComponent<IconController>().ShowWin();
                Left = Instantiate (LeftController, new Vector3(0f,-3.5f,5.0f), Quaternion.identity);
                Instantiate(WinVer, new Vector3(0, 3, 0), Quaternion.identity);
                aska = AsukaState.win;
                sound.PlayResult(true);
                Left.GetComponent<CharacterController>().SetLeftAnimation(aska);
                
                if (master.Difficulty == Master.Mode.hard)
                {
                    Instantiate(command, new Vector3(-3.5f, -4f, 0f), Quaternion.identity);
                }
                //勝利のプレハブ
            }
        }
        
        var objects = GameObject.FindGameObjectsWithTag("DontDestroy");
        foreach (var item in objects) 
        {
            Destroy(item);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetAnimation()
    {
        Left.GetComponent<CharacterController>().SetLeftAnimation(aska);
        Right.GetComponent<CharacterController>().SetRightAnimation(aska);
    }

    private void CreateIcon()
    {
        LeftIconImage = Instantiate (LeftIcon, new Vector3(-15.0f,4f,5.0f), Quaternion.identity);
        RightIconImage = Instantiate (RightIcon, new Vector3(15.0f,4f,5.0f), Quaternion.identity);
    }
    
    private void SetCharacter()
    {
        switch (master.Difficulty)
        {
            case Master.Mode.easy:
                LeftController = Easy;
                LeftIcon = IconOfEasy;
                break;
            case Master.Mode.normal:
                LeftController = Normal;
                LeftIcon = IconOfNormal;
                break;
            case Master.Mode.hard:
                LeftController = Hard;
                LeftIcon = IconOfHard;
                break;
            case Master.Mode.doubles:
                break;
            case Master.Mode.extra:
                LeftController = Hard;
                LeftIcon = IconOfHard;
                break;
        }

        switch (master.resultCount % 5)
        {
            case 0:
                RightController = Momizi;
                RightIcon = IconOfMomizi;
                break;
            case 1:
                RightController = Kogitune;
                RightIcon = IconOfKogitune;
                break;
            case 2:
                RightController = Takiyasya;
                RightIcon = IconOfTakiyasya;
                break;
            case 3:
                RightController = Amaterasu;
                RightIcon = IconOfAmaterasu;
                break;
            case 4:
                RightController = Kiichi;
                RightIcon = IconOfKiichi;
                break;
        }
    }

    private void SetPlayCharacter()
    {
        Left = Instantiate (LeftController, new Vector3(-5.0f,-3.5f,5.0f), Quaternion.identity);
        Right = Instantiate (RightController, new Vector3(5.0f,-3.5f,5.0f), new Quaternion(0f,180f,0f,0f));
        Left.transform.parent = parent.transform;
        Right.transform.parent = parent.transform;
        
        Invoke(nameof(SetAnimation),0.5f);
    }

    private void SetResult()
    {
        Invoke(nameof(SoundPlayer),0.5f);
        SetCharacter();
        CreateIcon();
        aska = AsukaState.lose;
        LeftIconImage.GetComponent<IconController>().ShowLose();
        RightIconImage.GetComponent<RightIcon>().ShowWin();
        loseResult = Instantiate(LoseResult, new Vector3(0, 3, 0), Quaternion.identity);
        SetPlayCharacter();
        master.state = GameState.None;
    }

    private void SoundPlayer()
    {
        sound = bgm.GetComponent<Sound>();
        sound.PlayResult(false);
    }
}
