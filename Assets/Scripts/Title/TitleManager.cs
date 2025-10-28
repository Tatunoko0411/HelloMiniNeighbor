using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
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

    [SerializeField] Text PlayText;
    [SerializeField] Text ClearText;
    [SerializeField] Text CreateText;

    [SerializeField]GameObject TitleStart;
    bool isTouched;
    bool isSave = false;
    bool StartGetNStage = false;//ステージ情報取得を開始したかどうか

    //コルーチン終了待ち用の変数
    int btnCnt = 0;
    int ObjCnt = 0;

    AudioSource audioSource;
    [SerializeField] AudioClip ClickSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bool isSuccess = NetworkManager.Instance.LoadUserData();
        if (isSuccess)
        {
            Debug.Log("データがありました");
           InitDate();
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.RegistUser(
               Guid.NewGuid().ToString(),           //名前
            result =>
            {                          //登録終了後の処理
                if (result == true)
                {
             
                    Debug.Log("登録完了");
                    Debug.Log(NetworkManager.Instance.ApiToken);
                    InitDate();
                }
                else
                {
                    Debug.Log("ユーザー登録が正常に終了しませんでした。");
                    
                }
            }));

        }

        isSuccess = NetworkManager.Instance.LoadStage();

        if (isSuccess)
        {
            Debug.Log("データがありました");
           // Debug.Log(StageManager.NormalstagesObjects[0].Count);
            isSave = true;
        }
        else
        {
            StartCoroutine(NetworkManager.Instance.GetNormalStages(
               result =>
               {                          //登録終了後の処理
                   if (result == true)
                   {
                       StartGetNStage = true;
                       Debug.Log(StageManager.NormalStageIDs.Count );
                     

                           GetNormalStageBtns(StageManager.NormalStageIDs);
                           GetNormalStageObjs(StageManager.NormalStageIDs);
                       


                       Debug.Log("登録完了");
                     
                   }
                   else
                   {
                       Debug.Log("ユーザー登録が正常に終了しませんでした。");

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
            playClickSE();
        }

        if (btnCnt >= StageManager.NormalStageIDs.Count
            && ObjCnt >= StageManager.NormalStageIDs.Count
            && StartGetNStage)
        {//全情報取得完了
            if (!isSave)
            {
                NetworkManager.Instance.SaveStage();
                Debug.Log("ステージ情報をセーブしました");
                isSave = true;
            }
           
        }
    }

    public void InitDate()
    {
        Name = NetworkManager.Instance.UserName;
        userId = NetworkManager.Instance.UserID;
        nameField.text = Name;
        IDText.text = $"{IDText.text}{userId}";
        
        PlayText.text = $"{PlayText.text}{NetworkManager.Instance.PlayTime}";
        ClearText.text = $"{ClearText.text}{NetworkManager.Instance.ClearTime}";
        CreateText.text = $"{CreateText.text}{NetworkManager.Instance.StageCreate}";
    }

    public void UpdateName()
    {
        StartCoroutine(NetworkManager.Instance.UpdateUser(nameField.text,
            result =>
            {                          //登録終了後の処理
            if (result == true)
            {
                Debug.Log("更新完了");
            }
            else
            {
                Debug.Log("正常に終了しませんでした。");

            }
        }));
    }

    void GetNormalStageObjs(List<int> Ids)
    {
        StartCoroutine(NetworkManager.Instance.GetNormelStageObjects(Ids,
               result =>
               {                          //登録終了後の処理
                   if (result == true)
                   {
                       Debug.Log("完了");
                       ObjCnt++;
                       Debug.Log("objCnt:" + ObjCnt + "StageManager.NormalStageIDs.Count" + StageManager.NormalStageIDs.Count);
                   }
                   else
                   {
                       Debug.Log("ユーザー登録が正常に終了しませんでした。");

                   }
               }));
    }

    void GetNormalStageBtns(List<int> Ids)
    {
        StartCoroutine(NetworkManager.Instance.GetNormalStageButtons(Ids,
               result =>
               {                          //登録終了後の処理
                   if (result == true)
                   {
            
                       Debug.Log("完了");
                       btnCnt++;
                       Debug.Log("btnCnt:" + btnCnt + "StageManager.NormalStageIDs.Count" + StageManager.NormalStageIDs.Count);

                   }
                   else
                   {
                       Debug.Log("ユーザー登録が正常に終了しませんでした。");

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

    public void MoveTutorial()
    {
        Initiate.Fade("TutorialScene", Color.black, 1.0f);
    }


    public void playClickSE()
    {
        audioSource.PlayOneShot(ClickSE);
    }
}

