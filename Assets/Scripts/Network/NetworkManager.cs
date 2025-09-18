using Assets.Scripts.Game;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Rendering;

public class NetworkManager : MonoBehaviour
{

    // WebAPIの接続先を設定
#if DEBUG
    // 開発環境で使用する値をセット
  //  const string API_BASE_URL = "http://localhost:8000/api/";
    const string API_BASE_URL = "http://ge202402.japaneast.cloudapp.azure.com/api/";
#else
    // 本番環境で使用する値をセット
    const string API_BASE_URL = "http://ge202402.japaneast.cloudapp.azure.com/api/";
#endif

    private int userID; // 自分のユーザーID
    private string userName; // 入力される想定の自分のユーザー名
    private string apiToken;  //APIトークン

   

                              // プロパティ
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

    public string ApiToken
    {
        get
        {
            return this.apiToken;
        }
    }

    private static NetworkManager instance;


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


    // 通信用の関数

    //ユーザー登録処理
    public IEnumerator RegistUser(string name, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistUserRequest requestData = new RegistUserRequest();
        requestData.Name = name;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);

        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            //通信が成功した場合、返ってきたJSONをオブジェクトに変換
            string resultJson = request.downloadHandler.text;
            RegistUserResponse response =
                         JsonConvert.DeserializeObject<RegistUserResponse>(resultJson);
            //ファイルにユーザーIDを保存
            this.userName = name;
            this.apiToken = response.APIToken;
            this.userID = response.UserID;

            SaveUserData();
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    // ステージ情報を保存する
    public void SaveStage()
    {
       List<StageData> stageData = new List<StageData>();

        for (int i = 0; i < StageManager.NormalStageIDs.Count; i++)
        {
            stageData.Add(new StageData(
                StageManager.NormalstagesObjects[i],
                StageManager.NormalStageButtonIDs[i],
                StageManager.NormalStagePoints[i]
                ));
        }

        string json = JsonConvert.SerializeObject(stageData);
        var writer =
                new StreamWriter(Application.persistentDataPath + "/stageData.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }


    // ステージ情報を読み込む
    public bool LoadStage()
    {
        if (!File.Exists(Application.persistentDataPath + "/stageData.json"))
        {
            return false;
        }
        var reader =
                   new StreamReader(Application.persistentDataPath + "/stageData.json");
        string json = reader.ReadToEnd();
        reader.Close();
        List<StageData> saveData = JsonConvert.DeserializeObject<List<StageData>>(json);
        foreach (StageData data in saveData)
        {
            StageManager.NormalStagePoints.Add(data.Point);
            StageManager.NormalstagesObjects.Add(data.objects);
            StageManager.NormalStageButtonIDs.Add(data.ButtonObjectIDLIst);
        }
        return true;
    }

    // ステージ情報を保存する
    public void SaveUserData()
    {
        SaveData saveData = new SaveData();
        saveData.UserName = this.userName;
        saveData.UserID = this.userID;
        saveData.apiToken = this.apiToken;
        string json = JsonConvert.SerializeObject(saveData);
        var writer =
                new StreamWriter(Application.persistentDataPath + "/saveData.json");
        writer.Write(json);
        writer.Flush();
        writer.Close();
    }

    public IEnumerator GetNormalStages( Action<bool> result)
    {
        //送信
        UnityWebRequest request = UnityWebRequest.Get(
                    API_BASE_URL + "stages/0");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            string resultJson = request.downloadHandler.text;
            List<GetStageResponse> response = JsonConvert.DeserializeObject<List<GetStageResponse>>(resultJson);

            ;

            foreach (GetStageResponse responseItem in response)
            {
                StageManager.NormalStageIDs.Add(responseItem.ID);
                StageManager.NormalStagePoints.Add(responseItem.Point);

              
            }


            isSuccess = true;
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }


    // ユーザー情報を読み込む
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
        this.apiToken = saveData.apiToken;
        return true;
    }


    //ユーザー情報更新
    public IEnumerator UpdateUser(string name, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        UpdateUserRequest requestData = new UpdateUserRequest();
        requestData.Name = name;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);
        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "users/update", json, "application/json");
        request.SetRequestHeader("Authorization", "Bearer " + this.apiToken);


