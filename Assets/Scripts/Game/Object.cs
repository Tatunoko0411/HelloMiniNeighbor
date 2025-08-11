using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static PlayerManager;

public class Object : MonoBehaviour
{
    public int id;

    static float outlineDistance = 0.075f;//判定の外側に対する距離
    public int cost;

    bool isDelete;
   public  bool isFixed;
    
    GameManager gameManager;
    [SerializeField] LayerMask ObjectLayer;

    public bool isDrag;

   public Rigidbody2D rb;

    /// ステージクリエイト系
    public int SetButtonID = -1;
    StageCreateManager stageCreateManager;
    public bool CreateMode;
    private int StageObjectID;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetButtonID = -1;

        if (CreateMode)
        {
            stageCreateManager = GameObject.Find("StageCreateManager").GetComponent<StageCreateManager>();
        }
        else
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (!CreateMode)
        {
            if (gameManager.isStart)
            {//ゲーム実行中は移動させない
                return;
            }
        }
        isRight();
        isLeft();
        isUp();
        isDown();
        if (!Input.GetMouseButton(0))
        {
            isDrag = false;
            rb.bodyType = RigidbodyType2D.Static;
            if (!CreateMode)
            {
                gameManager.Draging = false;
            }
            else if (CreateMode)
            {
                if (StageObjectID == 0)
                {
                    StageObjectID = stageCreateManager.StageObjectList.Count + 1;

                    stageCreateManager.StageObjectList.Add(new StageObject(StageObjectID,
                        transform.position.x,
                        transform.position.y,
                        transform.rotation.z));

     
                }

            }
        }

