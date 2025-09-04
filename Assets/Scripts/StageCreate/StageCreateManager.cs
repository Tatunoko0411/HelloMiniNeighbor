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
    public List<Object> PutObjects;//設置しているオブジェクト情報

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

    //試遊画面の生成処理
    public void SetTryStage()
    {
        //ボタンとオブジェクトの初期化
        stageManager.StageObjects = new List<StageObject>();
        stageManager.ButtonObjIDList = new List<int>();

       
        //ステージオブジェクトを追加
        foreach (StageObject obj in StageObjectList)
        {
            stageManager.StageObjects.Add(obj);
            
        }

        //ボタンにオブジェクトを追加
        foreach (int Id in ButtonObjIDList)
        {
           stageManager.ButtonObjIDList.Add(Id);
        }

        DeleteObjects();

        stageManager.SetObject();


        //試遊画面のUIに切り替え
        CreateObject.SetActive(false);
        GameObjects.SetActive(true);

        //オブジェクトを操作不可にする
        SetFixed();

        //ボタンのイベントリセット
        stageManager.ResetButtons();

        //ボタンのイベントを設定
        stageManager.SetButtons();


        //プレイヤーの設定変更
        gameManager.playerManager.createMode = false;
        gameManager.playerManager.StartPos = PlayerPos.position;
        gameManager.playerManager.InitPlayer();

        //ポイントの設定
        gameManager.point = StartPoint;
        gameManager.uiManager.ChangePointTex();
    }

    public void BackStageCreate()
    {
 

        //試遊画面のUIに切り替え
        CreateObject.SetActive(true);
        GameObjects.SetActive(false);

        //プレイヤーの設定変更
        gameManager.playerManager.createMode = true;
        gameManager.playerManager.InitPlayer();

        //オブジェクトを操作可能にする
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
            SetStageName, //名前
            StartPoint,
result =>
{                          //登録終了後の処理
    if (result == true)
    {
        StoreObjects();
        StoreButtons();
        StorePlayerPos();
        StoreGoalPos();
    }
    else
    {
        Debug.Log("登録が正常に終了しませんでした。");

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
          obj,StageId,           //名前
    result =>
    {                          //登録終了後の処理
        if (result == true)
        {
            Debug.Log("登録完了");
        }
        else
        {
            Debug.Log("登録が正常に終了しませんでした。");

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
          Id, StageId,           //名前
    result =>
    {                          //登録終了後の処理
        if (result == true)
        {
            Debug.Log("ボタン登録完了");
        }
        else
        {
            Debug.Log("登録が正常に終了しませんでした。");

        }
    }));

        }
    }

    public void StorePlayerPos()
    {
        //プレイヤーはID99でオブジェクト化
        StageObject player = new StageObject(99, gameManager.playerManager.StartPos.x, gameManager.playerManager.StartPos.y,0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          player, StageId,           //名前
    result =>
    {                          //登録終了後の処理
        if (result == true)
        {
            Debug.Log("登録完了");
        }
        else
        {
            Debug.Log("登録が正常に終了しませんでした。");

        }
    }));
    }

    public void StoreGoalPos()
    {
        //ゴールはID100でオブジェクト化
        StageObject Goal = new StageObject(100, GoalPos.position.x, GoalPos.position.y, 0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          Goal, StageId,           //名前
    result =>
    {                          //登録終了後の処理
        if (result == true)
        {
            Debug.Log("登録完了");
        }
        else
        {
            Debug.Log("登録が正常に終了しませんでした。");

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
           SetStageName, //名前
           StartPoint,
result =>
{                          //登録終了後の処理
   if (result == true)
   {
       StoreObjects();
       StoreButtons();
       StorePlayerPos();
       StoreGoalPos();
   }
   else
   {
       Debug.Log("登録が正常に終了しませんでした。");

   }
}));
    }
}
