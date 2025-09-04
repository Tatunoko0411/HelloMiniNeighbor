using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class faucet : MonoBehaviour
{

    [SerializeField] Transform DropPos;
    [SerializeField] GameObject DropObj;
    public float waitTime;

   public GameManager gameManager;
    Object obj;
    // Start is called before the first frame update
    void Start()
    {
        obj = GetComponent<Object>();

        if (!obj.CreateMode)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

            StartCoroutine(dropWater());
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //一定間隔でオブジェクト生成
    IEnumerator dropWater()
    {
        Debug.Log("開始");

        while (true)
        {

            yield return new WaitForSeconds(waitTime);

            if (gameManager.isStart)
            {
                GameObject water = Instantiate(DropObj,
                    DropPos.position,
                    Quaternion.identity);

                Destroy(water, 2.0f);
            }
        }

    }
}
