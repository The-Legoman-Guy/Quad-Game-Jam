using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Gameplay gameplay;
    public GameObject uiPointsGameObject;
    public AudioSource audioSource;

    void Start()
    {
        gameplay = GameObject.FindGameObjectWithTag("Gameplay").GetComponent<Gameplay>();
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Disposal")
        {
            audioSource.Play();
            GameObject uiPointsNew = Instantiate(uiPointsGameObject);
            uiPointsNew.GetComponent<UIPoints>().Score(1000, transform.position);
            gameplay.WinPoints(1000);
            Destroy(this.gameObject);
        }
    }
}
