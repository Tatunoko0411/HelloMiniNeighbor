using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{

    string Name;
    int userId;

    [SerializeField]InputField nameField;
    [SerializeField]Text IDText;

    // Start is called before the first frame update
    void Start()
    {
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
                      InitDate();
                    Debug.Log("登録完了");
                    Debug.Log(NetworkManager.Instance.ApiToken);
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
