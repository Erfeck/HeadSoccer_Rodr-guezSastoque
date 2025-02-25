using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HistoryController : MonoBehaviour
{
    public TextMeshProUGUI nameTeam, textIdTeam;
    public Image flagTeam, starTeam;

    void Start()
    {
        
    }
    void Update()
    {
        flagTeam.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];
        nameTeam.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];
        starTeam.sprite = TeamUI.instance.starTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1];
    }
    public void ButtonBack()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonNext()
    {
        SceneManager.LoadScene("HistoryTournament");
    }

    public void ButtonLeftPlayer()
    {
        if (PlayerPrefs.GetInt("valueHistory", 1) <= 1)
        {
            PlayerPrefs.SetInt("valueHistory", 10);
        }
        else
        {
            int valueHistory = PlayerPrefs.GetInt("valueHistory", 1);
            valueHistory--;
            PlayerPrefs.SetInt("valueHistory", valueHistory);
        }
    }
    public void ButtonRightPlayer()
    {
        if (PlayerPrefs.GetInt("valueHistory", 1) >= 10)
        {
            PlayerPrefs.SetInt("valueHistory", 1);
        }
        else
        {
            int valueHistory = PlayerPrefs.GetInt("valueHistory", 1);
            valueHistory++;
            PlayerPrefs.SetInt("valueHistory", valueHistory);
        }
    }
}