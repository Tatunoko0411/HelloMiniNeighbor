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

    public void GetStages()
    {
        StartCoroutine(NetworkManager.Instance.GetStages(          //–¼‘O
  result =>
  {                          //“o˜^I—¹Œã‚Ìˆ—
      if (result == true)
      {
          SetStageContents();
      }
      else
      {
          Debug.Log("“o˜^‚ª³í‚ÉI—¹‚µ‚Ü‚¹‚ñ‚Å‚µ‚½B");

      }
  }));
    }

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

}