        yield return request.SendWebRequest();

        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
         && request.responseCode == 200)
        {
            // 通信が成功した場合、ファイルに更新したユーザー名を保存
            this.userName = name;
            SaveUserData();
            isSuccess = true;
        }

        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }


    //ステージオブジェクト登録
    public IEnumerator RegistStageObject(StageObject stageObject,int stageID, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistStageObjectRequest requestData = new RegistStageObjectRequest();
        requestData.StageID = stageID;
        requestData.X = stageObject.Xpos;
        requestData.Y = stageObject.Ypos;
        requestData.Rot = stageObject.Rot;
        requestData.ObjectID = stageObject.ObjectId;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);

        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/object/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }


    //ステージ登録
    public IEnumerator RegistStage(string  name,int point, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
RegistStageRequest requestData = new RegistStageRequest();
        requestData.Name = name;
        requestData.UserID = this.userID;
        requestData.Point = point;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);

        //送信
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
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    public IEnumerator RegistNormalStage(string name, int point, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistStageRequest requestData = new RegistStageRequest();
        requestData.Name = name;
        requestData.UserID = 0;//ノーマルモードのステージはID0で登録
        requestData.Point = point;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);

        //送信
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
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    //オブジェクトボタン登録
    public IEnumerator RegistObjectButton(int objectID, int stageID, Action<bool> result)
    {
        //サーバーに送信するオブジェクトを作成
        RegistStageObjectRequest requestData = new RegistStageObjectRequest();
        requestData.StageID = stageID;
        requestData.ObjectID = objectID;
        //サーバーに送信するオブジェクトをJSONに変換
        string json = JsonConvert.SerializeObject(requestData);

        //送信
        UnityWebRequest request = UnityWebRequest.Post(
                    API_BASE_URL + "stages/button/store", json, "application/json");

        yield return request.SendWebRequest();
        bool isSuccess = false;
        if (request.result == UnityWebRequest.Result.Success
            && request.responseCode == 200)
        {
            isSuccess = true;
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    //ステージ情報取得a
    public IEnumerator GetStages( Action<bool> result)
    {


        //送信
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
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    public IEnumerator GetStageObjects(int stageID,Action<bool> result)
    {


        //送信
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
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    public IEnumerator GetNormelStageObjects(List<int> Ids, Action<bool> result)
    {
        bool isSuccess = false;
        foreach (int id in Ids)
        {
            //送信
            UnityWebRequest request = UnityWebRequest.Get(
                        API_BASE_URL + "stages/object/get/" + id);

            yield return request.SendWebRequest();
          
            if (request.result == UnityWebRequest.Result.Success
                && request.responseCode == 200)
            {
                string resultJson = request.downloadHandler.text;
                List<GetStageObjectResponse> response = JsonConvert.DeserializeObject<List<GetStageObjectResponse>>(resultJson);

                List<StageObject> stageObjects = new List<StageObject>();

                foreach (GetStageObjectResponse responseItem in response)
                {
                    stageObjects.Add(new StageObject(
                        responseItem.ObjectID,
                        responseItem.X,
                        responseItem.Y,
                        responseItem.Rot));
                }

                StageManager.NormalstagesObjects.Add(stageObjects);
            }

            isSuccess = true;
        }
        
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }


    public IEnumerator GetStageButtons(int stageID, Action<bool> result)
    {


        //送信
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
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
    }

    public IEnumerator GetNormalStageButtons(List<int> Ids, Action<bool> result)
    {
        bool isSuccess = false;
        foreach (int id in Ids)
        {

            //送信
            UnityWebRequest request = UnityWebRequest.Get(
                        API_BASE_URL + "stages/button/get/" + id);

            yield return request.SendWebRequest();
           
            if (request.result == UnityWebRequest.Result.Success
                && request.responseCode == 200)
            {
                string resultJson = request.downloadHandler.text;
                List<GetButtonResponse> response = JsonConvert.DeserializeObject<List<GetButtonResponse>>(resultJson);

                List<int> buttonIds = new List<int>();
                foreach (GetButtonResponse responseItem in response)
                {
                    buttonIds.Add(responseItem.ObjectID);
                }

                StageManager.NormalStageButtonIDs.Add(buttonIds);
                isSuccess = true;
            }
        }
        result?.Invoke(isSuccess); //ここで呼び出し元のresult処理を呼び出す
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
