﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Player : DolObject
{
    Vector2 spawnPos;
    AudioManager audioManager;
    GameManager gameManager;

    public bool portalContact;
    int player, contactPlayer;
    public static bool twoPlayerEnter;

  void Start()
  {
        twoPlayerEnter = false;
        this.gameObject.SetActive(true);
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (this.gameObject.name == "Player 1") player = 1;
        else if (this.gameObject.name == "Player 2") player = 2;

        Init();

        GameObject audioManagerObject = GameObject.Find("AudioManager");
        
        if (audioManagerObject)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
  }

  protected override void OnCollisionEnter2D(Collision2D collision)
  {
    base.OnCollisionEnter2D(collision);

    switch (collision.gameObject.tag)
    {
      case "Enemy":
                GameManager.Instance.starCount = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                GameManager.Instance.SetIsReloading(true);
                GameManager.Instance.SetCurrentStageName(GameManager.Instance.GetCurrentStageName());
        //gameObject.transform.position = spawnPos;
        break;

        //case "Star":
        //  GameManager.Instance.starCount += 1;
        //  break;

        //case "Key":
        //  GameManager.Instance.keyCount += 1;
        //  break;

        //case "Floor":

        //  break;
    }
  }

  private void OnTriggerEnter2D(Collider2D collision)
  {
    switch (collision.gameObject.tag)
    {
      case "Finish":
        Scene scene = SceneManager.GetActiveScene();

        if (scene.buildIndex == 0)      // 메인화면인 경우, 챕터선택화면으로 넘김
        {
          GameObject.Find("Canvas").transform.Find("S").gameObject.SetActive(true);
                    contactPlayer = player;
                    Debug.Log(contactPlayer);
        }
        else
        {
          portalContact = true;
        }
        break;

      case "Star":
        audioManager.SfxPlay(player, 2);
        GameManager.Instance.starCount += 1;

        if (UIManger.Instance)
        {
          UIManger.Instance.SetStarUI(GameManager.Instance.starCount);
        }

        break;

      case "Key":
        audioManager.SfxPlay(player, 5);
        GameManager.Instance.keyCount += 1;

        // 열쇠 개수 UI 갱신
        UIManger.Instance.SetKeyNumber(GameManager.Instance.keyCount);

        break;

        }
    }

private void OnTriggerExit2D(Collider2D collision)
{
  portalContact = false;
  if (SceneManager.GetActiveScene().buildIndex == 0)
  {
    GameObject s = GameObject.Find("S");
    if (s)
    {
      s.gameObject.SetActive(false);
    }
  }
}

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.S) && SceneManager.GetActiveScene().buildIndex == 0)
    {
            if (GameObject.Find("Canvas").transform.Find("S").gameObject.activeSelf == true && this.gameObject.name == ("Player " + contactPlayer.ToString()))
            {
                if (twoPlayerEnter == false)
                {
                    audioManager.SfxPlay(Mathf.Abs(player-1), 4);
                    twoPlayerEnter = true;
                    this.gameObject.SetActive(false);
                    
                    gameManager.charChoice = !gameManager.charChoice;
                }
                else
                {
                    Debug.Log(this.gameObject.name + " " + twoPlayerEnter);
                    audioManager.SfxPlay(player, 4);
                    twoPlayerEnter = false;
                    SceneManager.LoadScene("ChapterSelect");
                }
                
            }

    }
    else if (Input.GetKeyDown(KeyCode.S) && portalContact == true && GameManager.Instance.starCount > 0)
    {
            if (twoPlayerEnter == false)
            {
                audioManager.SfxPlay(Mathf.Abs(player - 1), 4);
                GameObject.Find("Player " + player.ToString()).SetActive(false);
                twoPlayerEnter = true;
                gameManager.charChoice = !gameManager.charChoice;
            }
            else
            {
                audioManager.SfxPlay(player, 4);
                twoPlayerEnter = false ;
                Debug.Log("Before " + GameManager.Instance.starCount + " " + ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1]);
                ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1] = GameManager.Instance.starCount;
                GameManager.Instance.starCount = 0;
                if (SceneManager.GetActiveScene().buildIndex == SceneManager.sceneCount - 1)           // 챕터의 마지막 스테이지인 경우
                {
                    Debug.Log("After " + GameManager.Instance.starCount + " " + SceneManager.GetActiveScene().buildIndex);
                    SceneManager.LoadScene("StageSelect");
                }
                else
                {
                    Debug.Log(GameManager.Instance.starCount + " " + ScoreManagement.clear[ScoreManagement.currentChapter - 1].stageStar[ScoreManagement.currentStage - 1]);
                    ScoreManagement.currentStage += 1;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    GameManager.Instance.SetCurrentStageName(ScoreManagement.currentChapter.ToString() + "-" + ScoreManagement.currentStage);
                    GameManager.Instance.SetIsReloading(true);
                }
            }
    }
  }

  public void SetSpawnPos(Vector2 spawnPos)
  {
    this.spawnPos = spawnPos;
  }

  public Vector2 GetSpawnPos()
  {
    return this.spawnPos;
  }
}