using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

public class StageCreateUIManager : MonoBehaviour
{

    [SerializeField] StageCreateManager stageCreateManager;



    [SerializeField] UIObject DeleteBox;
    [SerializeField] UIObject PopObjectButtons;
    [SerializeField] UIObject ObjectsView;

    [SerializeField] UIObject TryButton;
    [SerializeField] UIObject TiteButton;
    [SerializeField] UIObject PointInput;



    float moveSpeed = 0.2f;

    public bool isCustom = false;

    private bool changeButtons;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if (stageCreateManager.Draging)
        {

            PopObjectButtons.transform.position = Vector2.MoveTowards(
               PopObjectButtons.transform.position,
                PopObjectButtons.SetPos,
               moveSpeed);
            

            DeleteBox.transform.position = Vector2.MoveTowards(
                DeleteBox.transform.position,
                DeleteBox.SetPos,
                moveSpeed);

            ObjectsView.transform.position = Vector2.MoveTowards(
               ObjectsView.transform.position,
               ObjectsView.HidePos,
               moveSpeed);

            TryButton.transform.position = Vector2.MoveTowards(
               TryButton.transform.position,
               TryButton.HidePos,
               moveSpeed);

            TiteButton.transform.position = Vector2.MoveTowards(
               TiteButton.transform.position,
               TiteButton.HidePos,
               moveSpeed);

            PointInput.transform.position = Vector2.MoveTowards(
               PointInput.transform.position,
               PointInput.HidePos,
               moveSpeed);
        }
        else
        {
            if (changeButtons)
            {

                ObjectsView.transform.position = Vector2.MoveTowards(
                   ObjectsView.transform.position,
                   ObjectsView.HidePos,
                   moveSpeed);


                PopObjectButtons.transform.position = Vector2.MoveTowards(
                   PopObjectButtons.transform.position,
                   PopObjectButtons.SetPos,
                   moveSpeed);

            }
            else
            {
                ObjectsView.transform.position = Vector2.MoveTowards(
              ObjectsView.transform.position,
              ObjectsView.SetPos,
              moveSpeed);

                PopObjectButtons.transform.position = Vector2.MoveTowards(
                                 PopObjectButtons.transform.position,
                                 PopObjectButtons.HidePos,
                                 moveSpeed);

            }

            DeleteBox.transform.position = Vector2.MoveTowards(
                DeleteBox.transform.position,
                DeleteBox.HidePos,
                moveSpeed);

            TryButton.transform.position = Vector2.MoveTowards(
               TryButton.transform.position,
               TryButton.SetPos,
               moveSpeed);

            TiteButton.transform.position = Vector2.MoveTowards(
               TiteButton.transform.position,
               TiteButton.SetPos,
               moveSpeed);

            PointInput.transform.position = Vector2.MoveTowards(
               PointInput.transform.position,
               PointInput.SetPos,
               moveSpeed);
        }
    }

    //オブジェクト生成系ボタンの表示切り替え
    public void changeObjectButtons()
    {
        if(changeButtons)
        {
            changeButtons = false;
        }
        else
        {
            changeButtons = true;
        }
    }
}
