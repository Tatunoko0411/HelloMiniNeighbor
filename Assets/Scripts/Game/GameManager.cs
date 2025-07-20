using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool Draging = false;

    [SerializeField] PlayerManager playerManager;
    [SerializeField] UIManager uiManager;
    
    public bool isStart;

    public int point;

    public bool isClear;

    // Start is called before the first frame update
    void Start()
    {
        isStart = false;
        uiManager.ChangePointTex();
        isClear = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isClear)
        {
            uiManager.SetClearUI();
            enabled = false;
        }
        
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
        playerManager.rb.bodyType = RigidbodyType2D.Dynamic;
        playerManager.LimBox.SetActive(false);
;
    }

    public void StopGame()
    {
        isStart = false;
        playerManager.direction = PlayerManager.DIRECTION_TYPE.STOP;
        playerManager.rb.bodyType = RigidbodyType2D.Static;
        playerManager.LimBox.SetActive(true);
        playerManager.transform.position = playerManager.StartPos;
    }

    public void changePoint(int value)
    {
        point += value;
        uiManager.ChangePointTex();
    }

    public void Retry()
    {
        Initiate.Fade(SceneManager.GetActiveScene().name, Color.black,1.0f);
    }

    public void MoveStageSelect()
    {
        Initiate.Fade("StageSelectScene", Color.black, 1.0f);
    }
}
