using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class CustomStageSelectManager : MonoBehaviour
{
    public List<CustomStageDate> customStages = new List<CustomStageDate>();

    [SerializeField] public GameObject stageContentPrefab;
    [SerializeField] public GameObject ContentParent;
    // Start is called before the first frame update
    void Start()
    {
        GetStages();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //ステージ情報の取得
    public void GetStages()
    {
        StartCoroutine(NetworkManager.Instance.GetStages(          //名前
  result =>
  {                          //登録終了後の処理
      if (result == true)
      {
          SetStageContents();
      }
      else
      {
          Debug.Log("登録が正常に終了しませんでした。");

      }
  }));
    }

    //ステージ選択のオブジェクトを設置
    public void SetStageContents()
    {
        foreach (CustomStageDate date in customStages)
        {
            GameObject obj = Instantiate(stageContentPrefab,
                ContentParent.transform.position,
                Quaternion.identity,
                ContentParent.transform
                );

            CustomStageContent content = obj.GetComponent<CustomStageContent>();

            content.stageId = date.Id;
            content.title.text = date.Name;
            content.point = date.Point;
            
        }
    }

    //タイトル遷移
    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

}

