using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectButtonManager : MonoBehaviour
{
    GameManager gameManager;
    EventTrigger eventTrigger;
    Button button;
    public int Cost;

    public Object PopObject;
    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        eventTrigger = GetComponent<EventTrigger>();
        button = GetComponent<Button>();

        Cost = PopObject.cost;

        SetEvent();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameManager.point < Cost)
        {
            button.interactable = false;
            eventTrigger.enabled = false;
        }
        if (gameManager.point > Cost)
        {
            button.interactable = true;
            eventTrigger.enabled = true;
        }
    }

    public void SetEvent()
    {
        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerDown;
        entry.callback.AddListener((eventDate) => { gameManager.PopObject(PopObject); gameManager.changePoint(-Cost); });
        eventTrigger.triggers.Add(entry);
    }
}
