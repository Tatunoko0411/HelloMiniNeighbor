using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool Draging = false;

    [SerializeField] PlayerManager playerManager;
    [SerializeField] UIManager uiManager;
    
    public bool isStart;

    public int point;

    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        uiManager.ChangePointTex();
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClear)
        {
            uiManager.SetClearUI();
            enabled = false;
        }
        
    }


 
    public void StartGame()
    {
        isStart=true;
        playerManager.StartPlayer();

    }

    public void StopGame()
    {
        isStart = false;
        playerManager.StopPlayer();
    }

    public void changePoint(int value)
    {
        point += value;
        uiManager.ChangePointTex();
    }

    public void Retry()
    {
        Initiate.Fade(SceneManager.GetActiveScene().name, Color.black,1.0f);
    }

    public void MoveStageSelect()
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }
}
