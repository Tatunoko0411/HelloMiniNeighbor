using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-5)]

public class StageManager : MonoBehaviour
{
    [SerializeField]public List<GameObject> ButtonList;
    [SerializeField] List<GameObject> ObjectList;

    [SerializeField]public List<int> ButtonObjIDList;


    // Start is called before the first frame update
    void Start()
    {
        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetButtons()
    {
        for(int i = 0; i < ButtonObjIDList.Count; i++) {
        
            ButtonList[i].GetComponent<ObjectButtonManager>().PopObject 
                = ObjectList[ButtonObjIDList[i]-1].GetComponent<Object>() ;
        }
    }
}
