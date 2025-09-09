using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    string Name;
    int userId;

    [SerializeField]InputField nameField;
    [SerializeField]Text IDText;

    [SerializeField]GameObject UserUI;
    [SerializeField] GameObject MainUI;

    [SerializeField]GameObject TitleStart;
    bool isTouched;
    bool isSave = false;
    bool StartGetNStage = false;//�X�e�[�W���擾���J�n�������ǂ���

    //�R���[�`���I���҂��p�̕ϐ�
    int btnCnt = 0;
    int ObjCnt = 0;

    // Start is called before the first frame update
    void Start()
    {
        bool isSuccess = NetworkManager.Instance.LoadUserData();
        if (false)
        {
            Debug.Log("�f�[�^������܂���");
           InitDate();
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.RegistUser(
               Guid.NewGuid().ToString(),           //���O
            result =>
            {                          //�o�^�I����̏���
                if (result == true)
                {
             
                    Debug.Log("�o�^����");
                    Debug.Log(NetworkManager.Instance.ApiToken);
                }
                else
                {
                    Debug.Log("���[�U�[�o�^������ɏI�����܂���ł����B");
                    
                }
            }));

        }

        isSuccess = NetworkManager.Instance.LoadStage();

        if (isSuccess)
        {
            Debug.Log("�f�[�^������܂���");
            Debug.Log(StageManager.NormalstagesObjects[0].Count);
            isSave = true;
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.GetNormalStage(
               result =>
               {                          //�o�^�I����̏���
                   if (result == true)
                   {
                       StartGetNStage = true;
                       Debug.Log(StageManager.NormalStageIDs.Count );
                       foreach(int id in StageManager.NormalStageIDs)
                       {
                           

                           GetNormalStageBtns(id);
                           GetNormalStageObjs(id);
                       }


                       Debug.Log("�o�^����");
                     
                   }
                   else
                   {
                       Debug.Log("���[�U�[�o�^������ɏI�����܂���ł����B");

                   }
               }));

        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetMouseButtonDown(0) && !isTouched)
        {
            isTouched = true;
            TitleStart.SetActive(false);
        }

        if (btnCnt >= StageManager.NormalStageIDs.Count
            && ObjCnt >= StageManager.NormalStageIDs.Count
            && StartGetNStage)
        {//�S���擾����
            if (!isSave)
            {
                NetworkManager.Instance.SaveStage();
                Debug.Log("�X�e�[�W�����Z�[�u���܂���");
                isSave = true;
            }
           
        }
    }

    public void InitDate()
    {
        Name = NetworkManager.Instance.UserName;
        userId = NetworkManager.Instance.UserID;
        nameField.text = Name;
        IDText.text = "ID:" + userId;
    }

    public void UpdateName()
    {
        StartCoroutine(NetworkManager.Instance.UpdateUser(nameField.text,
            result =>
            {                          //�o�^�I����̏���
            if (result == true)
            {
                Debug.Log("�X�V����");
            }
            else
            {
                Debug.Log("����ɏI�����܂���ł����B");

            }
        }));
    }

    void GetNormalStageObjs(int stageId)
    {
        StartCoroutine(NetworkManager.Instance.GetNormelStageObjects(stageId,
               result =>
               {                          //�o�^�I����̏���
                   if (result == true)
                   {
                       Debug.Log("����");
                       ObjCnt++;
                       Debug.Log("objCnt:" + ObjCnt + "StageManager.NormalStageIDs.Count" + StageManager.NormalStageIDs.Count);
                   }
                   else
                   {
                       Debug.Log("���[�U�[�o�^������ɏI�����܂���ł����B");

                   }
               }));
    }

    void GetNormalStageBtns(int stageId)
    {
        StartCoroutine(NetworkManager.Instance.GetNormalStageButtons(stageId,
               result =>
               {                          //�o�^�I����̏���
                   if (result == true)
                   {
            
                       Debug.Log("����");
                       btnCnt++;
                       Debug.Log("btnCnt:" + btnCnt + "StageManager.NormalStageIDs.Count" + StageManager.NormalStageIDs.Count);

                   }
                   else
                   {
                       Debug.Log("���[�U�[�o�^������ɏI�����܂���ł����B");

                   }
               }));
    }

  

    public void BackTItle()
    {
        isTouched = false;
        TitleStart.SetActive(true);
    }

    public void DisplayUserData()
    {
        UserUI.SetActive(true);
        MainUI.SetActive(false);
    }

    public void HideUserData()
    {
        MainUI.SetActive(true);
        UserUI.SetActive(false);
    }

    public void MoveSelect()
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }

    public void MoveCustomStageSelect()
    {
        Initiate.Fade("CustomStageSelectScene", Color.black, 1.0f);
    }

    public void MoveCreate()
    {
        Initiate.Fade("StageCreateScene", Color.black, 1.0f);
    }
}
