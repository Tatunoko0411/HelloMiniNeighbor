using Assets.Scripts.Game;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManager : MonoBehaviour
{

    // WebAPI�̐ڑ����ݒ�
#if DEBUG
    // �J�����Ŏg�p����l���Z�b�g
    const string API_BASE_URL = "http://localhost:8000/api/";
#else
  // �{�Ԋ��Ŏg�p����l���Z�b�g
  const string API_BASE_URL = "https://�cazure.com/api/";
#endif

    private int userID; // �����̃��[�U�[ID
    private string userName; // ���͂����z��̎����̃��[�U�[��
                             // �v���p�e�B
    public int UserID
    {
        get
        {
            return this.userID;
        }
    }


    public string UserName
    {
        get
        {
            return this.userName;
        }
    }

    private static NetworkManager instance;

    private string apiToken;  //API�g�[�N��
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject gameObj = new GameObject("NetworkManager");
                instance = gameObj.AddComponent<NetworkManager>();
                DontDestroyOnLoad(gameObj);
            }
            return instance;
        }
    }


    // �ʐM�p�̊֐�

    //���[�U�[�o�^����
    public IEnumerator RegistUser(string name, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistUserRequest requestData = new RegistUserRequest();
        requestData.Name = name;
        requestData.Level = 1;
        requestData.Exp = 1;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);

        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            //�ʐM�����������ꍇ�A�Ԃ��Ă���JSON���I�u�W�F�N�g�ɕϊ�
            string resultJson = request.downloadHandler.text;
            RegistUserResponse response =
                         JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);
            //�t�@�C���Ƀ��[�U�[ID��ۑ�
            this.userName = name;
            this.apiToken = response.APIToken;
            this.userID = response.UserID;

            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }


    // ���[�U�[����ۑ�����
    private void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.UserName = this.userName;
        saveData.UserID = this.userID;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
                new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }


    // ���[�U�[����ǂݍ���
    public bool LoadUserData()
    {
        if (!File.Exists(Application.persistentDataPath + "/saveData.json"))
        {
            return false;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/saveData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        SaveData saveData = JsonConvert.DeserializeObject<SaveData>(json);
        this.userID = saveData.UserID;
        this.userName = saveData.UserName;
        return true;
    }


    //���[�U�[���X�V
    public IEnumerator UpdateUser(string name, int level, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.Name = name;
        requestData.Level = 1;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);
        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/update", json, "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);


        yield return request.SendWebRequest();

        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            // �ʐM�����������ꍇ�A�t�@�C���ɍX�V�������[�U�[����ۑ�
            this.userName = name;
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }


    //�X�e�[�W�I�u�W�F�N�g�o�^
    public IEnumerator RegistStageObject(StageObject stageObject,int stageID, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistStageObjectRequest requestData = new RegistStageObjectRequest();
        requestData.StageID = stageID;
        requestData.X = stageObject.Xpos;
        requestData.Y = stageObject.Ypos;
        requestData.Rot = stageObject.Rot;
        requestData.ObjectID = stageObject.ObjectId;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);

        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/object/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }


    //�X�e�[�W�o�^
    public IEnumerator RegistStage(string  name, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
RegistStageRequest requestData = new RegistStageRequest();
        requestData.Name = name;
        requestData.UserID = userID;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);

        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            isSuccess = true;

            string resultJson = request.downloadHandler.text;
            RegistStageResponse response = JsonConvert.DeserializeObject<RegistStageResponse>(resultJson);

            StageCreateManager stageCreateManager = GameObject.FindObjectOfType<StageCreateManager>();
            stageCreateManager.StageId = response.StageID;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }


    //�I�u�W�F�N�g�{�^���o�^
    public IEnumerator RegistObjectButton(int objectID, int stageID, Action<bool> result)
    {
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g���쐬
        RegistStageObjectRequest requestData = new RegistStageObjectRequest();
        requestData.StageID = stageID;
        requestData.ObjectID = objectID;
        //�T�[�o�[�ɑ��M����I�u�W�F�N�g��JSON�ɕϊ�
        string json = JsonConvert.SerializeObject(requestData);

        //���M
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/button/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    //�X�e�[�W���擾
    public IEnumerator GetStages( Action<bool> result)
    {


        //���M
        UnityWebRequest request = UnityWebRequest.Get(
                    API_BASE_URL + "stages/index");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            string resultJson = request.downloadHandler.text;
           List< GetStageResponse> response = JsonConvert.DeserializeObject<List<GetStageResponse>>(resultJson);

            CustomStageSelectManager manager = GameObject.FindObjectOfType<CustomStageSelectManager>();

            foreach ( GetStageResponse responseItem in response )
            {
                manager.customStages.Add(new CustomStageDate(
                    responseItem.ID,
                    responseItem.UserID,
                    responseItem.Name,
                    responseItem.Point,
                    responseItem.playTime,
                    responseItem.clearTime));
            }
         

            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    public IEnumerator GetStageObjects(int stageID,Action<bool> result)
    {


        //���M
        UnityWebRequest request = UnityWebRequest.Get(
                    API_BASE_URL + "stages/object/get/" + stageID);

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            string resultJson = request.downloadHandler.text;
            List<GetStageObjectResponse> response = JsonConvert.DeserializeObject<List<GetStageObjectResponse>>(resultJson);

            CustomStageGameManager manager = GameObject.FindObjectOfType<CustomStageGameManager>();

            foreach (GetStageObjectResponse responseItem in response)
            {
                manager.StageObjects.Add(new StageObject(
                    responseItem.ObjectID,
                    responseItem.X,
                    responseItem.Y,
                    responseItem.Rot));
            }


            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    public IEnumerator GetStageButtons(int stageID, Action<bool> result)
    {


        //���M
        UnityWebRequest request = UnityWebRequest.Get(
                    API_BASE_URL + "stages/button/get/" + stageID);

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            string resultJson = request.downloadHandler.text;
            List<GetButtonResponse> response = JsonConvert.DeserializeObject<List<GetButtonResponse>>(resultJson);

            CustomStageGameManager manager = GameObject.FindObjectOfType<CustomStageGameManager>();

            foreach (GetButtonResponse responseItem in response)
            {
              manager.ButtonObjIDList.Add(responseItem.ObjectID);
            }


            isSuccess = true;
        }
        result?.Invoke(isSuccess); //�����ŌĂяo������result�������Ăяo��
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
