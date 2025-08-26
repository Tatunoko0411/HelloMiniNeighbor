using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//他のスクリプトよりも読み込みを優先させる
[DefaultExecutionOrder(-5)]

public class CustomStageGameManager : MonoBehaviour
{

    static public int StageId = 0;
    static public int StartPoint = 0;

    [SerializeField] public List<GameObject> ButtonList;
    [SerializeField] List<GameObject> ObjectList;

    [SerializeField] public List<int> ButtonObjIDList;
    [SerializeField] GameManager gameManager;
    [SerializeField] UIManager uiManager;

    public List<StageObject> StageObjects = new List<StageObject>();


    // Start is called before the first frame update
    void Start()
    {
        gameManager.point = StartPoint;
        uiManager.ChangePointTex();
        GetStageObjects();
        GetButtons();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetButtons()
    {
        for (int i = 0; i < ButtonObjIDList.Count; i++)
        {
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
                Destroy(ButtonList[i].gameObject);
                ButtonList.Remove(ButtonList[i]);
                i--;
            }
        }
    }

    public void SetObject()
    {
        for (int i = 0; i < StageObjects.Count; i++)
        {
            if(StageObjects[i].ObjectId==99)
            {//プレイヤーの座標設定
                gameManager.playerManager.StartPos = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                gameManager.playerManager.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue;

            }

            if (StageObjects[i].ObjectId == 100)
            {//ゴールの座標設定
                gameManager.GoalObj.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue ;
               

            }

            GameObject SetObj = Instantiate(ObjectList[StageObjects[i].ObjectId],
                  new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos),
                  Quaternion.identity);

            SetObj.transform.Rotate(new Vector3(0, 0, StageObjects[i].Rot));

            Object obj = SetObj.GetComponent<Object>();

            obj.isFixed = true;
        }
    }

    public void GetStageObjects()
    {
        StartCoroutine(NetworkManager.Instance.GetStageObjects(StageId,          //ステージID
 result =>
 {                          //登録終了後の処理
     if (result == true)
     {

         SetObject();
     }
     else
     {
         Debug.Log("登録が正常に終了しませんでした。");

     }
 }));
    }

    public void GetButtons()
    {
        StartCoroutine(NetworkManager.Instance.GetStageButtons(StageId,          //ステージID
result =>
{                          //登録終了後の処理
    if (result == true)
    {
        foreach (int id in ButtonObjIDList)
        {
            Debug.Log($"{id}");
        }
        SetButtons();
    }
    else
    {
        Debug.Log("登録が正常に終了しませんでした。");

    }
}));
    }
}
