using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//���̃X�N���v�g�����ǂݍ��݂�D�悳����
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

    //�{�^���ɃI�u�W�F�N�g����ݒ�
    public void SetButtons()
    {
        for (int i = 0; i < ButtonObjIDList.Count; i++)
        {
            ObjectButtonManager manager = ButtonList[i].GetComponent<ObjectButtonManager>();

            manager.PopObjectPrefab
                = ObjectList[ButtonObjIDList[i]].GetComponent<Object>();

            manager.Cost = ObjectList[ButtonObjIDList[i]].GetComponent<Object>().cost;

            //�C�x���g��ǉ�
            manager.SetEvent();

        }

        for (int i = 0; i < ButtonList.Count; i++)
        {

            ObjectButtonManager manager = ButtonList[i].GetComponent<ObjectButtonManager>();

            if (manager.PopObjectPrefab == null)
            {//�����ݒ肳��Ă��Ȃ���΍폜
                Destroy(ButtonList[i].gameObject);
                ButtonList.Remove(ButtonList[i]);
                i--;
            }
        }
    }

    //�I�u�W�F�N�g�ݒu
    public void SetObject()
    {
        for (int i = 0; i < StageObjects.Count; i++)
        {
            if(StageObjects[i].ObjectId==99)
            {//�v���C���[�̍��W�ݒ�
                gameManager.playerManager.StartPos = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                gameManager.playerManager.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue;

            }

            if (StageObjects[i].ObjectId == 100)
            {//�S�[���̍��W�ݒ�
                gameManager.GoalObj.gameObject.transform.position = new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos);
                continue ;
               

            }

            if(StageObjects[i].ObjectId > 100)
            {
                continue ;
            }

            GameObject SetObj = Instantiate(ObjectList[StageObjects[i].ObjectId - 1],
                  new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos),
                ObjectList[StageObjects[i].ObjectId - 1].gameObject.transform.rotation);

  

            Object obj = SetObj.GetComponent<Object>();

            obj.isFixed = true;
        }
    }

    //�X�e�[�W�I�u�W�F�N�g�̏����擾
    public void GetStageObjects()
    {
        StartCoroutine(NetworkManager.Instance.GetStageObjects(StageId,          //�X�e�[�WID
 result =>
 {                          //�o�^�I����̏���
     if (result == true)
     {

         SetObject();
     }
     else
     {
         Debug.Log("�o�^������ɏI�����܂���ł����B");

     }
 }));
    }

    //�{�^�����̎擾
    public void GetButtons()
    {
        StartCoroutine(NetworkManager.Instance.GetStageButtons(StageId,          //�X�e�[�WID
result =>
{                          //�o�^�I����̏���
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
        Debug.Log("�o�^������ɏI�����܂���ł����B");

    }
}));
    }
}
