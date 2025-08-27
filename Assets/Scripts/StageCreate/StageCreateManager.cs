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
    public List<Object> PutObjects;//ê›íuÇµÇƒÇ¢ÇÈÉIÉuÉWÉFÉNÉgèÓïÒ

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

    public void SetTryStage()
    {
        stageManager.StageObjects = new List<StageObject>();
        stageManager.ButtonObjIDList = new List<int>();

       

        foreach (StageObject obj in StageObjectList)
        {
            stageManager.StageObjects.Add(obj);
        }

        foreach(int Id in ButtonObjIDList)
        {
           stageManager.ButtonObjIDList.Add(Id);
        }



        CreateObject.SetActive(false);
        GameObjects.SetActive(true);

        SetFixed();

        stageManager.SetButtons();
        gameManager.playerManager.createMode = false;
        gameManager.playerManager.StartPos = PlayerPos.position;
        gameManager.playerManager.InitPlayer();

        gameManager.point = StartPoint;
        gameManager.uiManager.ChangePointTex();
    }

    public void BackStageCreate()
    {
        CreateObject.SetActive(true);
        GameObjects.SetActive(false);

        gameManager.playerManager.createMode = true;
        gameManager.playerManager.InitPlayer();

        RemoveFixed();
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

    public void StageCreate()
    {

        StartCoroutine(NetworkManager.Instance.RegistStage(
            SetStageName, //ñºëO
            StartPoint,
result =>
{                          //ìoò^èIóπå„ÇÃèàóù
    if (result == true)
    {
        StoreObjects();
        StoreButtons();
        StorePlayerPos();
        StoreGoalPos();
    }
    else
    {
        Debug.Log("ìoò^Ç™ê≥èÌÇ…èIóπÇµÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");

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
          obj,StageId,           //ñºëO
    result =>
    {                          //ìoò^èIóπå„ÇÃèàóù
        if (result == true)
        {
            Debug.Log("ìoò^äÆóπ");
        }
        else
        {
            Debug.Log("ìoò^Ç™ê≥èÌÇ…èIóπÇµÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");

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
          Id, StageId,           //ñºëO
    result =>
    {                          //ìoò^èIóπå„ÇÃèàóù
        if (result == true)
        {
            Debug.Log("É{É^Éììoò^äÆóπ");
        }
        else
        {
            Debug.Log("ìoò^Ç™ê≥èÌÇ…èIóπÇµÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");

        }
    }));

        }
    }

    public void StorePlayerPos()
    {
        //ÉvÉåÉCÉÑÅ[ÇÕID99Ç≈ÉIÉuÉWÉFÉNÉgâª
        StageObject player = new StageObject(99, gameManager.playerManager.StartPos.x, gameManager.playerManager.StartPos.y,0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          player, StageId,           //ñºëO
    result =>
    {                          //ìoò^èIóπå„ÇÃèàóù
        if (result == true)
        {
            Debug.Log("ìoò^äÆóπ");
        }
        else
        {
            Debug.Log("ìoò^Ç™ê≥èÌÇ…èIóπÇµÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");

        }
    }));
    }

    public void StoreGoalPos()
    {
        //ÉSÅ[ÉãÇÕID100Ç≈ÉIÉuÉWÉFÉNÉgâª
        StageObject Goal = new StageObject(100, GoalPos.position.x, GoalPos.position.y, 0);

        StartCoroutine(NetworkManager.Instance.RegistStageObject(
          Goal, StageId,           //ñºëO
    result =>
    {                          //ìoò^èIóπå„ÇÃèàóù
        if (result == true)
        {
            Debug.Log("ìoò^äÆóπ");
        }
        else
        {
            Debug.Log("ìoò^Ç™ê≥èÌÇ…èIóπÇµÇ‹ÇπÇÒÇ≈ÇµÇΩÅB");

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
}
