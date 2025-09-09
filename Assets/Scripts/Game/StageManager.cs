using Assets.Scripts.Game;
using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//他のスクリプトよりも読み込みを優先させる
[DefaultExecutionOrder(-5)]

public class StageManager : MonoBehaviour
{
    [SerializeField]public List<GameObject> ButtonList;
    [SerializeField] List<GameObject> ObjectList;

    [SerializeField]public List<int> ButtonObjIDList;

    public List<StageObject> StageObjects = new List<StageObject>()
    {
        new StageObject(1,-7,-3,0), new StageObject(1,8,-3,0)
    };


    static public List<int> NormalStageIDs = new List<int>();
    static public List<int> NormalStagePoints = new List<int>();
    static public List<List<int>> NormalStageButtonIDs = new List<List<int>>();
    static public List<List<StageObject>> NormalstagesObjects = new List<List<StageObject>>();

    static public int StageID;

    public bool createMode;
    // Start is called before the first frame update
    void Start()
    {


        if (!createMode)
        {
            StageObjects = new List<StageObject>();
            StageObjects = NormalstagesObjects[StageID];

            ButtonObjIDList = new List<int>();
            ButtonObjIDList = NormalStageButtonIDs[StageID];

            GameManager gameManager = GameObject.FindObjectOfType<GameManager>();
            gameManager.point = NormalStagePoints[StageID];

            SetButtons();
            SetObject();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ボタンのリセット
    public void ResetButtons()
    {
        for (int i = 0; i < ButtonList.Count; i++)
        {
            if (ButtonList[i] == null)
            { continue; }
 
            ObjectButtonManager manager = ButtonList[i].GetComponent<ObjectButtonManager>();


            manager.ResetEvent();

        }
    }

    //ボタンの設定
    public void SetButtons()
    {
        for (int i = 0; i < ButtonObjIDList.Count; i++)
        {
            if(ButtonObjIDList[i] < 0)
            {
                continue;
            }
            ButtonList[i].gameObject.SetActive(true);
            ObjectButtonManager manager = ButtonList[i].GetComponent<ObjectButtonManager>();

            manager.PopObjectPrefab
                = ObjectList[ButtonObjIDList[i]].GetComponent<Object>();

            manager.Cost = ObjectList[ButtonObjIDList[i]].GetComponent<Object>().cost;

            manager.SetEvent();

        }

        for (int i = 0; i < ButtonList.Count; i++)
        {

            ObjectButtonManager manager = ButtonList[i].GetComponent<ObjectButtonManager>();

            if (manager.PopObjectPrefab == null)
            {
               ButtonList[i].gameObject.SetActive(false);
           
              
            }
        }
    }

    public void SetObject()
    {
        //for (int i = 0; i < StageObjects.Count; i++)
        //{
        //    if (StageObjects[i].ObjectId >= 999)
        //    { continue; }

        //    GameObject SetObj =   Instantiate(ObjectList[StageObjects[i].ObjectId-1],
        //        new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos),
        //        ObjectList[StageObjects[i].ObjectId - 1].transform.rotation);

        //  Object obj = SetObj.GetComponent<Object>();

        //    obj.CreateMode = false;
        //    obj.isFixed = true;
           

        //}

        GameManager gameManager = GameObject.FindAnyObjectByType<GameManager>();

        for (int i = 0; i < StageObjects.Count; i++)
        {
            if (StageObjects[i].ObjectId == 99)
            {//プレイヤーの座標設定
                gameManager.playerManager.StartPos = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                gameManager.playerManager.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue;

            }

            if (StageObjects[i].ObjectId == 100)
            {//ゴールの座標設定
                gameManager.GoalObj.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue;


            }

            if (StageObjects[i].ObjectId > 100 || StageObjects[i].ObjectId < 0)
            {
                continue;
            }

            GameObject SetObj = Instantiate(ObjectList[StageObjects[i].ObjectId - 1],
                  new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos),
                ObjectList[StageObjects[i].ObjectId - 1].gameObject.transform.rotation);



            Object obj = SetObj.GetComponent<Object>();

            obj.isFixed = true;
        }
    }
}
