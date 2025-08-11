using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ObjectButtonManager : MonoBehaviour
{
    GameManager gameManager;
    EventTrigger eventTrigger;
    Button button;
    public int Cost;

    public int ID;

    public Object PopObjectPrefab;

    public bool CreateMode;//クリエイトモードかどうか
    // Start is called before the first frame update
    void Start()
    {
        if (!CreateMode)
        {
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            Cost = PopObjectPrefab.cost;
        }
        eventTrigger = GetComponent<EventTrigger>();
        button = GetComponent<Button>();



       
        SetEvent();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!CreateMode)
        {


            if (gameManager.point < Cost)
            {
                button.interactable = false;
                eventTrigger.enabled = false;
            }
            if (gameManager.point >= Cost)
            {
                button.interactable = true;
                eventTrigger.enabled = true;
            }
        }
    }

    public void SetEvent()
    {
        if (CreateMode)
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((eventDate) => { PopObject(PopObjectPrefab);});
            eventTrigger.triggers.Add(entry);
        }
        else
        {
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = EventTriggerType.PointerDown;
            entry.callback.AddListener((eventDate) => {PopObject(PopObjectPrefab); gameManager.changePoint(-Cost); });
            eventTrigger.triggers.Add(entry);
        }
    }


    public void PopObject(Object obj)
    {
        if(obj == null)
        { return; }

        Vector2 mousePos = Input.mousePosition;

        Vector2 worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));

        GameObject PopObject = Instantiate(obj.gameObject, worldPos, Quaternion.identity);

        Object popObj = PopObject.GetComponent<Object>();
        popObj.isDrag = true;

        Rigidbody2D rigidbody = PopObject.GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;


        if (gameManager != null)
        {
            gameManager.Draging = true;
        }
        else
        {
            popObj.CreateMode = true;
        }

 
    }

}
