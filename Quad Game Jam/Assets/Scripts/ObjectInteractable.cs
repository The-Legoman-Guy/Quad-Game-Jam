using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectInteractable : MonoBehaviour
{
    public float timeNeeded = 5f;
    public float timePassed = 0f;
    public bool isBeingActivated = false;
    public GameObject uiPointsGameObject;
    public bool canBeActivated = true;
    public GameObject firePit;
    public GameObject secondEffect;

    public Gameplay gameplay;
    public Slider timePassedUi;

    // SOUND
    public AudioSource soundBox;

    void Start()
    {
        gameplay = GameObject.FindGameObjectWithTag("Gameplay").GetComponent<Gameplay>();
        timePassedUi.maxValue = timeNeeded;
        timePassedUi.value = timePassed;
        timePassedUi.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        timePassedUi.transform.position = new Vector3(timePassedUi.transform.position.x, timePassedUi.transform.position.y + 50, timePassedUi.transform.position.z);
    }

    void Update()
    {
        if (!canBeActivated)
            return;
        if (isBeingActivated)
            timePassed += Time.deltaTime;

        if (timePassed > 0)
        {
            timePassedUi.gameObject.SetActive(true);
            timePassedUi.value = timePassed;
            if (timePassed > timeNeeded)
            {
                canBeActivated = false;
                timePassedUi.gameObject.SetActive(false);
                GameObject uiPointsNew = Instantiate(uiPointsGameObject);
                if (this.gameObject.tag == "PileOfTires")
                {
                    soundBox.Play();
                    Instantiate(firePit, transform.position, new Quaternion());
                    uiPointsNew.GetComponent<UIPoints>().Score(750, transform.position);
                    gameplay.WinPoints(750);
                }
                if (this.gameObject.tag == "Gaz")
                {
                    soundBox.Play();
                    Instantiate(firePit, transform.position, new Quaternion());
                    uiPointsNew.GetComponent<UIPoints>().Score(500, transform.position);
                    gameplay.WinPoints(500);
                }
                if (this.gameObject.tag == "Baril")
                {
                    soundBox.Play();
                    Instantiate(firePit, transform.position, new Quaternion());
                    uiPointsNew.GetComponent<UIPoints>().Score(600, transform.position);
                    gameplay.WinPoints(600);
                }
                if (this.gameObject.tag == "Cow")
                {
                    soundBox.Play();
                    secondEffect.SetActive(true);
                    uiPointsNew.GetComponent<UIPoints>().Score(400, transform.position);
                    gameplay.WinPoints(400);
                }
                this.gameObject.tag = "Untagged";
            }
        }
    }
}
