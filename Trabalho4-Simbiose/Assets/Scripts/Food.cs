using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    Color32 color;

    Game gameRef;

    // Start is called before the first frame update
    void Start()
    {
        if (!gameRef || gameRef == null)
        {
            gameRef = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>();
        }
        randomizeColor();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void randomizeColor()
    {
        color = gameRef.getColours()[Random.Range(0, gameRef.getColours().Length)];

        GetComponent<SpriteRenderer>().color = color;
    }

    public void checkToDestroy()
    {
        List<Collider2D> colliders = new List<Collider2D>();

        GetComponent<Rigidbody2D>().OverlapCollider(new ContactFilter2D().NoFilter(), colliders);

        foreach(Collider2D c in colliders)
        {
            if (c.transform.CompareTag("CanalInsides"))
            {
                if (Utilities.isTwoColorsEqual(c.transform.parent.GetComponent<Canal>().GetColour(), color))
                {
                    gameRef.addToPool(this);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CanalInsides"))
        {
            GameEventManager.canalColorChange += checkToDestroy;

            if (Utilities.isTwoColorsEqual(collision.transform.parent.GetComponent<Canal>().GetColour(), color))
            {
                gameRef.addToPool(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("CanalInsides"))
        {
            GameEventManager.canalColorChange -= checkToDestroy;

            if (Utilities.isTwoColorsEqual(collision.transform.parent.GetComponent<Canal>().GetColour(), color))
            {
                gameRef.addToPool(this);
            }
        }
    }
}
