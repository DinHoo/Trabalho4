using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    //Timer
    [SerializeField]
    private float timerMax;

    private float timer = 0;

    //Spawns
    [SerializeField]
    private Transform spawnPoints;

    [SerializeField]
    private Transform[] spawnsPointsLeft;

    [SerializeField]
    private Transform[] spawnsPointsRight;

    //Comida
    public GameObject[] food;

    [SerializeField]
    private Queue<Food> foodQueue = new Queue<Food>();

    public int maxQueue;

    //Voltar pro Menu
    [SerializeField]
    private GameObject goMenuPopUp;

    //Cores
    [SerializeField]
    private Color32[] colours;

    //Canais
    [SerializeField]
    private Canal[] canals;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < canals.Length; i++)
        {
            canals[i].setColour(colours[i]);
        }
        spawn();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time > timer + timerMax)
        {
            timer = Time.time;

            spawn();
        }
    }

    private float getSpawnPositionX()
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

    private void spawn()
    {
        print("Spawnando");
        if (foodQueue.Count > 0)
        {
            respawn();
        }
        else
        {
            Food b = Instantiate(food[Random.Range(0, food.Length)], new Vector2(getSpawnPositionX(), spawnPoints.position.y), Quaternion.identity).GetComponent<Food>();
        }
    }

    private void respawn()
    {
        if (foodQueue.Count <= 0)
        {
            return;
        }

        Food b = foodQueue.Dequeue();

        b.transform.position = new Vector2(getSpawnPositionX(), spawnPoints.position.y);

        b.gameObject.SetActive(true);

        b.randomizeColor();
    }

    public void addToPool(Food b)
    {
        if (foodQueue.Count < maxQueue)
        {
            print("invisivel");

            b.gameObject.SetActive(false);

            GameEventManager.canalColorChange -= b.checkToDestroy;

            foodQueue.Enqueue(b);
        }
        else
        {
            print("destroy da pool");
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
        SceneManager.LoadScene(0);
    }
}