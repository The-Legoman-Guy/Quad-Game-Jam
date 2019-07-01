using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPoints : MonoBehaviour
{
    public Text points;
    public float life = 1.5f;
    public float maxLife = 1.5f;
    public bool activated = false;

    void Start()
    {
        
    }

    void Update()
    {
        if (activated)
        {
            life -= Time.deltaTime;
            if (life < maxLife)
            {
                points.transform.position = new Vector3(points.transform.position.x, points.transform.position.y + 1, points.transform.position.z);
            }
            if (life < 0)
                Destroy(this.gameObject);
            if (life < (maxLife / 2))
            {
                Color n = points.color;
                n.a -= 0.05f;
                points.color = n;
            }
        }
    }

    public void Score(int pointsComming, Vector3 pos)
    {
        points.transform.position = Camera.main.WorldToScreenPoint(pos);
        points.transform.position = new Vector3(points.transform.position.x, points.transform.position.y + 50, points.transform.position.z);
        points.text = pointsComming.ToString();
        activated = true;
    }
}
