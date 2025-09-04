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

    //�X�e�[�W���̎擾
    public void GetStages()
    {
        StartCoroutine(NetworkManager.Instance.GetStages(          //���O
  result =>
  {                          //�o�^�I����̏���
      if (result == true)
      {
          SetStageContents();
      }
      else
      {
          Debug.Log("�o�^������ɏI�����܂���ł����B");

      }
  }));
    }

    //�X�e�[�W�I���̃I�u�W�F�N�g��ݒu
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

    //�^�C�g���J��
    public void BackTitle()
    {
        Initiate.Fade("TitleScene", Color.black, 1.0f);
    }

}

