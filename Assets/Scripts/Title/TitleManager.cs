using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MoveSelect()
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }

    public void MoveCustomStageSelect()
    {
        Initiate.Fade("CustomStageSelectScene", Color.black, 1.0f);
    }

    public void MoveCreate()
    {
        Initiate.Fade("StageCreateScene", Color.black, 1.0f);
    }
}
