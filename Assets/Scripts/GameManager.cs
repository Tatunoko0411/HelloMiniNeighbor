using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool Draging = false;

    [SerializeField] PlayerManager playerManager;

    public bool isStart;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PopObject(Object obj)
    {
        Vector2 mousePos = Input.mousePosition;

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

        GameObject PopObject = Instantiate(obj.gameObject, worldPos,Quaternion.identity);

        Object popObj = PopObject.GetComponent<Object>(); 
        popObj.isDrag = true;
        Draging = true;
    }

    public void StartGame()
    {
        isStart=true;
        playerManager.direction = PlayerManager.DIRECTION_TYPE.RIGHT;
    }

    public void StopGame()
    {
        isStart = false;
        playerManager.direction = PlayerManager.DIRECTION_TYPE.STOP;
    }
}
