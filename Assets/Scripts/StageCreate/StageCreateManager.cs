using Assets.Scripts.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[DefaultExecutionOrder(-5)]

public class StageCreateManager : MonoBehaviour
{
    public List<Object> PutObjects;//�ݒu���Ă���I�u�W�F�N�g���

    [SerializeField] public InputField inputStageName;
    [SerializeField] public InputField inputPoint;
    [SerializeField] public List<ObjectButtonManager> ButtonList;
    [SerializeField] public List<GameObject> ObjectList;

    public List<int> ButtonObjIDList;

    public List<StageObject> StageObjectList = new List<StageObject>();

    [SerializeField] public GameObject ObjButtonPrefab;

    [SerializeField] public GameObject ParentButtonObj;

    public string SetStageName = "test000";

    public int StageId;

    public int StartPoint;

    [SerializeField] public Transform PlayerPos;
    [SerializeField] public Transform GoalPos;
    
    
    [SerializeField]public GameManager gameManager;
    [SerializeField]public StageManager stageManager;

    [SerializeField] GameObject CreateObject;
    [SerializeField] GameObject GameObjects;

    public bool Draging;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].ID = i;
        }

        ButtonObjIDList = new List<int>();

        for (int i = 0;i < ButtonList.Count; i++)
        {
            ButtonObjIDList.Add(-1);
        }

        SetObjectButton();
    }

    // Update is called once per frame
    void Update()
    {


    }

    public void SetObjectButton()
    {
        foreach (GameObject obj in ObjectList)
        {
            GameObject button = Instantiate(ObjButtonPrefab,
                ParentButtonObj.transform.position,
                Quaternion.identity,
                ParentButtonObj.transform);

            ObjectButtonManager manager = button.GetComponent<ObjectButtonManager>();

            manager.PopObjectPrefab = obj.GetComponent<Object>();
            manager.CreateMode = true;
        }
    }

    //���V��ʂ̐�������
    public void SetTryStage()
    {
        //�{�^���ƃI�u�W�F�N�g�̏�����
        stageManager.StageObjects = new List<StageObject>();
        stageManager.ButtonObjIDList = new List<int>();

       
        //�X�e�[�W�I�u�W�F�N�g��ǉ�
        foreach (StageObject obj in StageObjectList)
        {
            stageManager.StageObjects.Add(obj);
            
        }

        //�{�^���ɃI�u�W�F�N�g��ǉ�
        foreach (int Id in ButtonObjIDList)
        {
           stageManager.ButtonObjIDList.Add(Id);
        }

        DeleteObjects();

        stageManager.SetObject();


        //���V��ʂ�UI�ɐ؂�ւ�
        CreateObject.SetActive(false);
        GameObjects.SetActive(true);

        //�I�u�W�F�N�g�𑀍�s�ɂ���
        SetFixed();

        //�{�^���̃C�x���g���Z�b�g
        stageManager.ResetButtons();

        //�{�^���̃C�x���g��ݒ�
        stageManager.SetButtons();


        //�v���C���[�̐ݒ�ύX
        gameManager.playerManager.createMode = false;
        gameManager.playerManager.StartPos = PlayerPos.position;
        gameManager.playerManager.InitPlayer();

        //�|�C���g�̐ݒ�
        gameManager.point = StartPoint;
        gameManager.uiManager.ChangePointTex();
    }

    public void BackStageCreate()
    {
 

        //���V��ʂ�UI�ɐ؂�ւ�
        CreateObject.SetActive(true);
        GameObjects.SetActive(false);

        //�v���C���[�̐ݒ�ύX
        gameManager.playerManager.createMode = true;
        gameManager.playerManager.InitPlayer();

        //�I�u�W�F�N�g�𑀍�\�ɂ���
        RemoveFixed();

        DeleteObjects();
        SetObject();
    }

    public void RemoveFixed()
    {
        Object[] gameObjects = GameObject.FindObjectsOfType<Object>();

        foreach (Object obj in gameObjects)
        {
            obj.isFixed = false;
        }
    }

    public void SetFixed()
    {
        Object[] gameObjects = GameObject.FindObjectsOfType<Object>();

        foreach (Object obj in gameObjects)
        {
            obj.isFixed = true;
        }
    }


 

    public void SetObject()
    {
        for (int i = 0; i < StageObjectList.Count; i++)
        {
            if(StageObjectList[i].ObjectId >= 999)
            { continue;}

            GameObject SetObj = Instantiate(ObjectList[StageObjectList[i].ObjectId - 1],
                  new Vector3(StageObjectList[i].Xpos, StageObjectList[i].Ypos),
                 ObjectList[StageObjectList[i].ObjectId - 1].transform.rotation);

            SetObj.transform.Rotate(new Vector3(0, 0, StageObjectList[i].Rot));

            Object obj = SetObj.GetComponent<Object>();

            obj.isFixed = false;
            obj.CreateMode = true;
            obj.StageObjectID = i + 1;
        }
    }

    public void DeleteObjects()
    {
        Object[] gameObjects = GameObject.FindObjectsOfType<Object>();

        foreach (Object obj in gameObjects)
        {
            if(obj.id >= 999)
            { continue; }
            Destroy(obj.gameObject);
        }
    }

    public void StageCreate()
    {

        StartCoroutine(NetworkManager.Instance.RegistStage(
            SetStageName, //���O
            StartPoint,
result =>
{                          //�o�^�I����̏���
    if (result == true)
    {
        StoreObjects();
        StoreButtons();
        StorePlayerPos();
        StoreGoalPos();
    }
    else
    {
        Debug.Log("�o�^������ɏI�����܂���ł����B");

    }
}));

    }

    public void StoreObjects()
    {

        foreach (StageObject obj in StageObjectList)
        {
            Debug.Log(obj.ObjectId);

            if(obj.ObjectId == 999)
            {
                continue;
            }

            StartCoroutine(NetworkManager.Instance.RegistStageObject(
          obj,StageId,           //���O
    result =>
    {                          //�o�^�I����̏���
        if (result == true)
        {
            Debug.Log("�o�^����");
        }
        else
        {
            Debug.Log("�o�^������ɏI�����܂���ł����B");

        }
    }));

        }
    }
    public void StoreButtons()
    {
        foreach(int Id in ButtonObjIDList)
        {
            if(Id < 0) { continue; }


            StartCoroutine(NetworkManager.Instance.RegistObjectButton(
          Id, StageId,           //���O
    result =>
    {                          //�o�^�I����̏���
        if (result == true)
        {
            Debug.Log("�{�^���o�^����");
        }
        else
        {
            Debug.Log("�o�^������ɏI�����܂���ł����B");

        }
    }));

        }
    }

    public void StorePlayerPos()
    {
        //�v���C���[��ID99�ŃI�u�W�F�N�g��
        StageObject player = new StageObject(99, gameManager.playerManager.StartPos.x, gameManager.playerManager.StartPos.y,0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          player, StageId,           //���O
    result =>
    {                          //�o�^�I����̏���
        if (result == true)
        {
            Debug.Log("�o�^����");
        }
        else
        {
            Debug.Log("�o�^������ɏI�����܂���ł����B");

        }
    }));
    }

    public void StoreGoalPos()
    {
        //�S�[����ID100�ŃI�u�W�F�N�g��
        StageObject Goal = new StageObject(100, GoalPos.position.x, GoalPos.position.y, 0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          Goal, StageId,           //���O
    result =>
    {                          //�o�^�I����̏���
        if (result == true)
        {
            Debug.Log("�o�^����");
        }
        else
        {
            Debug.Log("�o�^������ɏI�����܂���ł����B");

        }
    }));
    }

    public void ChangeStageName()
    {
        SetStageName = inputStageName.text;
       
    }

    public void ChangePoint()
    {
        StartPoint = int.Parse(inputPoint.text);
        Debug.Log(StartPoint);
    }

    public void BackTitleScene()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

    public void CreateNormalStage()
    {
        StartCoroutine(NetworkManager.Instance.RegistNormalStage(
           SetStageName, //���O
           StartPoint,
result =>
{                          //�o�^�I����̏���
   if (result == true)
   {
       StoreObjects();
       StoreButtons();
       StorePlayerPos();
       StoreGoalPos();
   }
   else
   {
       Debug.Log("�o�^������ɏI�����܂���ł����B");

   }
}));
    }
}
