using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoubleGenerater : MonoBehaviour
{
    [Header("プレハブ生成")]
    [SerializeField] private GameObject Mask;
    [SerializeField] private GameObject LeftController;
    [SerializeField] private GameObject RightController;
    [SerializeField] private GameObject Header;
    [SerializeField] private GameObject LeftIcon;
    [SerializeField] private GameObject RightIcon;
    [SerializeField] private GameObject Win;
    [SerializeField] private GameObject Lose;
    [SerializeField] private GameObject TimeController;
    [SerializeField] private GameObject CheckMarker;

    [SerializeField] private GameObject burst;

    [SerializeField] private Text LeftScore;
    [SerializeField] private Text RightScore;
    [SerializeField] private backGoround back;
    // Start is called before the first frame update
    private AsukaState aska;
    private GameObject Left;
    private GameObject Right;
    private GameObject LeftIconImage;
    private GameObject RightIconImage;
    private GameObject timeController;
    private GameObject result;
    private GameObject bgm;
    private Sound sound;
    
    private float time;
    private float settingTime  = 999999;
    private float critical = 0;
    private float userTouchTime;
    
    private int battleCount = 0;
    //「いまだ」の生成を１回のみにする
    private bool isFirst = true;
    //お手付き用
    private bool LeftOtetsuki = false;

    private bool RightOtetsuki = false;
    //勝敗判定用
    private bool isLeftWin = false;
   
    
    //ゲーム開始処理用
    private bool isSet = true;
    //プレイヤーかCPUの入力を１回のみにする
    private bool isLeftClick = true;
    private bool isRightClick = true;
    private float LeftTime = 0f;
    private float RightTime = 0f;
    
    private GameObject parent;
    private GameObject item;
    private List<SetsunaMaster> list;
    private GameObject obj;
    private Master master;
    private int count;
    private float totalTime;
    
    
    
    private void Awake()
    {
        totalTime = Time.frameCount;
        obj = GameObject.Find("Master");
        master = obj.GetComponent<Master>();
        count = master.GetGameCount();
        list = master.GetAllList();
        bgm = GameObject.Find("SoundManager");
        sound = bgm.GetComponent<Sound>();
    }
    void Start()
    {
        parent = GameObject.Find("DoubleGenerater");
        SetGame();
        Invoke(nameof(backMusic),4.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Left.GetComponent<CharacterController>() != null)
        {
            if (battleCount  <= count)
            {
                if (Left.GetComponent<CharacterController>().IsPlayAnimation() && Right.GetComponent<CharacterController>().IsPlayAnimation())
                {
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
                        Invoke(nameof(SetGame), 0.5f);
                        Invoke(nameof(backMusic),4.5f);
                    }
                   
                }
                if (time - totalTime >= settingTime && isFirst)
                {
                    battleCount++;
                    item = Instantiate(burst,new Vector3(6.5f, 4.5f, 5.0f), Quaternion.identity);
                    back.OnClickStop();
                    sound.PlayJingle(true);
                    timeController = Instantiate(TimeController, new Vector3(7.0f, -4.5f, 5.0f), Quaternion.identity);
                    timeController.transform.parent = parent.transform;
                    isFirst = false;
                }

                //  1P用の判定装置
                if (Input.GetKeyDown(KeyCode.A))
                {
                   LeftAction();
                }
                //2P用の判定装置
                if (Input.GetKeyDown(KeyCode.L))
                {
                    RightAction();
                }

            }
           
        }
        
    }

    private AsukaState Judge()
    {
        var left = LeftTime - settingTime;
        var right = RightTime - settingTime;
        if (left < right)
        {
            if (left < 0f)
            {
                //１Pがお手付き
                return AsukaState.miss;
            }
            if (left < critical)
            {
                //１Pがクリティカル
                isLeftWin = true;
                return AsukaState.critical;
                
            }
            //１Pが勝ち
            isLeftWin = true;
            
            return AsukaState.win;
            
        }
        else if (right == left)
        {
            return AsukaState.draw;
        }
        else
        {
            if (right < 0f)
            {
                //2Pがお手付き
                return AsukaState.error;
            }
            if (right < critical)
            {
                //２Pがクリティカル
                isLeftWin = false;
                return AsukaState.uncritical;
            }
            //２Pが勝ち
            isLeftWin = false;
            return AsukaState.lose;
        }
    }
    
    private void CreateOfCharacter()
    {
        var objects = GameObject.FindGameObjectsWithTag("GameChara");
        foreach (var item in objects) 
        {
            Destroy(item);
        }
        Left = Instantiate (LeftController, new Vector3(-5.0f,-3.5f,5.0f), Quaternion.identity);
        Right = Instantiate (RightController, new Vector3(5.0f,-3.5f,5.0f), new Quaternion(0f,180f,0f,0f));
        Left.transform.parent = parent.transform;
        Right.transform.parent = parent.transform;
    }
    
    private void StartGame()
    { 
        var objects = GameObject.FindGameObjectsWithTag("Effect");
        foreach (var item in objects) 
        {
            Destroy(item);
        }

        var rand = new System.Random();
        var count = rand.Next(0, list.Count);
        settingTime = Random.Range(list[count].minTime, list[count].maxTime) / 10;
       
        critical = list[count].critical /100;
        isFirst = true;
        if (LeftOtetsuki)
        {
            var check = Instantiate(CheckMarker, new Vector3(-6, 3, 5), Quaternion.identity);
            check.transform.parent = parent.transform;
        }
        if (RightOtetsuki)
        {
            var check = Instantiate(CheckMarker, new Vector3(6, 3, 5), Quaternion.identity);
            check.transform.parent = parent.transform;
        }
        
        time = 0;
        userTouchTime = 0;
        timeController = null;
        LeftTime = 999999;
        RightTime = 999999;
        isLeftClick = true;
        isRightClick = true;
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
        RightIconImage = Instantiate (RightIcon, new Vector3(10.0f,-4f,5.0f), Quaternion.identity);
        LeftIconImage.GetComponent<IconController>().ShowIcon();
        RightIconImage.GetComponent<RightIcon>().ShowIcon();
        sound.PlayJingle();
    }
    private void SetResult()
    {
        var left = gameObject;
        var right = gameObject;
        if (isLeftWin)
        {
           left = Instantiate(Win, new Vector3(-6.0f, 3f, 5.0f), Quaternion.identity);
           right = Instantiate (Lose, new Vector3(6.0f,3f,5.0f), Quaternion.identity);
        }
        else
        {
            left = Instantiate(Lose, new Vector3(-6.0f, 3f, 5.0f), Quaternion.identity);
            right = Instantiate (Win, new Vector3(6.0f,3f,5.0f), Quaternion.identity);
        }

        left.transform.parent = parent.transform;
        right.transform.parent = parent.transform;

    }

    private void ResetState()
    {
        aska = AsukaState.none;
    }
    
    private IEnumerator SceneChange()
    {
        yield return new WaitForSeconds(2.0f);

        yield return SceneManager.LoadSceneAsync("Result");
    }
    
    private void SetGame()
    {
        if(battleCount == count)
        {
            SceneManager.LoadSceneAsync("Result");
        }
        else
        {
            CreateOfCharacter();
            isFirst = true;
            Invoke(nameof(SetMask), 0.5f);
            Invoke(nameof(StartPrepare), 1.0f);
            Invoke(nameof(CreateIcon), 1.0f);
            //sound.PlayFadeSe();
            ResetState();
            Invoke(nameof(StartGame), 5f);
            LeftScore.text = "アスカ　" + master.LeftCount + "勝";
            RightScore.text = "小狐丸　" + master.RightCount + "勝";
        }

        totalTime = Time.frameCount;
    }

    private void PlayAnimation()
    {
        Left.GetComponent<CharacterController>().SetLeftAnimation(aska);
        Right.GetComponent<CharacterController>().SetRightAnimation(aska);
        if(isLeftWin && aska != AsukaState.miss)
        {
            Invoke(nameof(LeftMove), 0.2f);
        }
        else if(!isLeftWin && aska != AsukaState.error)
        {
            RightMove();
            //Invoke(nameof(RightMove), 0.2f);
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

    private void LeftAction()
    {
        if (isLeftClick)
        {
            LeftTime = userTouchTime - totalTime;
            isLeftClick = false;
            if (timeController != null)
                timeController.GetComponent<TimeController>().StopCount();
            Destroy(item);
            isFirst = false;
            isRightClick = false;
            aska = Judge();
            settingTime = 999999;
            if (aska == AsukaState.critical)
            {
                sound.PlaySkillSe(CharacterKind.normal_Asuka);
            }
            else
            {
                sound.PlayAttackSe();
            }
            Invoke(nameof(PlayAnimation),0.1f);
         
                    
            if (aska is AsukaState.miss or AsukaState.error)
            {
                if (!LeftOtetsuki)
                {
                    //お手付きの場合はチェックを入れる
                    var check = Instantiate(CheckMarker, new Vector3(-5, -3, 5), Quaternion.identity);
                    check.transform.parent = parent.transform;
                    LeftOtetsuki = true;
                    time = 0;
                    sound.PlayOtetsuki(true);
                    Invoke(nameof(SetGame), 1f);
                                
                }
                else
                {
                    sound.PlayOtetsuki(false);
                    master.SetResultCount(battleCount);
                    //２回目は敗北でシーン遷移
                    StartCoroutine(SceneChange());
                }
            }
            else if (aska == AsukaState.draw)
            {
                //引き分けの場合はペナルティなく再挑戦
            }
            else
            {
                master.SetLeftCount();
            }
                            
            isSet = true;
        }
    }

    private void RightAction()
    {
        if (isRightClick)
        {
            RightTime = userTouchTime - totalTime;
            isRightClick = false;
            if (timeController != null)
                timeController.GetComponent<TimeController>().StopCount();
            Destroy(item);
            isFirst = false;
            isLeftClick = false;
            aska = Judge();
            settingTime = 999999;
            if (aska == AsukaState.uncritical)
            {
                sound.PlaySkillSe(CharacterKind.kogi);
            }
            else
            {
                sound.PlayAttackSe();
            }           
            Invoke(nameof(PlayAnimation),0.1f);
                        
            //sound.PlayAttackSe();
            if (aska is AsukaState.miss or AsukaState.error)
            {
                if (!RightOtetsuki)
                {
                    //お手付きの場合はチェックを入れる
                    var check = Instantiate(CheckMarker, new Vector3(5, -3, 5), Quaternion.identity);
                    check.transform.parent = parent.transform;
                    RightOtetsuki = true;
                    time = 0;
                    sound.PlayOtetsuki(true);
                    Invoke(nameof(SetGame), 1f);
                }
                else
                {
                    sound.PlayOtetsuki(false);
                    //２回目は敗北でシーン遷移
                    StartCoroutine(SceneChange());
                }
            }
            else if (aska == AsukaState.draw)
            {
                //引き分けの場合はペナルティなく再挑戦
                Invoke(nameof(SetGame), 1f);
            }
            else
            {
                master.SetRightCount();
            }
                        
        }
        isSet = true;
        
    }
    
    private void backMusic()
    {
        back.OnClickPlay();
    }
}
