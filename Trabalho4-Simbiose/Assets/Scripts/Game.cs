using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI textScore;

    [SerializeField]
    private TextMeshProUGUI textScoreF;

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

    [SerializeField]
    public bool isGameOver = false;

    [SerializeField]
    public int score;

    public static int highScore = 0;

    [SerializeField]
    private GameObject telaGameOver;

    public Sfx sfxRef;

    // Start is called before the first frame update
    private void Start()
    {
        GameEventManager.clearAllEvents();
        highScore = PlayerPrefs.GetInt("highscore", highScore);

        for (int i = 0; i < canals.Length; i++)
        {
            canals[i].setColour(colours[i]);
        }

        score = 0;
        
        isGameOver = false;
        telaGameOver.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (score >= PlayerPrefs.GetInt("highscore", highScore))
            PlayerPrefs.SetInt("highscore", score);

        if (!isGameOver)
        {
            if (Time.time > timer + timerMax)
            {
                timer = Time.time;

                spawn();
            }
            textScore.text = "Score: " + score;
            textScoreF.text = "Score: " + score;
        }
        else
        {
            telaGameOver.SetActive(true);
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
        if (!b || b == null) return;
        if (!b.gameObject || b.gameObject == null) return;

        score++;

        b.RegisterColorChange(false);
        if (foodQueue.Count < maxQueue)
        {
            print("invisivel");

            b.gameObject.SetActive(false);

            foodQueue.Enqueue(b);
        }
        else
        {
            print("destroy da pool");

            Destroy(b.gameObject);
        }

        sfxRef.Blip.Play();
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

    public void Reload()
    {
        SceneManager.LoadScene(1);
    }

}