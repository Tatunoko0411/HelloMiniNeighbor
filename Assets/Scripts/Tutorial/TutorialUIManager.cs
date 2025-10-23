using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUIManager : MonoBehaviour
{
   
    [SerializeField] GameManager gameManager;
   

    [SerializeField] UIObject StartButton;
    [SerializeField] UIObject StopButton;

    [SerializeField] GameObject ObjectButtons;


    float ObjBtnHidePosY = -200f;
    float ObjBtnShowPosY = 200f;

    float moveSpeed = 10f;

    public bool isCustom = false;

    // Start is called before the first frame update
    void Start()
    {

        

    }

    // Update is called once per frame
    void Update()
    {
        if (!isCustom)
        {
            UpdateNormalUI();
        }


    }


    public void UpdateNormalUI()
    {
        if (gameManager.isStart)
        {



                ObjectButtons.transform.position = Vector2.MoveTowards(
               ObjectButtons.transform.position,
               new Vector2(ObjectButtons.transform.position.x, ObjBtnHidePosY),
              moveSpeed);
            

            StartButton.transform.position = Vector2.MoveTowards(
                StartButton.transform.position,
                StartButton.HidePos,
                moveSpeed);

            StopButton.transform.position = Vector2.MoveTowards(
              StopButton.transform.position,
              StopButton.SetPos,
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
               ObjectButtons.transform.position = Vector2.MoveTowards(
               ObjectButtons.transform.position,
               new Vector2(ObjectButtons.transform.position.x, ObjBtnHidePosY),
               moveSpeed);
            

            StartButton.transform.position = Vector2.MoveTowards(
                StartButton.transform.position,
                StartButton.HidePos,
                moveSpeed);

        }
        else
        {
           
                ObjectButtons.transform.position = Vector2.MoveTowards(
               ObjectButtons.transform.position,
               new Vector2(ObjectButtons.transform.position.x, ObjBtnShowPosY),
               moveSpeed);
            

            StartButton.transform.position = Vector2.MoveTowards(
             StartButton.transform.position,
             StartButton.SetPos,
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
