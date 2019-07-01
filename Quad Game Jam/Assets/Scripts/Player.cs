using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 0.05f;
    public float life = 10f;
    public bool alive = true;
    public GameObject actualObject = null;
    public GameObject objectNearPlayer = null;
    public ObjectInteractable objectInteractableNearPlayer = null;
    public bool nearDisposal = false;
    public Gameplay gameplay;
    public GameObject uiPointsGameObject;
    public RuntimeAnimatorController animatorMale;
    public RuntimeAnimatorController animatorFemale;
    public Animator animator;
    public Sprite deadMale;
    public Sprite deadWomen;
    public bool isMale = false;

    // POSIBLE LITTERS
    public GameObject can;

    // SOUND
    public AudioSource soundBox;
    public AudioClip ploufSound;
    public AudioClip scoreSound;
    public AudioClip throwObjectSound;

    void Start()
    {
        animator = this.GetComponent<Animator>();
        gameplay = GameObject.FindGameObjectWithTag("Gameplay").GetComponent<Gameplay>();
    }

    public void SetSex(bool sex)
    {
        isMale = sex;
        animator = this.GetComponent<Animator>();
        if (isMale)
            animator.runtimeAnimatorController = animatorMale;
        else
            animator.runtimeAnimatorController = animatorFemale;
    }

    void Update()
    {
        if (alive)
        {
            Movement();
            if (actualObject != null)
                actualObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1, -0.1f);
            life -= Time.deltaTime;
            if (life < 0)
            {
                alive = false;
                if (objectInteractableNearPlayer != null)
                    objectInteractableNearPlayer.isBeingActivated = false;
                objectInteractableNearPlayer = null;
                animator.enabled = false;
                this.gameObject.GetComponent<Collider2D>().isTrigger = true;
                if (isMale)
                    this.GetComponent<SpriteRenderer>().sprite = deadMale;
                else
                    this.GetComponent<SpriteRenderer>().sprite = deadWomen;
            }
        }
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            DoOneAnimation("Up");
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y + speed, this.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.S))
        {
            DoOneAnimation("Down");
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y - speed, this.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            DoOneAnimation("Left");
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x - speed, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        if (Input.GetKey(KeyCode.D))
        {
            DoOneAnimation("Right");
            this.gameObject.transform.position = new Vector3(this.gameObject.transform.position.x + speed, this.gameObject.transform.position.y, this.gameObject.transform.position.z);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (actualObject == null)
            {
                if (objectNearPlayer != null)
                {
                    actualObject = objectNearPlayer;
                    actualObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2, -0.1f);
                }
            }
            else if (actualObject != null)
            {
                if (nearDisposal)
                {
                    soundBox.clip = ploufSound;
                    soundBox.Play();
                    if (actualObject.tag.ToString() == "Can")
                    {
                        GameObject uiPointsNew = Instantiate(uiPointsGameObject);
                        uiPointsNew.GetComponent<UIPoints>().Score(100, transform.position);
                        gameplay.WinPoints(100);
                    }
                    if (actualObject.tag.ToString() == "Trash")
                    {
                        GameObject uiPointsNew = Instantiate(uiPointsGameObject);
                        uiPointsNew.GetComponent<UIPoints>().Score(200, transform.position);
                        gameplay.WinPoints(200);
                    }
                    if (actualObject.tag.ToString() == "Player")
                    {
                        GameObject uiPointsNew = Instantiate(uiPointsGameObject);
                        uiPointsNew.GetComponent<UIPoints>().Score(120, transform.position);
                        gameplay.WinPoints(120);
                    }
                    Destroy(actualObject);
                }
                else
                {
                    soundBox.clip = throwObjectSound;
                    soundBox.Play();
                    actualObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -0.1f);
                    actualObject = null;
                }
            }
        }
        if (Input.GetKey(KeyCode.Space) && actualObject == null && objectInteractableNearPlayer != null)
        {
            DoOneAnimation("Action");
            objectInteractableNearPlayer.isBeingActivated = true;
        }
        else if (objectInteractableNearPlayer != null)
            objectInteractableNearPlayer.isBeingActivated = false;
    }

    public void DoOneAnimation(string lol)
    {
        if (lol != "Up")
            animator.SetBool("Up", false);
        if (lol != "Down")
            animator.SetBool("Down", false);
        if (lol != "Left")
            animator.SetBool("Left", false);
        if (lol != "Right")
            animator.SetBool("Right", false);
        if (lol != "Action")
            animator.SetBool("Action", false);
        animator.SetBool(lol, true);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Can" || collision.tag == "Player" || collision.tag == "Trash")
            objectNearPlayer = collision.gameObject;
        if (collision.tag == "PileOfTires" || collision.tag == "Gaz" || collision.tag == "Cow" || collision.tag == "Baril")
            objectInteractableNearPlayer = collision.gameObject.GetComponent<ObjectInteractable>();
        if (collision.tag == "Disposal")
            nearDisposal = true;
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Can" || collision.tag == "Player" || collision.tag == "Trash")
            objectNearPlayer = null;
        if (collision.tag == "PileOfTires" || collision.tag == "Gaz" || collision.tag == "Cow" || collision.tag == "Baril")
        {
            if (objectInteractableNearPlayer != null)
                objectInteractableNearPlayer.isBeingActivated = false;
            objectInteractableNearPlayer = null;
        }
        if (collision.tag == "Disposal")
            nearDisposal = false;
    }
}
