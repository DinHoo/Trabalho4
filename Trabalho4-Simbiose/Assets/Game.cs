using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    [SerializeField]
    Color32[] colours;
    [SerializeField]
    Canal[] canals;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < canals.Length; i++)
        {
            canals[i].setColour(colours[i]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color32[] getColours()
    {
        return colours;
    }
}
