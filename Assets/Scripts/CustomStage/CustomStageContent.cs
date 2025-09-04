using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomStageContent : MonoBehaviour
{
    public int stageId;
    public int point;
    [SerializeField] public Text title;
    [SerializeField] public Text playerName;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�e�[�W�J��
    public void loadStage()
    {
        CustomStageGameManager.StageId = stageId;
        CustomStageGameManager.StartPoint = point;
        Initiate.Fade("CustomStageGameScene", Color.black, 1.0f);
    }
}
