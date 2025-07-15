using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public int point;
    
    GameManager gameManager;

    public bool isDrag;

    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (isDrag)
        {
   
           Vector2 mousePos = Input.mousePosition;

          Vector2  worldPos = Camera.main.ScreenToWorldPoint(new Vector2(mousePos.x, mousePos.y));


            Vector2 movePos = Vector2.MoveTowards(
                transform.position,
                worldPos,
                0.7f );

          //Ç±ÇÃå„ï«îªíËïtÇØÇƒà⁄ìÆêßå¿

            transform.position = movePos;

        }
    }

    private void OnMouseDrag()
    {
        isDrag = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void OnMouseUp()
    {
        isDrag = false;
        rb.bodyType = RigidbodyType2D.Static;
    }
}
