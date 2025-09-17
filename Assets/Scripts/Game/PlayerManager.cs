using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;

    public Rigidbody2D rb;
    CapsuleCollider2D capsuleCollider2D;

    [SerializeField] GameManager gameManager;

    [SerializeField] public GameObject LimBox;

    public Vector2 StartPos;

    public float JumpPower;//ジャンプの強さ
    public float JumpSpeedLate;//ジャンプの推進力

    public float MaxSpeed;

    public float groundSpeed;//地上のスピード
    public float junpSpeed;//空中のスピード

    public bool isDead;//死亡判定

    public bool createMode;

    public enum DIRECTION_TYPE
    {
        RIGHT,
        LEFT,
        STOP
    };


   public DIRECTION_TYPE direction;
   public float speed;

   public bool isDorp;//落下状態

    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        direction = DIRECTION_TYPE.STOP;
        isDead = false;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        animator.SetBool("isGround", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(rb.bodyType == RigidbodyType2D.Static)
        {
            transform.position = StartPos;
        }

        animator.SetFloat("Velocity",Math.Abs( rb.velocity.x));

        if (createMode)
        {
            transform.position = LimBox.transform.position;
        
            return;
        }

        //死亡時は行動できない
        if (isDead) return;

        if(isWall())
        {
            if (direction == DIRECTION_TYPE.RIGHT)
            {
                direction = DIRECTION_TYPE.LEFT;
               
            }
            else if (direction == DIRECTION_TYPE.LEFT)
            {
                direction= DIRECTION_TYPE.RIGHT;
               

            }
        }



        move();
  

    }

    private void move()
    {

        if (isGlound())
        {
            isDorp = false;

           animator.SetBool("isGround", true);

            if (direction == DIRECTION_TYPE.RIGHT)
            {
               rb.velocity = new Vector2 (groundSpeed,rb.velocity.y);
                transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);
                
            }
            else if (direction == DIRECTION_TYPE.LEFT)
            {
              

                rb.velocity = new Vector2(-groundSpeed, rb.velocity.y);
                transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
            }
        }
        else
        {
            animator.SetBool("isGround", false);

            if(isDorp)
            { return; }

            if (direction == DIRECTION_TYPE.RIGHT)
            {
                rb.velocity = new Vector2(junpSpeed, rb.velocity.y);
                transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);
            }
            else if (direction == DIRECTION_TYPE.LEFT)
            {
                //rb.velocity = new Vector2(rb.velocity.x - (speed * SpLate), rb.velocity.y);

                rb.velocity = new Vector2(-junpSpeed, rb.velocity.y);
                transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
            }
        }

      

    
    }

    public bool isWall()
    {
        Vector3 BasePos = new Vector3(transform.position.x - 0.7f,transform.position.y,transform.position.z);
        Vector3 UpStartpoint = BasePos + Vector3.up * 0.8f;
        Vector3 DownStartpoint = BasePos + Vector3.down * 0.8f;
 

        if (direction == DIRECTION_TYPE.LEFT)
        {
            BasePos = new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z);
             UpStartpoint = BasePos + Vector3.up * 0.8f;
             DownStartpoint = BasePos + Vector3.down * 0.8f;

        }
        else if(direction == DIRECTION_TYPE.RIGHT)
        {
            BasePos = new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z);
            UpStartpoint = BasePos + Vector3.up * 0.8f;
            DownStartpoint = BasePos + Vector3.down * 0.8f;

        }

        Debug.DrawLine(UpStartpoint, DownStartpoint);

        if(!Physics2D.Linecast(UpStartpoint, DownStartpoint, blockLayer))
        {
            return false;
        }

        GameObject gameObject = Physics2D.Linecast(UpStartpoint, DownStartpoint, blockLayer).collider.gameObject;

        if (gameObject.tag == "Book")
        {
            return true;
        }

        return false;
    }

    private bool isGlound()
    {
        Vector3 BasePos = new Vector3(transform.position.x , transform.position.y - 1f, transform.position.z);
        Vector3 UpStartpoint = BasePos + Vector3.left * 0.1f;
        Vector3 DownStartpoint = BasePos + Vector3.right * 0.1f;
        Vector3 EndPos = BasePos + Vector3.down * 0.05f;


        Debug.DrawLine(UpStartpoint, EndPos);
        Debug.DrawLine(DownStartpoint, EndPos);

      

        return Physics2D.Linecast(UpStartpoint, EndPos, blockLayer)
            || Physics2D.Linecast(DownStartpoint, EndPos, blockLayer);
    }

    public void Jump()
    {
       rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * JumpPower);

        rb.AddForce(new Vector3(speed*JumpSpeedLate, 0f,0));
    }

    public void StartPlayer()
    {
        direction = PlayerManager.DIRECTION_TYPE.RIGHT;
       rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = new Vector3(0.5f, 0f, 0f);
        LimBox.SetActive(false);
        capsuleCollider2D.enabled = true;
    }

    public void StopPlayer()
    {
        direction = PlayerManager.DIRECTION_TYPE.STOP;
        rb.bodyType = RigidbodyType2D.Static;
        LimBox.SetActive(true);
        transform.position = StartPos;
        isDead = false;
    }

    public void Dead()
    {
        Debug.Log("死亡");
        capsuleCollider2D.enabled = false;
        isDead = true;
        animator.SetBool("Death", true);
    }

    public void OffDeathAnim()
    {
        animator.SetBool("Death", false);
    }

    //プレイヤーの初期化
    public void InitPlayer()
    {
        if (createMode == false)
        {
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            capsule.enabled = true;

            rb.gravityScale = 1f;
        }
        else if (createMode == true)
        {
            CapsuleCollider2D capsule = GetComponent<CapsuleCollider2D>();
            capsule.enabled = false;

            rb.gravityScale = 0f;
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "goal")
        {
            if (!isDead)
            {
                Debug.Log("ゴール");
                gameManager.isClear = true;
                rb.velocity = Vector3.zero;
                enabled = false;
            }
        }

        if(collision.gameObject.tag == "water")
        {
            rb.velocity = Vector3.zero;
            isDorp = true;
        }


        if (collision.gameObject.tag == "JumpPad")
        {
            Jump();
        }

        if (collision.gameObject.tag == "Trap")
        {
            Dead();
            
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Bubble")
        {//泡の中にいる時は平行移動のみ
            rb.velocity = new Vector2(rb.velocity.x, 0.1f);
        }
    }

}
