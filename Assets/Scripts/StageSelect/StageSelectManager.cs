using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BackTitle()
    {
        Initiate.Fade("TitleScene",Color.black,1.0f);
    }

    public void SelectStage(int stage)
    {
        StageManager.StageID = stage;
        Initiate.Fade("GameScene", Color.black, 1.0f);

    }
}
