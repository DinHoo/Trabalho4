using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    private GameObject goMenuPopUp;

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

        int a = 3;
        int b = 2;
        a + b;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Color32[] getColours()
    {
        return colours;
    }

    public void goMenu()
    {
        goMenuPopUp.SetActive(true);
    }

    public void goMenuClose()
    {
        goMenuPopUp.SetActive(false);
    }

    public void loadMenuScene()
    {
        SceneManager.LoadScene("Menu Kelvin");
    }
}
