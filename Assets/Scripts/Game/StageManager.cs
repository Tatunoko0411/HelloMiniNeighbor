using Assets.Scripts.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DefaultExecutionOrder(-5)]

public class StageManager : MonoBehaviour
{
    [SerializeField]public List<GameObject> ButtonList;
    [SerializeField] List<GameObject> ObjectList;

    [SerializeField]public List<int> ButtonObjIDList;

    public List<StageObject> StageObjects = new List<StageObject>()
    {
        new StageObject(1,-7,-3,0), new StageObject(1,8,-3,0)
    };


    // Start is called before the first frame update
    void Start()
    {
        SetButtons();
        SetObject();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtons()
    {
        for(int i = 0; i < ButtonObjIDList.Count; i++) {
        
            ButtonList[i].GetComponent<ObjectButtonManager>().PopObjectPrefab 
                = ObjectList[ButtonObjIDList[i]-1].GetComponent<Object>() ;
        }
    }

    public void SetObject()
    {
        for (int i = 0; i < StageObjects.Count; i++)
        {
          GameObject SetObj =   Instantiate(ObjectList[StageObjects[i].ObjectId -1],
                new Vector3(StageObjects[i].Xpos, StageObjects[i].Ypos),
                Quaternion.identity);

            SetObj.transform.Rotate(new Vector3(0,0, StageObjects[i].Rot));

          Object obj = SetObj.GetComponent<Object>();

            obj.isFixed = true;
        }
    }
}
