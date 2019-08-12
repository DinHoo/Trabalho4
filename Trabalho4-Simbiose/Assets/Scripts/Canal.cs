using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canal : MonoBehaviour
{
    Color32 colour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
