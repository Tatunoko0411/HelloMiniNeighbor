using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static PlayerManager;

public class Bee : MonoBehaviour
{
    public enum DIRECTION_TYPE
    {
        STOP,
        TOP,
        BOTTOM,
    }

    DIRECTION_TYPE direction;

    Vector3 startPosition; // 開始時点のオブジェクトの位置
    float speed;
    bool isMove;

    Object obj;

    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Object>();

        direction = DIRECTION_TYPE.STOP;

        startPosition = transform.position;

        if (!obj.CreateMode)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!obj.CreateMode)
        {
            
            if (gameManager.isStart)
            {
                if (!isMove)
                {
             
                    direction = DIRECTION_TYPE.TOP;
                    isMove = true;
                    Move();

                }
            }
            else if (!gameManager.isStart)
            {
                if (isMove)
                {
                    transform.position = startPosition;
                    direction = DIRECTION_TYPE.STOP;
                    isMove = false;
                }
            }
        }

    }

    //移動処理
    private void Move()
    {
        float goalY = 0.0f; // 目的地点のy座標
        if (direction == DIRECTION_TYPE.TOP)
        {
            goalY = 1.5f;
            direction = DIRECTION_TYPE.BOTTOM; // 方向を切り替え
        }
        else if (direction == DIRECTION_TYPE.BOTTOM)
        {
            goalY = -1.5f;
            direction = DIRECTION_TYPE.TOP; // 方向を切り替え
        }

        // 目的地を設定
        Vector3 goalPosition = startPosition + new Vector3(0.0f, goalY, 0.0f);

        // 目的地まで3.0f秒かけて移動
        this.transform.DOMove(goalPosition, 3.0f).SetEase(Ease.InOutQuad).OnComplete(Move);

    }


}
