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
                      InitDate();
                    Debug.Log("�o�^����");
                    Debug.Log(NetworkManager.Instance.ApiToken);
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
