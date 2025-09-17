using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEManager : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] AudioClip ClickSE;
    [SerializeField] AudioClip PutSE;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playClickSE()
    {
        audioSource.PlayOneShot(ClickSE);
    }

    public void playPutSE()
    {
        audioSource.PlayOneShot(PutSE);
    }
}
