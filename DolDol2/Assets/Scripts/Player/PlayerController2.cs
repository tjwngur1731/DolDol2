using System.Collections;
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

    public float runDelay;
    float curDelay;

    bool runCount;
    public Sprite[] runSprite;

    public Sprite[] jumpSprite;
    public Sprite staySprite;

    //움직이는 중인가.
    bool isrunnig = false;
    bool isLanding;
    bool isWall;
    bool isDoubleJump;

    float landingTime;
    float jumpingTime;

    int jumpingCnt;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        rigid = GetComponent<Rigidbody2D>();

        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if (GameManager.Instance.GetIsRotating() == true)
        {
            return;
        }

        if (isrunnig == true)
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
                curDelay = 0;
            }
        }
        //바닥체크 점프
        isGround = Physics2D.OverlapCircle(pos.position, checkRadius, islayer);

        if (!isGround)
        {
            jumpingTime += Time.deltaTime;
            if (jumpingTime > 0.2f && jumpingCnt < 2)
            {
                jumpingCnt++;
                jumpingTime = 0;
            }
            renderer.sprite = jumpSprite[jumpingCnt];
            landingTime = 0;
        }
        else if (Input.GetKey(KeyCode.A) && GameManager.Instance.charChoice == true)
        {
            isrunnig = true;
            renderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.D) && GameManager.Instance.charChoice == true)
        {
            isrunnig = true;
            renderer.flipX = true;
        }
        else if (isGround)
        {
            landingTime += Time.deltaTime;
            if (landingTime < 0.1f)
            {
                renderer.sprite = jumpSprite[3];
            }
            else
            {
                renderer.sprite = staySprite;
            }
            isDoubleJump = false;
        }
        else
        {
            isrunnig = false;
        }
    }

    void FixedUpdate()
    {
        if (GameManager.Instance.GetIsRotating() == true)
        {
            audioManager.SfxPlay(1, 3);
            return;
        }

        Vector2 position = transform.position;
        //좌우 이동, 점프
        if (GameManager.Instance.charChoice == true)
        {
            float hor = Input.GetAxis("Horizontal");

            rigid.velocity = new Vector2(hor * speed, rigid.velocity.y);

            if (Input.GetKey(KeyCode.W) && (isGround || isWall) && !isDoubleJump)
            {

                audioManager.SfxPlay(1, 1);
                rigid.velocity = Vector2.up * jumpPower;
                jumpingCnt = 0;

                //Debug.Log("JUMP");

                if (!isGround)
                {
                    isDoubleJump = true;
                }
            }
        }
        transform.position = position;
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        bool collisionDolObj = collision.gameObject.CompareTag("Floor");

        if (collisionDolObj && !isDoubleJump && !isGround)
        {
            isWall = true;
        }
    }
}
