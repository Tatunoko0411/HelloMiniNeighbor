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

    Vector3 startPosition; // �J�n���_�̃I�u�W�F�N�g�̈ʒu
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

    //�ړ�����
    private void Move()
    {
        float goalY = 0.0f; // �ړI�n�_��y���W
        if (direction == DIRECTION_TYPE.TOP)
        {
            goalY = 1.5f;
            direction = DIRECTION_TYPE.BOTTOM; // ������؂�ւ�
        }
        else if (direction == DIRECTION_TYPE.BOTTOM)
        {
            goalY = -1.5f;
            direction = DIRECTION_TYPE.TOP; // ������؂�ւ�
        }

        // �ړI�n��ݒ�
        Vector3 goalPosition = startPosition + new Vector3(0.0f, goalY, 0.0f);

        // �ړI�n�܂�3.0f�b�����Ĉړ�
        this.transform.DOMove(goalPosition, 3.0f).SetEase(Ease.InOutQuad).OnComplete(Move);

    }


}
