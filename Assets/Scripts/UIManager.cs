using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]StageManager stageManager;
    [SerializeField] GameManager gameManager;

    [SerializeField] UIObject StartButton;
    [SerializeField] UIObject StopButton;


    [SerializeField] UIObject DeleteBox;

    [SerializeField] Text PointTex;

    float ObjBtnHidePosY = -7f;
    float ObjBtnShowPosY = -4f;

    float moveSpeed = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

        if (gameManager.isStart)
        {
            foreach (GameObject button in stageManager.ButtonList)
            {
                button.transform.position = Vector2.MoveTowards(
               button.transform.position,
               new Vector2(button.transform.position.x, ObjBtnHidePosY),
               moveSpeed);
            }

            StartButton.transform.position = Vector2.MoveTowards(
                StartButton.transform.position,
                StartButton.HidePos,
                moveSpeed);

            StopButton.transform.position = Vector2.MoveTowards(
              StopButton.transform.position,
              StopButton.SetPos,
              moveSpeed);

            DeleteBox.transform.position = Vector2.MoveTowards(
               DeleteBox.transform.position,
               DeleteBox.HidePos,
               moveSpeed);

            return;
        }
        else if (!gameManager.isStart)
        {

            StopButton.transform.position = Vector2.MoveTowards(
              StopButton.transform.position,
              StopButton.HidePos,
              moveSpeed);
        }

        if (gameManager.Draging)
        {
           foreach (GameObject button in stageManager.ButtonList)
            {
                button.transform.position = Vector2.MoveTowards(
               button.transform.position,
               new Vector2(button.transform.position.x, ObjBtnHidePosY),
               moveSpeed);
            }

           StartButton.transform.position = Vector2.MoveTowards(
               StartButton.transform.position,
               StartButton.HidePos,
               moveSpeed);

            DeleteBox.transform.position = Vector2.MoveTowards(
               DeleteBox.transform.position,
               DeleteBox.SetPos,
               moveSpeed);
        }
        else
        {
            foreach (GameObject button in stageManager.ButtonList)
            {
                button.transform.position = Vector2.MoveTowards(
               button.transform.position,
               new Vector2(button.transform.position.x, ObjBtnShowPosY),
               moveSpeed);
            }

            StartButton.transform.position = Vector2.MoveTowards(
             StartButton.transform.position,
             StartButton.SetPos,
             moveSpeed);

            DeleteBox.transform.position = Vector2.MoveTowards(
              DeleteBox.transform.position,
              DeleteBox.HidePos,
              moveSpeed);
        }

    }

    public void ChangePointTex()
    {
        PointTex.text = $"残りポイント：{gameManager.point}";
    }
}
