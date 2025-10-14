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
    AudioSource audioSource;
    [SerializeField] AudioClip SE;


    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //�X�e�[�W�J��
    public void loadStage()
    {
        audioSource.PlayOneShot(SE);
        //�X�e�[�W���̐ݒ�
        CustomStageGameManager.StageId = stageId;
        CustomStageGameManager.StartPoint = point;
        Initiate.Fade("CustomStageGameScene", Color.black, 1.0f);
    }
}
