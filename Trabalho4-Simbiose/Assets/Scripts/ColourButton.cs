using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourButton : MonoBehaviour
{
    Color32 currentColour;
    Color32[] colours;

    [SerializeField]
    Canal canal;

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        colours = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().getColours();
        currentColour = colours[index];
        GetComponent<SpriteRenderer>().color = currentColour;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                changeColour();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null && hit.collider.transform == transform)
            {
                activateColour();
            }
        }
    }

    public void changeColour()
    {
        if (index < colours.Length - 1)
        {
            index++;
        }
        else
        {
            index = 0;
        }

        currentColour = colours[index];
        GetComponent<SpriteRenderer>().color = currentColour;
    }

    public void activateColour()
    {
        canal.setColour(currentColour);
    }
}
