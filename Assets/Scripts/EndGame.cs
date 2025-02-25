using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image flagLeft, flagRight;
    public TextMeshProUGUI nameLeft, nameRight, result, goalsLeft, goalsRight;
    public GameObject panelMatch, panelHistory;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (PlayerPrefs.GetString("TypeGame").Equals("Match"))
        {
            flagLeft.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
            nameLeft.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

            flagRight.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
            nameRight.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        }
        else if (PlayerPrefs.GetString("TypeGame").Equals("History"))
        {
            flagLeft.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];
            nameLeft.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];

            flagRight.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueAIHistory", 1)];
            nameRight.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueAIHistory", 1)];
        }
        

        goalsLeft.text = GameController.numberGoalsLeft.ToString();
        goalsRight.text = GameController.numberGoalsRight.ToString();


        if (GameController.numberGoalsLeft > GameController.numberGoalsRight && nameLeft.text.Equals("Portugal"))
        {
            audioManager.PlaySound(audioManager.siuRonaldo);
        }

        if (GameController.numberGoalsLeft < GameController.numberGoalsRight)
        {
            result.text = "Has perdido";
            if (PlayerPrefs.GetString("TypeGame").Equals("History"))
            {
                PlayerPrefs.SetString("PlayerLose", "Yes");
            }
        }
        else if (GameController.numberGoalsLeft == GameController.numberGoalsRight)
        {
            result.text = "Empate";
        }
        else
        {
            result.text = "Has ganado crack";
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetString("TypeGame").Equals("Match"))
        {
            panelMatch.SetActive(true);
            panelHistory.SetActive(false);
        }
        else if (PlayerPrefs.GetString("TypeGame").Equals("History"))
        {
            panelMatch.SetActive(false);
            panelHistory.SetActive(true);
        }
    }

    public void ButtonHome()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonMatch()
    {
        SceneManager.LoadScene("Exhibition");
    }
    public void ButtonRestart()
    {
        SceneManager.LoadScene("Game");
    }
    public void ButtonTournament()
    {
        if (PlayerPrefs.GetInt("hasToPlayQuarterPlayer") == 0)
        {
            if (result.text.Equals("Has ganado crack"))
            {
                PlayerPrefs.SetInt("SemiFinalTeam" + 0, PlayerPrefs.GetInt("QuarterTeam" + 0));
            } else
            {
                PlayerPrefs.SetInt("SemiFinalTeam" + 0, PlayerPrefs.GetInt("QuarterTeam" + 1));
            }
            PlayerPrefs.SetInt("hasToPlayQuarterPlayer", 1);
        }
        if (PlayerPrefs.GetInt("hasToPlaySemiFinalPlayer") == 0)
        {
            if (result.text.Equals("Has ganado crack"))
            {
                PlayerPrefs.SetInt("FinalTeam" + 0, PlayerPrefs.GetInt("SemiFinalTeam" + 0));
            }
            else
            {
                PlayerPrefs.SetInt("FinalTeam" + 0, PlayerPrefs.GetInt("SemiFinalTeam" + 1));
            }
            PlayerPrefs.SetInt("hasToPlaySemiFinalPlayer", 1);
        }
        if (PlayerPrefs.GetInt("hasToPlayFinalPlayer") == 0)
        {
            if (result.text.Equals("Has ganado crack"))
            {
                PlayerPrefs.SetInt("championTeam", PlayerPrefs.GetInt("FinalTeam" + 0));
            }
            else
            {
                PlayerPrefs.SetInt("championTeam", PlayerPrefs.GetInt("FinalTeam" + 1));
            }
            PlayerPrefs.SetInt("hasToPlayFinalPlayer", 1);
        }
        SceneManager.LoadScene("HistoryTournament");
    }
}