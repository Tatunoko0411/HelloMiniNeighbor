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
    [SerializeField] public List<ObjectButtonManager> ButtonList;
    [SerializeField] public List<GameObject> ObjectList;

    public List<int> ButtonObjIDList;

    public List<StageObject> StageObjectList = new List<StageObject>();

    [SerializeField] public GameObject ObjButtonPrefab;

    [SerializeField] public GameObject ParentButtonObj;

    public string SetStageName = "test000";

    public int StageId;

    [SerializeField] public Transform PlayerPos;
    [SerializeField] public Transform GoalPos;


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

    public void StageCreate()
    {

        StartCoroutine(NetworkManager.Instance.RegistStage(
            SetStageName, //���O
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

        StageObject player = new StageObject(99,PlayerPos.position.x,PlayerPos.position.y,0);

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
        Debug.Log(SetStageName);
    }
}
