using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    // Start is called before the first frame update



    private AsukaState asuka = AsukaState.none;

    private AnimationType anim = AnimationType.normal;
    private int count = 0;
    [SerializeField]
    private Renderer rend;
    //public Sound sound;

    

    void Start()
    {
        StartAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        // if (!IsPlayAnimation())
        // {
        //     PlayAnimation(AnimationType.normal);
        // }
        //
        // if (Input.GetKeyDown(KeyCode.A))
        // {
        //     sound.PlayAttackSe();
        //     PlayAnimation(AnimationType.attack);
        // }

    }

    /// <summary>
    /// 初期再生
    /// </summary>
    private void StartAnimation()
    {
        Script_SpriteStudio6_Root characterRoot = null;
        characterRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);
        PlayAnimation(AnimationType.normal);

    }

    public void SetLeftAnimation(AsukaState state)
    {
        switch (state)
        {
            case AsukaState.win:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.draw:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.critical:
                PlayAnimation(AnimationType.skill1);
                break;
            case AsukaState.error:
                PlayAnimation(AnimationType.normal);
                break;
            case AsukaState.miss:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.lose:
                PlayAnimation(AnimationType.damage);
                break;
            case AsukaState.uncritical:
                PlayAnimation(AnimationType.damage);
                break;
            case AsukaState.loser:
                PlayAnimation(AnimationType.damage);
                break;
        }
    }

    public void SetRightAnimation(AsukaState state)
    {
        switch (state)
        {
            case AsukaState.win:
                PlayAnimation(AnimationType.damage);
                break;
            case AsukaState.draw:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.critical:
                PlayAnimation(AnimationType.damage);
                break;
            case AsukaState.error:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.miss:
                PlayAnimation(AnimationType.normal);
                break;
            case AsukaState.lose:
                PlayAnimation(AnimationType.attack);
                break;
            case AsukaState.uncritical:
                PlayAnimation(AnimationType.skill1);
                break;
            case AsukaState.loser:
                PlayAnimation(AnimationType.win);
                break;
        }
    }


    /// <summary>
    /// アニメーションを再生する
    /// </summary>
    /// <param name="anime"></param>
    public void PlayAnimation(AnimationType anime)
    {
        anim = anime;
        Script_SpriteStudio6_Root characterRoot = null;
        characterRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);
        //連続再生を指定する変数
        var playTime = 0;
        if (anime == AnimationType.normal || anime == AnimationType.win)
        {
            // playTime = 0 は無限ループ再生される
            characterRoot.AnimationPlay(-1, (int) anime, playTime);
        }
        else
        {
            // playTime = １ は1回のみ再生される
            playTime = 1;
            characterRoot.AnimationPlay(-1, (int) anime, playTime);
        }

    }

    /// <summary>
    /// アニメーションが再生中かどうか
    /// </summary>
    /// <returns></returns>
    public bool IsPlayAnimation()
    {
        var isPlay = false;
        Script_SpriteStudio6_Root characterRoot = null;
        characterRoot = Script_SpriteStudio6_Root.Parts.RootGet(gameObject);
        //AnimationPlay内の再生回数を取得
        var count = characterRoot.PlayTimesGetRemain(0);
        if (count >= 0)
        {
            //再生回数が０なら再生中
            isPlay = true;
        }

        return isPlay;
    }

    public bool IsNormal()
    {
        return anim == AnimationType.normal;
    }

    public void SetRenderUp(int num)
    {
        rend.sortingOrder = num;
    }




}


