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

    static float outlineDistance = 0.075f;//����̊O���ɑ΂��鋗��
    public int cost;

    bool isDelete;
   public  bool isFixed;
    
    GameManager gameManager;
    [SerializeField] LayerMask ObjectLayer;

    public bool isDrag;

   public Rigidbody2D rb;

    /// �X�e�[�W�N���G�C�g�n
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
            {//�Q�[�����s���͈ړ������Ȃ�
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
            {//�h���b�O��Ԃ̉���
                gameManager.Draging = false;
                
            }
            else if (CreateMode)
            {
                if (SetButtonID < 0)
                {
                    if (StageObjectID == 0)
                    {//�I�u�W�F�N�g����o�^
                        StageObjectID = stageCreateManager.StageObjectList.Count + 1;

                        stageCreateManager.StageObjectList.Add(new StageObject(id+1,
                            transform.position.x,
                            transform.position.y,
                            transform.rotation.z));

                        Debug.Log("�I�u�W�F�N�g���V�����ݒu����܂���");
                        Debug.Log("ID:" + (id+1));

                    }
                }


                //�h���b�O��Ԃ̉���
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

            //�e�����ɓ����蔻�肪����Έړ�����������
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
            {//��݃I�u�W�F�N�g�͍폜����Ȃ�

                //�I�u�W�F�N�g�폜
                Destroy(this.gameObject);

                if (!CreateMode)
                {//�����|�C���g�ύX
                    gameManager.changePoint(cost);
                }
                else
                {
                    if (StageObjectID != 0)
                    {//�o�^����Ă��������폜
                        stageCreateManager.StageObjectList[StageObjectID - 1].ObjectId = -1;
                        Debug.Log("�I�u�W�F�N�g���폜����܂���");
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
                    {//�{�^���ɃI�u�W�F�N�g����ǉ�
                        stageCreateManager.ButtonList[SetButtonID].PopObjectPrefab
                            = stageCreateManager.ObjectList[id].GetComponent<Object>();

                        stageCreateManager.ButtonList[SetButtonID].ChangeSprite();



                        stageCreateManager.ButtonObjIDList[SetButtonID] = stageCreateManager.ObjectList[id].GetComponent<Object>().id;

                        //�I�u�W�F�N�g���폜
                        Destroy(this.gameObject);

                        //if (StageObjectID != 0)
                        //{//�o�^����Ă��������폜
                        //    stageCreateManager.StageObjectList.Remove(stageCreateManager.StageObjectList[StageObjectID  - 1]);
                        //    Debug.Log("�I�u�W�F�N�g���폜����܂���");
                        //}
                    }
                }
            }

        }
    }

    private void OnMouseDrag()
    {
        if(isFixed || gameManager.isStart)
        {//��݃I�u�W�F�N�g�܂��͎��s���͑I���ł��Ȃ�
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
        //�{�f�B�^�C�v�̕ύX
        rb.bodyType = RigidbodyType2D.Dynamic;

        //�h���b�O��Ԃɂ���
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

        //�h���b�O��Ԃ̉���
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

                    Debug.Log("�I�u�W�F�N�g���V�����ݒu����܂���");
                }
                else
                {//�I�u�W�F�N�g���̍X�V
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
                    Debug.Log("�I�u�W�F�N�g���폜����܂���");
                }
            }

        }

    }

    //�I�u�W�F�N�g�̓����蔻��i�E�j
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

    //�I�u�W�F�N�g�̓����蔻��i���j
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

    //�I�u�W�F�N�g�̓����蔻��i��j
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

    //�I�u�W�F�N�g�̓����蔻��i���j
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
