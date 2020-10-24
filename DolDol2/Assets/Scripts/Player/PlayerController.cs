using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigid;
    [SerializeField]
    private float speed = 1.0f;
    public float jumpPower = 1.0f;
    [SerializeField]
    private bool isGround = false;
    [SerializeField]
    Transform pos;
    [SerializeField]
    float checkRadius;
    [SerializeField]
    LayerMask islayer;

    SpriteRenderer renderer;

    public float runDelay;
    float curDelay;

    bool runCount;
    public Sprite[] runSprite;

    public Sprite jumpSprite;
    public Sprite staySprite;

    //움직이는 중인가.
    bool isrunnig = false;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();

        renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
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
            renderer.sprite = jumpSprite;
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
        else if(isGround)
        {
            renderer.sprite = staySprite;
        }
        else
        {
            isrunnig = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        //좌우 이동, 점프
        if (GameManager.Instance.charChoice == true)
        {
            float hor = Input.GetAxis("Horizontal");

            rigid.velocity = new Vector2(hor * speed, rigid.velocity.y);

            if (Input.GetKey(KeyCode.W) && isGround == true)
            {
                rigid.velocity = Vector2.up * jumpPower;
            }
        }
        transform.position = position;
    }
}
