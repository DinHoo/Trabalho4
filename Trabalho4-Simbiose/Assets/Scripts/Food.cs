using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private Color32 color;

    [SerializeField]
    private bool crossedCanal = false;

    bool limiteExit = false;

    public float necessaryTime = 1.5f;
    float elapsed;

    private Game gameRef;

    // Start is called before the first frame update
    private void Start()
    {
        if (!gameRef || gameRef == null)
        {
            gameRef = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
        randomizeColor();
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void randomizeColor()
    {
        color = gameRef.getColours()[Random.Range(0, gameRef.getColours().Length)];

        GetComponent<SpriteRenderer>().color = color;
    }

    public void checkToDestroy()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }

        List<Collider2D> colliders = new List<Collider2D>();

        GetComponent<Rigidbody2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);

        foreach (Collider2D c in colliders)
        {
            if (c.transform.CompareTag("CanalInsides"))
            {
                if (Utilities.isTwoColorsEqual(c.transform.parent.GetComponent<Canal>().GetColour(), color))
                {
                    print("check to destroy");
                    if (gameObject.activeSelf)
                    {
                        crossedCanal = false;
                        gameRef.addToPool(this);
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        if (collision.transform.CompareTag("CanalInsides"))
        {
            GameEventManager.canalColorChange += checkToDestroy;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (!gameObject.activeSelf)
        {
            print("saiu fake");
            return;
        }

        if (other.transform.CompareTag("CanalInsides"))
        {
            if (crossedCanal)
            {
                crossedCanal = false;

                if (Utilities.isTwoColorsEqual(other.transform.parent.GetComponent<Canal>().GetColour(), color))
                {
                    print("on trigger enter");
                    if (gameObject.activeSelf)
                    {
                        crossedCanal = false;

                        gameRef.addToPool(this);
                    }
                }
            }
        }

        if (other.transform.CompareTag("Limite"))
        {

            elapsed += Time.fixedDeltaTime;
            if (elapsed > necessaryTime)
            {
                gameRef.isGameOver = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeSelf)
        {
            print("saiu fake");
            return;
        }
        if (collision.transform.CompareTag("CanalInsides"))
        {
            GameEventManager.canalColorChange -= checkToDestroy;

            if (Utilities.isTwoColorsEqual(collision.transform.parent.GetComponent<Canal>().GetColour(), color))
            {
                print("on trigger exit");

                crossedCanal = false;

                gameRef.addToPool(this);
            }
        }
        if (collision.transform.CompareTag("LimiteDestroy"))
        {
            crossedCanal = true;
        }
        if (collision.transform.CompareTag("Limite"))
        {
            elapsed = 0;
        }
    }
}