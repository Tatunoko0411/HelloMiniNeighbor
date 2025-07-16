using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] LayerMask blockLayer;

    Rigidbody2D rb;

    [SerializeField] GameManager gameManager;

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
        direction = DIRECTION_TYPE.STOP;
    }

    // Update is called once per frame
    void Update()
    {

        if(isWall())
        {
            if (direction == DIRECTION_TYPE.RIGHT)
            {
                direction = DIRECTION_TYPE.LEFT;
                transform.localScale = new Vector3(-0.27f, 0.27f, 0.27f);
            }
            else if (direction == DIRECTION_TYPE.LEFT)
            {
                direction= DIRECTION_TYPE.RIGHT;
                transform.localScale = new Vector3(0.27f, 0.27f, 0.27f);

            }
        }
        
    }

    private void FixedUpdate()
    {
        if ((!gameManager.isStart))
        {
            rb.velocity = Vector3.zero;
            return;
        }

        if (direction == DIRECTION_TYPE.RIGHT)
        {
            rb.velocity = Vector2.right * speed;
        }
        else if (direction == DIRECTION_TYPE.LEFT)
        {
            rb .velocity = Vector2.left * speed;
        }
        

        
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
}
