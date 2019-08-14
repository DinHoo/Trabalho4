using UnityEngine;

public class Canal : MonoBehaviour
{
    private Color32 colour;

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public Color32 GetColour()
    {
        return colour;
    }

    public void setColour(Color32 c)
    {
        colour = c;
        GetComponent<SpriteRenderer>().color = c;
    }
}