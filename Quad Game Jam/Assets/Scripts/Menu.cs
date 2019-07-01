using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject[] flowers;
    public GameObject tuto1;
    public GameObject tuto2;
    public AudioSource audioSource;

    void Start()
    {

    }

    void Update()
    {
        spinFlowers();
    }

    public void ShowTuto1()
    {
        tuto1.SetActive(true);
    }

    public void ShowTuto2()
    {
        tuto2.SetActive(true);
    }

    public void LaunchGame()
    {
        SceneManager.LoadScene("main");
        audioSource.Stop();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void spinFlowers()
    {
        foreach (GameObject flower in flowers)
        {
            flower.transform.Rotate(new Vector3(flower.transform.rotation.x, flower.transform.rotation.y, flower.transform.rotation.z + 1.5f));
        }
    }
}
