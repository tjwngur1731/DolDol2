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
    public AudioSource[] SFXAudioSource = new AudioSource[3];                       //        (0: 버튼, 1: 플레이어1, 2:플레이어2)
    public AudioClip[] bgm = new AudioClip[2];                                      //        (0: 메인화면, 1: 플레이화면)
    public AudioClip[] sfx = new AudioClip[6];      // 챌린지 이후 9개로 늘리기               (0: 버튼, 1: 점프, 2: 별, 3: 회전, 4: 포탈, 5: 열쇠)

    public int bgmNum;
    public int sceneIndex;
    public int pastSceneIndex;

    public Slider bgmSlider; //bgm 슬라이더
    public Slider sfxSlider; //sfx 슬라이더

    public static float bgmVol = 1.0f; //bgm 슬라이더 값을 유지하기 위한 변수
    public static float sfxVol = 1.0f; //sfx 슬라이더 값을 유지하기 위한 변수

    public static float pastBVol;
    public static float pastSVol;

    public static AudioManager instance;

    private void Awake()
    {
        bgmNum = 1;
        pastSceneIndex = 0;
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
        BGMAudioSource.volume = PlayerPrefs.GetFloat("bgmvol"); //bgm 볼륨 값 적용
        for (int i = 0; i<SFXAudioSource.Length; i++)
            SFXAudioSource[i].volume = PlayerPrefs.GetFloat("sfxvol"); //sfx 볼륨 값 적용
        sfxVol = PlayerPrefs.GetFloat("sfxvol", 1.0f);
        bgmVol = PlayerPrefs.GetFloat("bgmvol", 1.0f); //"bgmvol"이 비어있을 경우 1.0
        pastBVol = bgmVol;
        pastSVol = sfxVol;
    }

    private void Start()
    {
        
        bgmSlider.value = bgmVol;
        BGMAudioSource.volume = bgmSlider.value;
        sfxSlider.value = sfxVol;
        for (int i = 0; i < SFXAudioSource.Length; i++)
            SFXAudioSource[i].volume = PlayerPrefs.GetFloat("sfxvol"); //sfx 볼륨 값 적용

    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex != 1 && SceneManager.GetActiveScene().buildIndex != 2 && sceneIndex != SceneManager.GetActiveScene().buildIndex)
        {
            if (SceneManager.GetActiveScene().buildIndex == 0)
            {
                this.bgmSlider = GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/BGM").GetComponent<Slider>();
                this.sfxSlider = GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/SFX").GetComponent<Slider>();
                bgmSlider.value = bgmVol;
                sfxSlider.value = sfxVol;
                sceneIndex = 0;
            }
            else if (SceneManager.GetActiveScene().buildIndex > 2)
            {
                this.bgmSlider = GameObject.Find("Canvas").transform.Find("Pause/Option_Window/Sound/BGM").GetComponent<Slider>();
                this.sfxSlider = GameObject.Find("Canvas").transform.Find("Pause/Option_Window/Sound/SFX").GetComponent<Slider>();
                bgmSlider.value = bgmVol;
                sfxSlider.value = sfxVol;
                sceneIndex = SceneManager.GetActiveScene().buildIndex;

            }                                                                                                                                                           // 슬라이더 찾아서 적용, 기존 볼륨 저장
        }
        else
            pastSceneIndex = SceneManager.GetActiveScene().buildIndex;

        if(sceneIndex != pastSceneIndex)            // if Scene Change
        {
            SFXAudioSource[0] = GameObject.Find("Button_Sound").GetComponent<AudioSource>() ;           // 버튼 오디오 소스 적용
            SFXAudioSource[0].volume = sfxVol;
            Debug.Log("I'm here!");
            if (sceneIndex > 2 || sceneIndex == 0)
            {
                SFXAudioSource[1] = GameObject.Find("Player 1").GetComponent<AudioSource>();
                SFXAudioSource[2] = GameObject.Find("Player 2").GetComponent<AudioSource>();             // 플레이어 오디오 소스 적용
                for (int i = 1; i < SFXAudioSource.Length; i++)
                    SFXAudioSource[i].volume = sfxVol;
            }
            
            sfxSlider.value = sfxVol;
            bgmSlider.value = bgmVol;
            
            BGMAudioSource.volume = bgmVol;

            pastSceneIndex = sceneIndex;
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
                    GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/BGM_Box/Text").GetComponent<Text>().text = ((int)(bgmSlider.value * 100)).ToString();
                    GameObject.Find("Canvas/Field").transform.Find("Option_Window/Sound/SFX_Box/Text").GetComponent<Text>().text = ((int)(sfxSlider.value * 100)).ToString();
                }
            }
            else if (SceneManager.GetActiveScene().buildIndex > 2)
            {
                if (GameObject.Find("Canvas/Pause").activeSelf == true)
                {
                    SoundSlider();
                    GameObject.Find("Canvas/Pause").transform.Find("Option_Window/Sound/BGM_Box/Text").GetComponent<Text>().text = ((int)(bgmSlider.value * 100)).ToString();
                    GameObject.Find("Canvas/Pause").transform.Find("Option_Window/Sound/SFX_Box/Text").GetComponent<Text>().text = ((int)(sfxSlider.value * 100)).ToString();
                }
            }
        }
        catch(NullReferenceException) { }
        
        

    }

    public void SoundSlider()
    {
        BGMAudioSource.volume = bgmSlider.value; //슬라이더 값을 오디오소스의 volume에 대입
        for (int i = 0; i < SFXAudioSource.Length; i++)
            SFXAudioSource[i].volume = sfxSlider.value;
        // AudioSourceName.volume = SFxVolume.value;

        bgmVol = bgmSlider.value;
        sfxVol = sfxSlider.value;
        PlayerPrefs.SetFloat("bgmvol", bgmVol); //"bgmvol"이라는 키에 bgmVol 저장
        PlayerPrefs.SetFloat("sfxvol", sfxVol);
    }

    void BGM()
    {
        if(SceneManager.GetActiveScene().buildIndex < 3 && bgmNum == 2)  // 메인화면, 챕터선택화면, 스테이지선택화면의 파일인 경우
        {
            this.gameObject.GetComponent<AudioSource>().clip = bgm[0];
            bgmNum = 1;
            BGMAudioSource.Play();
        }
        else if(SceneManager.GetActiveScene().buildIndex > 2 && bgmNum == 1)// 플레이 화면의 오디오매니저 인 경우
        {
            this.gameObject.GetComponent<AudioSource>().clip = bgm[1];
            bgmNum = 2;
            BGMAudioSource.Play();
        }
    }

    public void SfxPlay(int who, int index)            // who : AudioSource, index : AudioClip
    {
        SFXAudioSource[who].clip = sfx[index];
        SFXAudioSource[who].Play();
    }

    public void OnClickCancel()
    {
        bgmVol = pastBVol;
        sfxVol = pastSVol;
        bgmSlider.value = bgmVol;
        sfxSlider.value = sfxVol;
    }
}
