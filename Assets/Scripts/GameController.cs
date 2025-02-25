using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static Unity.IO.LowLevel.Unsafe.AsyncReadManagerMetrics;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public static int numberGoalsRight, numberGoalsLeft;
    public TextMeshProUGUI textGoalsLeft, textGoalsRight, textTimeMatch;
    public bool isScore, matchEnd;
    public float timeMatch;
    private GameObject _ball, _cpu, _player;
    public GameObject panelPause;

    public Image flagLeft, flagRight;
    public TextMeshProUGUI nameLeft, nameRight;

    public SpriteRenderer headPlayer, bodyPlayer;
    public SpriteRenderer headAI, bodyAI;


    private GameObject _stadium;
    private int stadiumLevel;

    public GameObject buttonX2, buttonGiant;
    public bool isReadyDoubleGoal, isReadyGiant;
    public bool isDoubleGoalActive, isGiantActive;
    public float powerUpTimer;
    public GameObject aniX2, aniGiant;
    public Transform transformPlayer;

    AudioManager audioManager;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (AudioManager.isMusicSounds)
        {
            audioManager.StopTheme();
            AudioManager.isMusicSounds = false;
        }

        PlayerPrefs.SetString("wasSkillUsed", "No");
        isDoubleGoalActive = false;
        isGiantActive = false;
        if (PlayerPrefs.GetString("typeSkill").Equals("Giant"))
        {
            buttonX2.SetActive(false);
        }
        else if (PlayerPrefs.GetString("typeSkill").Equals("X2"))
        {
            buttonGiant.SetActive(false);
        }

        _stadium = GameObject.FindGameObjectWithTag("Stadium");
        //_stadium.GetComponent<SpriteRenderer>().sprite =
        System.Random random = new System.Random();
        stadiumLevel = random.Next(0,2);
        _stadium.GetComponent<SpriteRenderer>().sprite = TeamUI.instance.stadiums[stadiumLevel];


        numberGoalsLeft = 0;
        numberGoalsRight = 0;
        timeMatch = 90;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _cpu = GameObject.FindGameObjectWithTag("AI");
        _player = GameObject.FindGameObjectWithTag("Player");

        if (PlayerPrefs.GetString("TypeGame").Equals("Match"))
        {
            flagLeft.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
            nameLeft.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

            flagRight.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
            nameRight.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];

            headPlayer.sprite = TeamUI.instance.head[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
            bodyPlayer.sprite = TeamUI.instance.body[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

            headAI.sprite = TeamUI.instance.head[PlayerPrefs.GetInt("valueAI", 1) - 1];
            bodyAI.sprite = TeamUI.instance.body[PlayerPrefs.GetInt("valueAI", 1) - 1];
        }
        else if (PlayerPrefs.GetString("TypeGame").Equals("History"))
        {
            flagLeft.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];
            nameLeft.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];

            flagRight.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueAIHistory", 1)];
            nameRight.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueAIHistory", 1)];

            headPlayer.sprite = TeamUI.instance.head[PlayerPrefs.GetInt("valueHistory", 1) - 1];
            bodyPlayer.sprite = TeamUI.instance.body[PlayerPrefs.GetInt("valueHistory", 1) - 1];

            headAI.sprite = TeamUI.instance.head[PlayerPrefs.GetInt("valueAIHistory", 1)];
            bodyAI.sprite = TeamUI.instance.body[PlayerPrefs.GetInt("valueAIHistory", 1)];
        }

        if (nameLeft.text.Equals("Portugal") && nameRight.text.Equals("Portugal"))
        {
            audioManager.PlayEasterEggSomosGuapos();
        }

        StartCoroutine(BeginMatch());

        panelPause.SetActive(false);
    }
    void Update()
    {
        textGoalsLeft.text = numberGoalsLeft.ToString();
        textGoalsRight.text = numberGoalsRight.ToString();
        textTimeMatch.text = timeMatch.ToString();

        if (stadiumLevel == 1)
        {
            if (timeMatch >= 66 && timeMatch < 76)
            {
                _cpu.GetComponent<CPU>().isFrozenAI = true;
            }
            else
            {
                _cpu.GetComponent<CPU>().isFrozenAI = false;
            }
            if (timeMatch >= 43 && timeMatch < 53)
            {
                _player.GetComponent<Player>().isFrozenPlayer = true;
            }
            else
            {
                _player.GetComponent<Player>().isFrozenPlayer = false;
            }
        }

        if (timeMatch == 69)
        {
            if (PlayerPrefs.GetString("typeSkill").Equals("Giant"))
            {
                isReadyGiant = true;
            }
            else if (PlayerPrefs.GetString("typeSkill").Equals("X2"))
            {
                isReadyDoubleGoal = true;
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (isReadyDoubleGoal)
            {
                ButtonDoubleGoal();
            }
            else if (isReadyGiant)
            {
                ButtonGiant();
            }
        }

        // Verificar combinación de teclas G + H + J
        if (Input.GetKey(KeyCode.G) && Input.GetKey(KeyCode.H) && Input.GetKey(KeyCode.J))
        {
            WinByShortcut();
        }
    }

    void TogglePause()
    {
        if (panelPause.activeSelf)
        {
            ButtonResume(); // Si ya está en pausa, reanudar el juego
        }
        else
        {
            ButtonPause(); // Si no está en pausa, pausar el juego
        }
    }

    IEnumerator BeginMatch()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (timeMatch > 0)
            {
                timeMatch--;
            }
            else
            {
                matchEnd = true;
                StartCoroutine(WaitEndGame());
                break;
            }
        }


    }

    public void ContinueMatch(bool winPlayer)
    {
        StartCoroutine(WaitContinueMatch(winPlayer));
    }

    IEnumerator WaitContinueMatch(bool winPlayer)
    {
        yield return new WaitForSeconds(0.2f);
        isScore = false;
        if (!matchEnd)
        {
            _ball.transform.position = new Vector3(0, 2, 0); // // // 
            _cpu.transform.position = new Vector3(1.8f, 0, 0);
            _player.transform.position = new Vector3(-1.8f, 0, 0);
            if (winPlayer)
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(100, 200));
            }
            else
            {
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(-100, 200));
            }
        }
    }

    public void ButtonPause()
    {
        panelPause.SetActive(true);
        Time.timeScale = 0;
    }
    public void ButtonLose()
    {
        numberGoalsLeft = 0;
        numberGoalsRight = 3;
        timeMatch = 0;
        panelPause.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(WaitEndGame());
    }
    public void ButtonResume()
    {
        panelPause.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator WaitEndGame()
    {
        audioManager.PlaySound(audioManager.refereeWhistle);
        yield return new WaitForSeconds(0.2f);
        SceneManager.LoadScene("EndGame");
    }

    void WinByShortcut()
    {
        numberGoalsLeft = 3;
        numberGoalsRight = 0;
        timeMatch = 0;
        matchEnd = true;
        StartCoroutine(WaitEndGame());
    }

    public void ButtonDoubleGoal()
    {
        if (isReadyDoubleGoal && !isDoubleGoalActive)
        {
            audioManager.PlaySound(audioManager.poweUpTheme);
            isDoubleGoalActive = true;
            Instantiate(aniX2, new Vector3(-1.5f, 3.37f, 0), Quaternion.identity);
            StartCoroutine(DoubleGoalEffect());
        }
    }
    public void ButtonGiant()
    {
        if (isReadyGiant && !isGiantActive)
        {
            audioManager.PlaySound(audioManager.poweUpTheme);
            isGiantActive = true;
            transformPlayer.position = new Vector3(0.1f, 4, 0);
            transformPlayer.localScale = new Vector3(2, 2, 1);
            Instantiate(aniGiant, new Vector3(-1.5f, 3.37f, 0), Quaternion.identity);
            StartCoroutine(GiantEffect());
        }
    }

    IEnumerator DoubleGoalEffect()
    {
        float timer = powerUpTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }
        isDoubleGoalActive = false;
        isReadyDoubleGoal = false;
        PlayerPrefs.SetString("wasSkillUsed", "Yes");
    }
    IEnumerator GiantEffect()
    {
        float timer = powerUpTimer;
        while (timer > 0)
        {
            yield return new WaitForSeconds(1f);
            timer--;
        }
        isGiantActive = false;
        isReadyGiant = false;
        PlayerPrefs.SetString("wasSkillUsed", "Yes");
        transformPlayer.position = new Vector3(0.1f, 0.25f, 0);
        transformPlayer.localScale = new Vector3(1, 1, 1);
    }
}