        if (isDrag)
        {
   
           Vector2 mousePos = Input.mousePosition;

          Vector2  worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));


            Vector2 movePos = Vector2.MoveTowards(
                transform.position,
                worldPos,
                0.1f );

            if(isRight())
            {
                if(transform.position.x < movePos.x)
                {
                    movePos.x = transform.position.x;
                }

            }
            if(isLeft())
            {
                if (transform.position.x > movePos.x)
                {
                    movePos.x = transform.position.x;
                }
            }
            if(isUp())
            {
                if (transform.position.y < movePos.y)
                {
                    movePos.y = transform.position.y;
                }
            }
            if(isDown())
            {
                if (transform.position.y > movePos.y)
                {
                    movePos.y = transform.position.y;
                }
            }

          //この後壁判定付けて移動制限

            transform.position = movePos;

        }
        else
        {
            if(isDelete && !isFixed)
            {//常設オブジェクトは削除されない
                Destroy(this.gameObject);

                if (!CreateMode)
                {
                    gameManager.changePoint(cost);
                }
                enabled = false;
                return;
            }

            if(CreateMode)
            {
                if(SetButtonID>=0)
                {
                    stageCreateManager.ButtonList[SetButtonID].PopObjectPrefab
                        = stageCreateManager.ObjectList[id].GetComponent<Object>();
                    Destroy(this.gameObject);   
                }
            }

        }
    }

    private void OnMouseDrag()
    {
        if(isFixed)
        {//常設オブジェクトは選択できない
            return;
        }
        isDrag = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        if (!CreateMode)
        {
            gameManager.Draging = true;
        }
    }

    private void OnMouseUp()
    {
        isDrag = false;
        rb.bodyType = RigidbodyType2D.Static;

        if (!CreateMode)
        {
            gameManager.Draging = false;
        }

        if (CreateMode)
        {


            if (StageObjectID == 0)
            {
                StageObjectID = stageCreateManager.StageObjectList.Count + 1;

                stageCreateManager.StageObjectList.Add(new StageObject(StageObjectID,
                    transform.position.x,
                    transform.position.y,
                    transform.rotation.z));

            }
            else
            {
                stageCreateManager.StageObjectList[StageObjectID - 1].Xpos = transform.position.x;
                stageCreateManager.StageObjectList[StageObjectID - 1].Ypos = transform.position.y;
                stageCreateManager.StageObjectList[StageObjectID - 1].Rot = transform.rotation.z;



            }
        }

    }

    public bool isRight()
    {
        SpriteRenderer sqSr = GetComponent<SpriteRenderer>();

        Vector3 BasePos = new Vector3(transform.position.x + (sqSr.bounds.size.x / 2)+ outlineDistance, transform.position.y, transform.position.z);
        Vector3 UpStartpoint = new Vector3(BasePos.x,BasePos.y + (sqSr.bounds.size.y / 2.2f),BasePos.z);
        Vector3 DownStartpoint = new Vector3(BasePos.x, BasePos.y - (sqSr.bounds.size.y / 2.2f), BasePos.z);

        Debug.DrawLine(UpStartpoint, BasePos);
        Debug.DrawLine(DownStartpoint, BasePos);

        return Physics2D.Linecast(UpStartpoint, BasePos, ObjectLayer)
            || Physics2D.Linecast(DownStartpoint, BasePos, ObjectLayer);
    }

    public bool isLeft() {
        SpriteRenderer sqSr = GetComponent<SpriteRenderer>();

        Vector3 BasePos = new Vector3(transform.position.x - (sqSr.bounds.size.x / 2) - outlineDistance, transform.position.y, transform.position.z);
        Vector3 UpStartpoint = new Vector3(BasePos.x, BasePos.y + (sqSr.bounds.size.y / 2.2f), BasePos.z);
        Vector3 DownStartpoint = new Vector3(BasePos.x, BasePos.y - (sqSr.bounds.size.y / 2.2f), BasePos.z);

        Debug.DrawLine(UpStartpoint, BasePos);
        Debug.DrawLine(DownStartpoint, BasePos);

        return Physics2D.Linecast(UpStartpoint, BasePos, ObjectLayer)
            || Physics2D.Linecast(DownStartpoint, BasePos, ObjectLayer);
    }

    public bool isUp() {
        SpriteRenderer sqSr = GetComponent<SpriteRenderer>();

        Vector3 BasePos = new Vector3(transform.position.x, transform.position.y + (sqSr.bounds.size.y / 2) + outlineDistance, transform.position.z);
        Vector3 RightStartpoint = new Vector3(BasePos.x + (sqSr.bounds.size.x / 2.2f), BasePos.y , BasePos.z);
        Vector3 LeftStartpoint = new Vector3(BasePos.x - (sqSr.bounds.size.x / 2.2f), BasePos.y, BasePos.z);

        Debug.DrawLine(RightStartpoint, BasePos);
        Debug.DrawLine(LeftStartpoint, BasePos);

        return Physics2D.Linecast(RightStartpoint, BasePos, ObjectLayer)
            || Physics2D.Linecast(LeftStartpoint, BasePos, ObjectLayer);
    }

    public bool isDown() {
        SpriteRenderer sqSr = GetComponent<SpriteRenderer>();


        Vector3 BasePos = new Vector3(transform.position.x, transform.position.y - (sqSr.bounds.size.y / 2) - outlineDistance, transform.position.z);
        Vector3 RightStartpoint = new Vector3(BasePos.x + (sqSr.bounds.size.x / 2.2f), BasePos.y, BasePos.z);
        Vector3 LeftStartpoint = new Vector3(BasePos.x - (sqSr.bounds.size.x / 2.2f), BasePos.y, BasePos.z);

        Debug.DrawLine(RightStartpoint, BasePos);
        Debug.DrawLine(LeftStartpoint, BasePos);

        return Physics2D.Linecast(RightStartpoint, BasePos, ObjectLayer)
            || Physics2D.Linecast(LeftStartpoint, BasePos, ObjectLayer);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.gameObject.tag == "DeleteBox")
        {
            isDelete = true;
         
        }

        if(collision.gameObject.tag == "Button")
        {
            if(stageCreateManager != null)
            {
                SetButtonID = collision.gameObject.GetComponent<ObjectButtonManager>().ID;

                Debug.Log(SetButtonID);
            }

            Debug.Log(SetButtonID);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DeleteBox")
        {
            isDelete = false;
            
        }

        if (collision.gameObject.tag == "Button")
        {
            SetButtonID = -1;
        }
    }


}
