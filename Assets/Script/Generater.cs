using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class Generater : MonoBehaviour
{
    [Header("プレハブ生成")]
    [SerializeField] private GameObject Mask;

    [SerializeField] private GameObject Easy;
    [SerializeField] private GameObject Normal;
    [SerializeField] private GameObject Hard;
    [SerializeField] private GameObject Momizi;
    [SerializeField] private GameObject Amaterasu;
    [SerializeField] private GameObject Takiyasya;
    [SerializeField] private GameObject Kiichi;
    [SerializeField] private GameObject Kogitune;
    [SerializeField] private GameObject Header;
    [SerializeField] private GameObject IconOfEasy;
    [SerializeField] private GameObject IconOfNormal;
    [SerializeField] private GameObject IconOfHard;
    [SerializeField] private GameObject IconOfMomizi;
    [SerializeField] private GameObject IconOfAmaterasu;
    [SerializeField] private GameObject IconOfTakiyasya;
    [SerializeField] private GameObject IconOfKiichi;
    [SerializeField] private GameObject IconOfKogitune;
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    [SerializeField] private GameObject TimeController;
    [SerializeField] private GameObject CheckMarker;
    [SerializeField] private GameObject burst;
    [Header("アタッチするもの")]
    [SerializeField] private Judge judgement;

    [SerializeField] private backGoround back;
   
   
    public bool isStart;

    private GameObject LeftController;
    private GameObject RightController;
    private GameObject LeftIcon;
    private GameObject RightIcon;
    
    private AsukaState aska;
    private CharacterKind chara;
    private GameObject Left;
    private GameObject Right;
    private GameObject timeController;
    private GameObject result;
    private GameObject item;
    private GameObject LeftIconImage;
    private GameObject RightIconImage;
    // Start is called before the first frame update
    private float time;
    //入力防止のために大きい数をとりあえず入れとく
    private float settingTime  = 999999;
    private float enemyTime = 999999;
    private float critical = 0;
    private float userTouchTime;
    
    private int battleCount = 0;
    //「いまだ」の生成を１回のみにする
    private bool isFirst = true;
    //お手付き用
    private bool otetsuki = false;
    //勝敗判定用
    private bool iswin = false;
    //ゲーム開始処理用
    private bool isSet = true;
    //プレイヤーかCPUの入力を１回のみにする
    private bool isClick = true;
    
    private GameObject parent;
    private List<SetsunaMaster> list;
    private GameObject obj;
    private GameObject bgm;
    private Master master;
    private Sound sound;
    private int count;
    private Master.Mode mode;
    public float totalTime;
    private bool enemy = false;

    private void Awake()
    {
        obj = GameObject.Find("Master");
        master = obj.GetComponent<Master>();
        bgm = GameObject.Find("SoundManager");
        sound = bgm.GetComponent<Sound>();
        count = master.GetGameCount();
        list = master.GetList();
        mode = master.Difficulty;
        totalTime = Time.frameCount;
    }

    void Start()
    {
        parent = GameObject.Find("Generater");
        SetAsuka();
        Left = Instantiate (LeftController, new Vector3(-5.0f,-3.5f,5.0f), Quaternion.identity);
        isFirst = false;
        SetGame();
        Invoke(nameof(backMusic),5.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Left.GetComponent<CharacterController>() != null)

        {
            if (battleCount <= count)
            {
                if (Left.GetComponent<CharacterController>().IsPlayAnimation() && Right.GetComponent<CharacterController>().IsPlayAnimation())
                {
                    //judgement.SetResult(false);
                    time = Time.frameCount;
                    userTouchTime = Time.frameCount;
                }

                else if (!Left.GetComponent<CharacterController>().IsPlayAnimation() &&
                         !Right.GetComponent<CharacterController>().IsPlayAnimation() && isSet)
                {
                    if (battleCount > 0)
                    {
                        isSet = false;
                        SetResult();
                        Invoke(nameof(SetGame), 1.5f);
                        Invoke(nameof(backMusic),5.5f);
                    }
                }

                if (time - totalTime >= settingTime && isFirst)
                {
                    item = Instantiate(burst,new Vector3(0f, 0f, 5.0f), Quaternion.identity);
                   back.OnClickStop();
                    sound.PlayJingle(true);
                    timeController = Instantiate(TimeController, new Vector3(7.0f, -4.5f, 5.0f), Quaternion.identity);
                    timeController.transform.parent = parent.transform;
                    isFirst = false;
                    enemy = true;
                }

                if (time - totalTime >= enemyTime && enemy)
                {
                    if (isClick)
                    {
                        enemy = false;
                        isClick = false;
                        timeController.GetComponent<TimeController>().StopCount();
                        Destroy(item);
                        enemyTime = 999999;
                        isFirst = false;
                        aska = AsukaState.lose;
                        if (battleCount == 3)
                        {
                            sound.PlayAttackSe(true);
                        }
                        else
                        {
                            sound.PlayAttackSe();
                        }
                        Invoke(nameof(PlayAnimation),0.1f);
                        settingTime = 999999;
                        isSet = true;
                      
                        master.SetResultCount(battleCount);

                        StartCoroutine(SceneChange());
                    }
                }
                else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.L))
                {
                    if (isClick)
                    {
                        isClick = false;
                        if (timeController != null)
                            timeController.GetComponent<TimeController>().StopCount();
                        isFirst = false;
                        Destroy(item);
                        aska = judgement.CheckResult(settingTime, userTouchTime - totalTime, critical);
                        if (aska == AsukaState.critical)
                        {
                            sound.PlaySkillSe(chara);
                        }
                        else
                        {
                            sound.PlayAttackSe();
                        }
                        Invoke(nameof(PlayAnimation),0.1f);
                        
                        
                        settingTime = 999999;
                        isSet = true;
                        if (battleCount < count)
                        {
                            if (aska is AsukaState.miss or AsukaState.error)
                            {
                                if (!otetsuki)
                                {
                                    //お手付きの場合はチェックを入れる
                                    var check = Instantiate(CheckMarker, new Vector3(-5, -3, 5), Quaternion.identity);
                                    check.transform.parent = parent.transform;
                                    otetsuki = true;
                                    time = 0;
                                    sound.PlayOtetsuki(true);
                                    Invoke(nameof(SetGame), 1f);
                                }
                                else
                                {
                                    sound.PlayOtetsuki(false);
                                    //２回目は敗北でシーン遷移
                                    master.SetResultCount(battleCount);
                                    StartCoroutine(SceneChange());
                                }
                            }
                            else if (aska == AsukaState.draw)
                            {
                                //引き分けの場合はペナルティなく再挑戦
                            }
                            else
                            {
                                iswin = judgement.SetResult();
                                battleCount++;
                            }

                        }
                    }

                }
            }
        }
    }

    private void CreateOfCharacter()
    {
        SetCharacter();
        foreach ( Transform child in this.transform )
        {
            // 一つずつ破棄する
            Destroy(child.gameObject);
        }
       
        Right = Instantiate (RightController, new Vector3(5.0f,-3.5f,5.0f), new Quaternion(0f,180f,0f,0f));
        // Left.transform.parent = parent.transform;
        Right.transform.parent = parent.transform;
        Left.transform.position = new Vector3(-5f, -3.5f, 6);
        Left.GetComponent<CharacterController>().PlayAnimation(AnimationType.normal);
    }

    private void StartGame()
    { 
        var objects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (var item in objects) 
        {
            Destroy(item);
        }
     
        var selectList = list.Where(x => x.stageNum == battleCount + 1).ToList();

        var count = Random.Range(0, selectList.Count());
        settingTime = Random.Range(selectList[count].minTime, selectList[count].maxTime) /10 ;
        enemyTime = settingTime + Random.Range(selectList[count].minEnemy, selectList[count].maxEnemy) ;
        critical = selectList[count].critical;
        isFirst = true;
        if (otetsuki)
        {
            var check = Instantiate(CheckMarker, new Vector3(-6, 3, 5), Quaternion.identity);
            check.transform.parent = parent.transform;
        }
        time = 0;
        userTouchTime = 0;
        timeController = null;
        isClick = true;
        totalTime = Time.frameCount;
    }
    
    public void SetMask()
    {
        Instantiate (Mask, new Vector3(0.0f,0f,1.0f), Quaternion.identity);
    }
    public void StartPrepare()
    {
        Instantiate (Header, new Vector3(0.0f,4.0f,5.0f), Quaternion.identity);
        Instantiate (Header, new Vector3(0.0f,-4.0f,5.0f), Quaternion.identity);
    }
    public void CreateIcon()
    {
        LeftIconImage = Instantiate (LeftIcon, new Vector3(-10.0f,4f,5.0f), Quaternion.identity);
        RightIconImage = Instantiate (RightIcon, new Vector3(10.0f,-3f,5.0f), Quaternion.identity);
        LeftIconImage.GetComponent<IconController>().ShowIcon();
        RightIconImage.GetComponent<RightIcon>().ShowIcon();
        sound.PlayJingle();
    }

    private void SetResult()
    {
        result = iswin ? Instantiate (Win, new Vector3(0.0f,3f,5.0f), Quaternion.identity) : Instantiate (Lose, new Vector3(0.0f,3f,5.0f), Quaternion.identity);
        result.transform.parent = parent.transform;
    }

    private void SetGame()
    {
        if(battleCount == count)
        {
            master.resultCount = battleCount;
            SceneManager.LoadSceneAsync("Result");
        }
        else
        {
            CreateOfCharacter();
            isFirst = true;
            Invoke(nameof(SetMask), 0.5f);
            Invoke(nameof(StartPrepare), 1.0f);
            Invoke(nameof(CreateIcon), 1.0f);

            ResetState();
            Invoke(nameof(StartGame), 5f);
        }
    }

    private void ResetState()
    {
        aska = AsukaState.none;
        iswin = false;
    }

    private IEnumerator SceneChange()
    {
        master.resultCount = battleCount;
        yield return new WaitForSeconds(1.0f);

        yield return SceneManager.LoadSceneAsync("Result");
    }

    private void SetAsuka()
    {
        switch (mode)
        {
            case Master.Mode.easy:
                LeftController = Easy;
                LeftIcon = IconOfEasy;
                chara = CharacterKind.easy_Asuka;
                break;
            case Master.Mode.normal:
                LeftController = Normal;
                LeftIcon = IconOfNormal;
                chara = CharacterKind.normal_Asuka;
                break;
            case Master.Mode.hard:
                LeftController = Hard;
                LeftIcon = IconOfHard;
                chara = CharacterKind.hard_Asuka;
                break;
            case Master.Mode.doubles:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
    private void SetCharacter()
    {
        switch (battleCount % 5)
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
    
    private void PlayAnimation()
    {
        Left.GetComponent<CharacterController>().SetLeftAnimation(aska);
        Right.GetComponent<CharacterController>().SetRightAnimation(aska);
        if(iswin && aska != AsukaState.miss)
        {
            Invoke(nameof(LeftMove), 0.2f);
        }
        else if(!iswin && aska != AsukaState.miss)
        {
            RightMove();
            
        }
    }
    
    private void LeftMove()
    {
        for (var i = -5; i < 0; i++)
        {
            Left.transform.position = new Vector3(i, -3.5f, 6);
        }
        
    }

    private void RightMove()
    {
        Right.transform.position = new Vector3(0, -3.5f, 6);
    }

    private void backMusic()
    {
        back.OnClickPlay();
    }
}
