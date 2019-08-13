using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //Timer
    [SerializeField]
    float timerMax;
    float timer = 0;

    //Spawns
    [SerializeField]
    Transform spawnPoints;
    [SerializeField]
    Transform[] spawnsPointsLeft;
    [SerializeField]
    Transform[] spawnsPointsRight;

    //Bolas
    public Food ball;

    [SerializeField]
    Queue<Food> balls = new Queue<Food>();

    public int maxQueue;

    //Voltar pro Menu
    [SerializeField]
    private GameObject goMenuPopUp;

    //Cores
    [SerializeField]
    Color32[] colours;

    //Canais
    [SerializeField]
    Canal[] canals;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < canals.Length; i++)
        {
            canals[i].setColour(colours[i]);
        }
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer + timerMax && balls.Count < maxQueue)
        {
            timer = Time.time;

            spawn();
        }
    }

    float getSpawnPositionX()
    {
        float spawnHere;
        if (Random.value > 0.5)
        {
            spawnHere = Random.Range(spawnsPointsLeft[0].transform.position.x, spawnsPointsLeft[1].transform.position.x);
        }
        else
        {
            spawnHere = Random.Range(spawnsPointsRight[0].transform.position.x, spawnsPointsRight[1].transform.position.x);
        }

        return spawnHere;
    }

    void spawn()
    {
        if (balls.Count > 0)
        {
            respawn();
        }
        else
        {
            Food b = Instantiate(ball, new Vector2(getSpawnPositionX(), spawnPoints.position.y), Quaternion.identity).GetComponent<Food>();
        }
    }

    void respawn()
    {
        if (balls.Count <= 0)
            return;

        Food b = balls.Dequeue();

        b.transform.position = new Vector2(getSpawnPositionX(), spawnPoints.position.y);

        b.gameObject.SetActive(true);

        b.randomizeColor();
    }

    public void addToPool(Food b)
    {
        if (balls.Count < maxQueue)
        {
            b.gameObject.SetActive(false);

            GameEventManager.canalColorChange -= b.checkToDestroy;

            balls.Enqueue(b);
        }
        else
        {
            Destroy(b.gameObject);
        }
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
