﻿using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rigid;
    [SerializeField]
    private float speed = 1.0f;
    public float jumpPower = 1.0f;
    [SerializeField]
    private bool isGround = false;
    [SerializeField]
    Transform pos;
    [SerializeField]
    Player player;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask islayer;

    SpriteRenderer renderer;
    public static bool raycast;

    public float runDelay;
    float curDelay;

    bool runCount;
    public Sprite[] runSprite;

    public Sprite[] jumpSprite;
    public Sprite staySprite;

    //움직이는 중인가.
    bool isrunning = false;
    bool isLanding;
    bool isWall;
    bool isDoubleJump;
    bool onPlayer = false;
    bool moveLeft;

    float landingTime;
    float jumpingTime;

    int jumpingCnt;
    // Start is called before the first frame update
    void Start()
    {
        GameObject audioManagerObject = GameObject.Find("AudioManager");
        
        if (audioManagerObject)
        {
            audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }
        
        rigid = GetComponent<Rigidbody2D>();

        renderer = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        if (GameManager.Instance.GetIsRotating() == true)
        {
            return;
        }


        //바닥체크 점프
        isGround = Physics2D.OverlapPoint(new Vector2(this.gameObject.transform.position.x, gameObject.transform.position.y - 0.45f), islayer) || onPlayer;
        isWall = Physics2D.OverlapPoint(new Vector2(this.gameObject.transform.position.x - 0.35f, gameObject.transform.position.y), islayer) || Physics2D.OverlapPoint(new Vector2(this.gameObject.transform.position.x + 0.35f, gameObject.transform.position.y), islayer);

        if (this.gameObject.GetComponent<Rigidbody2D>().velocity == Vector2.zero)
        {
            renderer.sprite = staySprite;
        }

        if (!isGround)
        {
            if (isrunning == true)
            {
                curDelay += Time.deltaTime;
                if (curDelay >= runDelay)
                {
                    if (runCount)
                    {
                        runCount = !runCount;
                        renderer.sprite = runSprite[0];
                        Debug.Log("I am here");
                    }
                    else if (!runCount)
                    {
                        runCount = !runCount;
                        renderer.sprite = runSprite[1];
                    }
                    if (moveLeft)
                    {
                        renderer.flipX = false;
                    }
                    else
                        renderer.flipX = true;
                    curDelay = 0;

                }
            }

            jumpingTime += Time.deltaTime;
            if (jumpingTime > 0.2f && jumpingCnt < 2)
            {
                jumpingCnt++;
                jumpingTime = 0;
            }
            renderer.sprite = jumpSprite[jumpingCnt];
            landingTime = 0;

            if (Input.GetKey(KeyCode.A) && GameManager.Instance.charChoice == false)
            {
                isrunning = true;
                moveLeft = true;
            }
            else if (Input.GetKey(KeyCode.D) && GameManager.Instance.charChoice == false)
            {
                isrunning = true;
                moveLeft = false;
            }
        }
        else if (isGround)
        {
            landingTime += Time.deltaTime;
            if (landingTime < 0.1f && landingTime != 0)
            {
                renderer.sprite = jumpSprite[3];
            }


            if (isrunning == true)
            {
                curDelay += Time.deltaTime;
                if (curDelay >= runDelay)
                {
                    if (runCount)
                    {
                        runCount = !runCount;
                        renderer.sprite = runSprite[0];
                    }
                    else if (!runCount)
                    {
                        runCount = !runCount;
                        renderer.sprite = runSprite[1];
                    }
                    if (moveLeft)
                    {
                        renderer.flipX = false;
                    }
                    else
                        renderer.flipX = true;
                    curDelay = 0;

                }
            }
            else
                renderer.sprite = staySprite;

            isDoubleJump = false;

            if (Input.GetKey(KeyCode.A) && GameManager.Instance.charChoice == false)
            {
                isrunning = true;
                moveLeft = true;
            }
            else if (Input.GetKey(KeyCode.D) && GameManager.Instance.charChoice == false)
            {
                isrunning = true;
                moveLeft = false;
            }
            else
            {
                isrunning = false;
                this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }

        }
        else
        {
            isrunning = false;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GetIsRotating() == true)
        {
            if (audioManager)
            {
              audioManager.SfxPlay(1, 3);
            }
            return;
        }

        Vector2 position = transform.position;
        //좌우 이동, 점프
        if (GameManager.Instance.charChoice == false)
        {
            float hor = Input.GetAxis("Horizontal");

            rigid.velocity = new Vector2(hor * speed, rigid.velocity.y);


            if (Input.GetKeyDown(KeyCode.W) && (isGround))
            {
                if (audioManager)
                {
                  audioManager.SfxPlay(1, 1);
                }
                rigid.velocity = Vector2.up * jumpPower;
                jumpingCnt = 0;
                isDoubleJump = true;
                isGround = false;
            }
            else if (Input.GetKeyDown(KeyCode.W) && (isWall) && isDoubleJump)
            {
                if (audioManager)
                {
                  audioManager.SfxPlay(1, 1);
                }
                rigid.velocity = Vector2.up * jumpPower;
                jumpingCnt = 0;
                isDoubleJump = false;
                isGround = false;
            }
        }
        transform.position = position;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isDoubleJump = true;
            onPlayer = true;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        onPlayer = false;
    }
}