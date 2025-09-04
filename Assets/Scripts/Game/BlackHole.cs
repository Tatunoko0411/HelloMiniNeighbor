using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float lenge;

    GameObject player;

    Object obj;

    [SerializeField]Transform centerPos;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Object>();

     
            player = GameObject.Find("Player");
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!obj.CreateMode)
        {//プレイヤーの距離に応じてプレイヤーを引き寄せる
            float distance = Vector2.Distance(centerPos.position, player.transform.position);

            float power = lenge - distance;

            if (power < 0)
            {
                power = 0;
            }

            player.transform.position = Vector2.MoveTowards(player.transform.position,
               centerPos.position,
               power * Time.deltaTime);
        }
    }
}
