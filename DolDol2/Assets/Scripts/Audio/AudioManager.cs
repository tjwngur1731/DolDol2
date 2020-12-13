using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// 사운드 관련 파일

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMAudioSource;
    public AudioSource[] SFXAudioSource; // = new AudioSource[4];
    public int bgmNum;
    public int sceneIndex;
    //public AudioSource Button;
    //public AudioSource caughtd;
    //public AudioSource fever;

    public Slider bgmVolume; //bgm 슬라이더
    public Slider SFXVolume; //sfx 슬라이더

    public static float bgmVol = 1.0f; //bgm 슬라이더 값을 유지하기 위한 변수
    public static float sfxVol = 1.0f; //sfx 슬라이더 값을 유지하기 위한 변수

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else if (instance != this)
        {
            Destroy(this.gameObject);
        }
        bgmNum = 1;

    }

    private void Start()
    {
        BGMAudioSource.volume = PlayerPrefs.GetFloat("bgmvol"); //bgm 볼륨 값 적용
        for (int i = 0; i<SFXAudioSource.Length; i++)
            SFXAudioSource[i].volume = PlayerPrefs.GetFloat("sfxvol"); //sfx 볼륨 값 적용


        bgmVol = PlayerPrefs.GetFloat("bgmvol", 1.0f); //"bgmvol"이 비어있을 경우 1.0
        bgmVolume.value = bgmVol;
        BGMAudioSource.volume = bgmVolume.value;

        sfxVol = PlayerPrefs.GetFloat("sfxvol", 1.0f);
        //AudioSourceName.volume = sfxVol;
    }

    void Update()
    {
        if(SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2 && sceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                this.bgmVolume = GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/BGM").GetComponent<Slider>();
                this.SFXVolume = GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/SFX").GetComponent<Slider>();
                sceneIndex = 0;
            }
            else if (SceneManager.GetActiveScene().buildIndex > 2)                                                               
            {
                this.bgmVolume = GameObject.Find("Canvas").transform.Find("Pause/Option_Window/Sound/BGM").GetComponent<Slider>();
                this.SFXVolume = GameObject.Find("Canvas").transform.Find("Pause/Option_Window/Sound/SFX").GetComponent<Slider>();
                sceneIndex = SceneManager.GetActiveScene().buildIndex;
                
            }
        }
        // IF문으로  this.SFXAudioSource.Play(); //sfx 플레이
                                           // 취소버튼 작동 X
        if (this.gameObject.name == "AudioManager")
            BGM();
        try
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                if (GameObject.Find("Canvas/Field/Option_Window/Sound").activeSelf == true)          // 볼륨 슬라이더 텍스트 변경
                {
                    SoundSlider();
                    GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/BGM_Box/Text").GetComponent<Text>().text = ((int)(bgmVolume.value * 100)).ToString();
                    GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/SFX_Box/Text").GetComponent<Text>().text = ((int)(SFXVolume.value * 100)).ToString();
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex > 2)
            {
                if (GameObject.Find("Canvas/Pause").activeSelf == true)
                {
                    SoundSlider();
                    GameObject.Find("Canvas/Pause").transform.Find("Option_Window/Sound/BGM_Box/Text").GetComponent<Text>().text = ((int)(bgmVolume.value * 100)).ToString();
                    GameObject.Find("Canvas/Pause").transform.Find("Option_Window/Sound/SFX_Box/Text").GetComponent<Text>().text = ((int)(SFXVolume.value * 100)).ToString();
                }
            }
        }
        catch(NullReferenceException) { }
        
        

    }

    public void SoundSlider()
    {
        BGMAudioSource.volume = bgmVolume.value; //슬라이더 값을 오디오소스의 volume에 대입
        for (int i = 0; i < SFXAudioSource.Length; i++)
            SFXAudioSource[i].volume = SFXVolume.value;
        // AudioSourceName.volume = SFxVolume.value;

        bgmVol = bgmVolume.value;
        sfxVol = SFXVolume.value;
        PlayerPrefs.SetFloat("bgmvol", bgmVol); //"bgmvol"이라는 키에 bgmVol 저장
        PlayerPrefs.SetFloat("sfxvol", sfxVol);
    }

    void BGM()
    {
        if(SceneManager.GetActiveScene().buildIndex < 3 && bgmNum == 2)  // 메인화면, 챕터선택화면, 스테이지선택화면의 파일인 경우
        {
            Debug.Log("I'm here!");
            this.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/#353 Starry Night - FLiCo");
            bgmNum = 1;
            BGMAudioSource.Play();
        }
        else if(SceneManager.GetActiveScene().buildIndex > 2 && bgmNum == 1)// 플레이 화면의 오디오매니저 인 경우
        {
            this.gameObject.GetComponent<AudioSource>().clip = Resources.Load<AudioClip>("Sound/PerituneMaterial_Positive_Happy");
            bgmNum = 2;
            BGMAudioSource.Play();
        }
    }
}
