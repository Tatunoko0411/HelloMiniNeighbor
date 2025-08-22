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
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
        direction = DIRECTION_TYPE.STOP;
        isDead = false;
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

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

        if(isGlound())
        {
            move();
        }

        if (rb.velocity.x >= MaxSpeed)
        {
            rb.velocity = new Vector2(MaxSpeed, rb.velocity.y);

        }
        if (rb.velocity.x <= -MaxSpeed)
        {
            rb.velocity = new Vector2(-MaxSpeed, rb.velocity.y);

        }
        if (rb.velocity.y <= -22)
        {
            rb.velocity = new Vector2(rb.velocity.x, -22);

        }

    }

    private void move()
    {


        if ((!gameManager.isStart))
        {
            rb.velocity = Vector3.zero;
            return;
        }

       // float SpLate = speed -(rb.velocity.x / speed);

        if (direction == DIRECTION_TYPE.RIGHT)
        {
            rb.AddForce(new Vector2(speed, 0));
            transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            //rb.velocity = new Vector2(rb.velocity.x - (speed * SpLate), rb.velocity.y);

            rb.AddForce(new Vector2(-speed, 0));
            transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
        }

        //rb.velocity = new Vector2(rb.velocity.x - rb.velocity.x * 0.05f, rb.velocity.y);

    
    }

    public bool isWall()
    {
        Vector3 BasePos = new Vector3(transform.position.x - 0.5f,transform.position.y,transform.position.z);
        Vector3 UpStartpoint = BasePos - Vector3.up * 0.1f;
        Vector3 DownStartpoint = BasePos + Vector3.down * 0.1f;
 

        if (direction == DIRECTION_TYPE.LEFT)
        {
            BasePos = new Vector3(transform.position.x - 0.7f, transform.position.y, transform.position.z);
            UpStartpoint = BasePos + Vector3.up * 0.1f;
            DownStartpoint = BasePos + Vector3.down * 0.1f;

        }
        else if(direction == DIRECTION_TYPE.RIGHT)
        {
            BasePos = new Vector3(transform.position.x + 0.7f, transform.position.y, transform.position.z);
            UpStartpoint = BasePos + Vector3.up * 0.1f;
            DownStartpoint = BasePos + Vector3.down * 0.1f;
  
        }

        Debug.DrawLine(UpStartpoint, BasePos);
        Debug.DrawLine(DownStartpoint, BasePos);

        return Physics2D.Linecast(UpStartpoint, BasePos, blockLayer)
            || Physics2D.Linecast(DownStartpoint, BasePos, blockLayer);
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "goal")
        {
            Debug.Log("ゴール");
            gameManager.isClear = true;
            rb.velocity = Vector3.zero; 
            enabled = false;
        }

        if(collision.gameObject.tag == "water")
        {
            rb.velocity = Vector3.zero;
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

}
