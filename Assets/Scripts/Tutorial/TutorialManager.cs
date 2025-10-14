using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    int count;//チュートリアルの進行度
    [SerializeField]List<GameObject> tutorialObjcts;
    [SerializeField]GameObject LimField;
    [SerializeField] CircleCollider2D putPoint;
    [SerializeField]GameManager gameManager;

    public enum TutorialProgress
    {
        start = 0,
        point,
        button,
        delete_box,
        chara,
        put,
        startButton,
        suger,
        move,

    }
    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        ChangeTutorial();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if (count != (int)TutorialProgress.put&& count != (int)TutorialProgress.move)
            {
                count++;
                Debug.Log(count);
                ChangeTutorial();
            }
        }

        if (gameManager.Draging == false)
        {
            if (count == (int)TutorialProgress.put)
            {
               Collider2D hitCollider = Physics2D.OverlapCircle(putPoint.transform.position, 0.1f);

                if (hitCollider != null)
                {
                    Debug.Log("オブジェクトがおかれてます");
                    count = (int)TutorialProgress.startButton;
                    Debug.Log(count);
                    ChangeTutorial();
                }
            }
        }
    }

    void ChangeTutorial()
    {
        foreach (GameObject obj in tutorialObjcts)
        {
            obj.SetActive(false);
        }

        if(count == (int)TutorialProgress.put|| count == (int)TutorialProgress.move)
        {
            LimField.SetActive(false);
            return;
        }

        tutorialObjcts[count].SetActive(true);
    }
}
