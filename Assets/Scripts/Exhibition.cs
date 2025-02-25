using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Exhibition : MonoBehaviour
{
    public Image flagPlayer;
    public Image flagAI;
    public TextMeshProUGUI namePlayer;
    public TextMeshProUGUI nameAI;
    public Image starPlayer;  
    public Image starAI;

    void Start()
    {
        //valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
        //valueAI = PlayerPrefs.GetInt("valueAI", 1);
    }
    void Update()
    {
        flagPlayer.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        namePlayer.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];
        starPlayer.sprite = TeamUI.instance.starTeam[PlayerPrefs.GetInt("valuePlayer", 1) - 1];

        flagAI.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        nameAI.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
        starAI.sprite = TeamUI.instance.starTeam[PlayerPrefs.GetInt("valueAI", 1) - 1];
    }
    public void ButtonBack()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonNext()
    {
        PlayerPrefs.SetString("TypeGame", "Match");
        System.Random random = new System.Random();
        int numberTypePowerUp = random.Next(0, 2);
        PlayerPrefs.SetString("typeSkill", TeamUI.instance.typePowers[numberTypePowerUp]);
        SceneManager.LoadScene("Game");
    }

    public void ButtonLeftPlayer()
    {
        if (PlayerPrefs.GetInt("valuePlayer", 1) <= 1)
        {
            PlayerPrefs.SetInt("valuePlayer", 10);
        }
        else
        {
            int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
            valuePlayer--;
            PlayerPrefs.SetInt("valuePlayer", valuePlayer);
        }
    }
    public void ButtonRightPlayer()
    {
        if (PlayerPrefs.GetInt("valuePlayer", 1) >= 10)
        {
            PlayerPrefs.SetInt("valuePlayer", 1);
        }
        else
        {
            int valuePlayer = PlayerPrefs.GetInt("valuePlayer", 1);
            valuePlayer++;
            PlayerPrefs.SetInt("valuePlayer", valuePlayer);
        }
    }
    public void ButtonLeftAI()
    {
        if (PlayerPrefs.GetInt("valueAI", 1) <= 1)
        {
            PlayerPrefs.SetInt("valueAI", 10);
        }
        else
        {
            int valueAI = PlayerPrefs.GetInt("valueAI", 1);
            valueAI--;
            PlayerPrefs.SetInt("valueAI", valueAI);
        }
    }
    public void ButtonRightAI()
    {
        if (PlayerPrefs.GetInt("valueAI", 1) >= 10)
        {
            PlayerPrefs.SetInt("valueAI", 1);
        }
        else
        {
            int valueAI = PlayerPrefs.GetInt("valueAI", 1);
            valueAI++;
            PlayerPrefs.SetInt("valueAI", valueAI);
        }
    }
}
