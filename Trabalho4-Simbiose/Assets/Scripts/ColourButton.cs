using UnityEngine;
using TMPro;

public class ColourButton : MonoBehaviour
{
    public TextMeshProUGUI textoCD;

    private Color32 currentColour;
    private Color32[] colours;

    [SerializeField]
    private Canal canal;

    private int index = 0;

    public float timerChange, timerChangeMax = 5f;
    public int cooldown;

    [SerializeField]
    bool isAbleToChange = true;
    
    // Start is called before the first frame update
    private void Start()
    {
        timerChange = Time.time;

        colours = GameObject.FindGameObjectWithTag("Game").GetComponent<Game>().getColours();
        currentColour = colours[index];
        GetComponent<SpriteRenderer>().color = currentColour;
    }

    // Update is called once per frame
    private void Update()
    {
        if (isAbleToChange == false) textoCD.text = "" + (cooldown);
        else textoCD.text = "";

        cooldown = (int)(timerChangeMax - (Time.time - timerChange)) + 1;

        if (Time.time > timerChange + timerChangeMax && isAbleToChange == false)
        {
            timerChange = Time.time;

            isAbleToChange = true;
        }

#if UNITY_EDITOR
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

            if (hit.collider != null && hit.collider.transform == transform && isAbleToChange == true)
            {
                activateColour();
            }
        }
#endif

#if UNITY_ANDROID
        foreach (Touch toque in Input.touches)
        {
            if (GetComponent<BoxCollider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(toque.position)))
            {
                if (toque.phase == TouchPhase.Began)
                {
                    changeColour();
                }
                else if (toque.phase == TouchPhase.Moved && isAbleToChange == true)
                {
                    activateColour();
                }
            }
        }

#endif
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

        GameEventManager.triggerCanalColorChange();

        isAbleToChange = false;

        timerChange = Time.time;
    }
}