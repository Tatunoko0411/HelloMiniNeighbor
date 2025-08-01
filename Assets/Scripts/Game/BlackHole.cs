using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    public float lenge;

    GameObject player;

    Object obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Object>();

        if (!obj.CreateMode)
        {
            player = GameObject.Find("Player");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!obj.CreateMode)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            float power = lenge - distance;

            if (power < 0)
            {
                power = 0;
            }

            player.transform.position = Vector2.MoveTowards(player.transform.position,
               transform.position,
               power * Time.deltaTime);
        }
    }
}
