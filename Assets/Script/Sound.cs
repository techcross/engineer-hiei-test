using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Sound : MonoBehaviour
{
  
    [SerializeField] private AudioClip attack0001 = null;
    [SerializeField] private AudioClip attack0002 = null;
    [SerializeField] private AudioClip attack0003 = null;
    [SerializeField] private AudioClip attack0004 = null;
    [SerializeField] private AudioClip attack0005 = null;
    [SerializeField] private AudioClip attack0006 = null;
    
    [SerializeField] private AudioClip Jingle0001 = null;
    [SerializeField] private AudioClip Jingle0002 = null;
    [SerializeField] private AudioClip Jingle0003 = null;
    [SerializeField] private AudioClip Jingle0004 = null;
    [SerializeField] private AudioClip Jingle0005 = null;
    [SerializeField] private AudioClip Jingle0006 = null;
    [SerializeField] private AudioClip Jingle0007 = null;
    [SerializeField] private AudioClip Jingle0008 = null;
    
    [SerializeField] private AudioClip UISE_0001 = null;
    [SerializeField] private AudioClip UISE_0002 = null;
    [SerializeField] private AudioClip UISE_0003 = null;

    private AudioSource Audio;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        Audio = GetComponent<AudioSource>();
    }

    public void PlayAttackSe(bool amaterasu = false)
    {
        Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(amaterasu ? attack0002 : attack0001);
    }

    public void PlaySkillSe(CharacterKind chara)
    {
        Audio = GetComponent<AudioSource>();
        switch (chara)
        {
            case CharacterKind.easy_Asuka:
                Audio.PlayOneShot(attack0003);
                break;
            case CharacterKind.normal_Asuka:
                Audio.PlayOneShot(attack0004);
                break;
            case CharacterKind.hard_Asuka:
                Audio.PlayOneShot(attack0005);
                break;
            case CharacterKind.kogi:
                Audio.PlayOneShot(attack0006);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(chara), chara, null);
        }
    }

    public void PlayJingle(bool hit = false)
    {
        Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(hit ? Jingle0004 : Jingle0003);
    }

    public void PlayTitle()
    {
        Audio = GetComponent<AudioSource>();
        Audio.PlayOneShot(Jingle0001);
    }

    public void playPush(int num)
    {
        Audio = GetComponent<AudioSource>();
        switch (num)
        {
            case 1:
                Audio.PlayOneShot(UISE_0001);
                break;
            case 2:
                Audio.PlayOneShot(UISE_0002);
                break;
            case 3:
                Audio.PlayOneShot(UISE_0003);
                break;
        }
    }

    public void PlayResult(bool isWin)
    {
        Audio.PlayOneShot(isWin ? Jingle0006 : Jingle0005);
    }

    public void PlayOtetsuki(bool first)
    {
        if (first)
        {
            Audio.PlayOneShot(Jingle0007);
        }
        else
        {
            Audio.PlayOneShot(Jingle0008);
        }
    }


}
