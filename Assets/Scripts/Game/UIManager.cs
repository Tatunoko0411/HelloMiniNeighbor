using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]StageManager stageManager;
    [SerializeField] GameManager gameManager;
    [SerializeField] CustomStageGameManager customManager;

    [SerializeField] UIObject StartButton;
    [SerializeField] UIObject StopButton;

    [SerializeField]GameObject ObjectButtons;

    [SerializeField] UIObject DeleteBox;

    [SerializeField] Text PointTex;

    [SerializeField] GameObject ClearUI;

    float ObjBtnHidePosY = -7f;
    float ObjBtnShowPosY = -4f;

    float moveSpeed = 0.2f;

    public bool isCustom = false;

    // Start is called before the first frame update
    void Start()
    {

            ClearUI.SetActive(false);
      
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCustom )
        {
            UpdateNormalUI();
        }

        if (isCustom )
        {
            UpdateCustomUI();
        }

    }

    public void ChangePointTex()
    {

            PointTex.text = $":{gameManager.point}";
        
    }

    public void SetClearUI()
    {
        ClearUI.SetActive(true);
    }

    public void UpdateNormalUI()
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

    public void UpdateCustomUI()
    {
        if (gameManager.isStart)
        {
            foreach (GameObject button in customManager.ButtonList)
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
            foreach (GameObject button in customManager.ButtonList)
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
            foreach (GameObject button in customManager.ButtonList)
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

    //オブジェクト生成系ボタンの表示切り替え
    public void changeObjectButtons()
    {
        if (ObjectButtons.activeSelf)
        {
            ObjectButtons.SetActive(false);
        }
        else
        {
            ObjectButtons.SetActive(true);
        }
    }

}
