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
    public bool CreateMode = false;
    public int StageObjectID;

    SEManager seManager;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        SetButtonID = -1;

        if (CreateMode)
        {
            stageCreateManager = GameObject.Find("StageCreateManager").GetComponent<StageCreateManager>();
        }
       
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        seManager = GameObject.Find("SEManager").GetComponent<SEManager>();
        


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

        if (!Input.GetMouseButton(0))
        {

            if (isDrag)
            {
                seManager.playPutSE();
            }
            isDrag = false;
            rb.bodyType = RigidbodyType2D.Static;
            if (!CreateMode)
            {//ドラッグ状態の解除
                gameManager.Draging = false;
                
            }
            else if (CreateMode)
            {
                if (SetButtonID < 0)
                {
                    if (StageObjectID == 0)
                    {//オブジェクト情報を登録
                        StageObjectID = stageCreateManager.StageObjectList.Count + 1;

                        stageCreateManager.StageObjectList.Add(new StageObject(id+1,
                            transform.position.x,
                            transform.position.y,
                            transform.rotation.z));

                        Debug.Log("オブジェクトが新しく設置されました");
                        Debug.Log("ID:" + (id+1));

                    }
                }


                //ドラッグ状態の解除
                stageCreateManager.Draging = false;
              

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

            //各方向に当たり判定があれば移動制限をする
            //if (isRight())
            //{
            //    if (transform.position.x < movePos.x)
            //    {
            //        movePos.x = transform.position.x;
            //    }

            //}
            //if (isLeft())
            //{
            //    if (transform.position.x > movePos.x)
            //    {
            //        movePos.x = transform.position.x;
            //    }
            //}
            //if (isUp())
            //{
            //    if (transform.position.y < movePos.y)
            //    {
            //        movePos.y = transform.position.y;
            //    }
            //}
            //if (isDown())
            //{
            //    if (transform.position.y > movePos.y)
            //    {
            //        movePos.y = transform.position.y;
            //    }
            //}



            transform.position = movePos;

        }
        else
        {
            if(isDelete && !isFixed &&id < 999)
            {//常設オブジェクトは削除されない

                //オブジェクト削除
                Destroy(this.gameObject);

                if (!CreateMode)
                {//所持ポイント変更
                    gameManager.changePoint(cost);
                }
                else
                {
                    if (StageObjectID != 0)
                    {//登録されていた情報を削除
                        stageCreateManager.StageObjectList[StageObjectID - 1].ObjectId = -1;
                        Debug.Log("オブジェクトが削除されました");
                    }
                }

                enabled = false;
                return;
            }

            if(CreateMode)
            {
                if (id < 999)
                {
                    if (SetButtonID >= 0)
                    {//ボタンにオブジェクト情報を追加
                        stageCreateManager.ButtonList[SetButtonID].PopObjectPrefab
                            = stageCreateManager.ObjectList[id].GetComponent<Object>();

                        stageCreateManager.ButtonList[SetButtonID].ChangeSprite();



                        stageCreateManager.ButtonObjIDList[SetButtonID] = stageCreateManager.ObjectList[id].GetComponent<Object>().id;

                        //オブジェクト情報削除
                        Destroy(this.gameObject);

                        //if (StageObjectID != 0)
                        //{//登録されていた情報を削除
                        //    stageCreateManager.StageObjectList.Remove(stageCreateManager.StageObjectList[StageObjectID  - 1]);
                        //    Debug.Log("オブジェクトが削除されました");
                        //}
                    }
                }
            }

        }
    }

    private void OnMouseDrag()
    {
        if(isFixed || gameManager.isStart)
        {//常設オブジェクトまたは実行中は選択できない
            return;
        }

        if (!CreateMode)
        {
            if (gameManager.Draging)
            {
                return;
            }
        }
        else if (stageCreateManager.Draging)
        {
            return;
        }

        isDrag = true;
        //ボディタイプの変更
        rb.bodyType = RigidbodyType2D.Dynamic;

        //ドラッグ状態にする
        if (!CreateMode)
        {
            gameManager.Draging = true;
        }
        else
        {
            stageCreateManager.Draging = true ;
        }
    }

    private void OnMouseUp()
    {

        if (isDrag)
        {
            seManager.playPutSE();
        }
        isDrag = false;
        rb.bodyType = RigidbodyType2D.Static;

        //ドラッグ状態の解除
        if (!CreateMode)
        {
            gameManager.Draging = false;
            
        }
        else
        {
            stageCreateManager.Draging = false;
        }

        if (CreateMode)
        {
            if(id == 999) { return; }

            if (SetButtonID < 0)
            {

                if (StageObjectID == 0)
                {
                    StageObjectID = stageCreateManager.StageObjectList.Count + 1;

                    stageCreateManager.StageObjectList.Add(new StageObject(id,
                        transform.position.x,
                        transform.position.y,
                        transform.rotation.z));

                    Debug.Log("オブジェクトが新しく設置されました");
                }
                else
                {//オブジェクト情報の更新
                    stageCreateManager.StageObjectList[StageObjectID - 1].Xpos = transform.position.x;
                    stageCreateManager.StageObjectList[StageObjectID - 1].Ypos = transform.position.y;
                    stageCreateManager.StageObjectList[StageObjectID - 1].Rot = transform.rotation.z;



                }
            }
            else
            {
                if (StageObjectID != 0)
                {
                    stageCreateManager.StageObjectList[StageObjectID - 1].ObjectId = -1;
                    Debug.Log("オブジェクトが削除されました");
                }
            }

        }

    }

    //オブジェクトの当たり判定（右）
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

    //オブジェクトの当たり判定（左）
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

    //オブジェクトの当たり判定（上）
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

    //オブジェクトの当たり判定（下）
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

              
            }

           
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
