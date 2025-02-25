using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tournament : MonoBehaviour
{
    public GameObject buttonNextMatch, buttonNewTournament;
    public Image[] quarterFlagTeam;
    public TextMeshProUGUI[] quarterNameTeam;
    public Image[] semifinalFlagTeam;
    public TextMeshProUGUI[] semifinalNameTeam;
    public Image[] finalFlagTeam;
    public TextMeshProUGUI[] finalNameTeam;
    public Image championFlagTeam;
    public TextMeshProUGUI championNameTeam;
    //public int winnerQuarter, winnerSemiFinal, winnerFinal;

    void Start()
    {
        if (PlayerPrefs.GetInt("existsTournament", 0) == 0)
        {
            CreateTournament();
        }
    }
    void Update()
    {
        GetTeamsTournament();
        if (PlayerPrefs.GetString("PlayerLose").Equals("No"))
        {
            buttonNextMatch.SetActive(true);
            buttonNewTournament.SetActive(false);
        }
        else if (PlayerPrefs.GetString("PlayerLose").Equals("Yes"))
        {
            buttonNextMatch.SetActive(false);
            buttonNewTournament.SetActive(true);
        }
    }

    public void ButtonBackMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void ButtonNextMatch()
    {
        PlayerPrefs.SetString("TypeGame", "History");

        System.Random random = new System.Random();
        int numberTypePowerUp = random.Next(0, 2);
        PlayerPrefs.SetString("typeSkill", TeamUI.instance.typePowers[numberTypePowerUp]);

        if (PlayerPrefs.GetInt("existsSemifinal") == 0)
        {
            PlayQuarterFinal();
            obtenerIDTeamAI(quarterNameTeam[1]);
            SceneManager.LoadScene("Game");
            return;
        }
        if (PlayerPrefs.GetInt("existsFinal") == 0)
        {
            PlaySemiFinal();
            obtenerIDTeamAI(semifinalNameTeam[1]);
            SceneManager.LoadScene("Game");
            return;
        }
        if (PlayerPrefs.GetInt("existsChampion") == 0)
        {
            PlayFinal();
            obtenerIDTeamAI(finalNameTeam[1]);
            SceneManager.LoadScene("Game");
            return;
        } 
    }
    public void ButtonNewTournament()
    {
        PlayerPrefs.SetInt("existsTournament", 0);
        PlayerPrefs.SetString("PlayerLose", "No");
        SceneManager.LoadScene("HistorySelectTeam");
    }

    public void CreateTournament()
    {
        int[] numeros = Enumerable.Range(0, 10).ToArray();
        System.Random random = new System.Random();

        int userTeam = PlayerPrefs.GetInt("valueHistory", -1) - 1;
        if (userTeam == -1) return;

        // Elimina el equipo del usuario de la lista de equipos disponibles para evitar duplicados
        numeros = numeros.Where(x => x != userTeam).ToArray();
        // Mezcla aleatoriamente los números restantes
        numeros = numeros.OrderBy(x => random.Next()).ToArray();

        int[] quarterTeams = new int[8];
        quarterTeams[0] = userTeam;

        for (int i = 1; i < quarterTeams.Length; i++)
        {
            quarterTeams[i] = numeros[i]; // Asigna los equipos restantes
        }
        for (int i = 0; i < quarterTeams.Length; i++)
        {
            PlayerPrefs.SetInt("QuarterTeam" + i, quarterTeams[i]);
        }

        // Marca que el torneo ha sido creado
        PlayerPrefs.SetInt("existsTournament", 1);
        PlayerPrefs.SetInt("existsSemifinal", 0);
        PlayerPrefs.SetInt("existsFinal", 0);
        PlayerPrefs.SetInt("existsChampion", 0);
        PlayerPrefs.SetInt("hasToPlayQuarterPlayer", 0);
        PlayerPrefs.SetInt("hasToPlaySemiFinalPlayer", 0);
        PlayerPrefs.SetInt("hasToPlayFinalPlayer", 0);
    }

    public void GetTeamsTournament()
    {

        GameObject teamsTournament = GameObject.FindGameObjectWithTag("Tournament");
        //QuarterFinal
        for (int i = 0; i < 8; i++)
        {
            quarterFlagTeam[i].sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("QuarterTeam" + i)];
            quarterNameTeam[i].text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("QuarterTeam" + i)];
            if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("QuarterTeam" + i)] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
            {
                quarterNameTeam[i].color = Color.green;
            }
        }
        if (PlayerPrefs.GetInt("existsSemifinal") == 1)
        {
            //SemiFinal
            for (int i = 0; i < 4; i++)
            {
                semifinalFlagTeam[i].sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("SemiFinalTeam" + i)];
                semifinalNameTeam[i].text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("SemiFinalTeam" + i)];
                if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("SemiFinalTeam" + i)] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
                {
                    semifinalNameTeam[i].color = Color.green;
                }
            }
        }
        if (PlayerPrefs.GetInt("existsFinal") == 1)
        {
            //Final
            for (int i = 0; i < 2; i++)
            {
                finalFlagTeam[i].sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("FinalTeam" + i)];
                finalNameTeam[i].text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("FinalTeam" + i)];
                if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("FinalTeam" + i)] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
                {
                    finalNameTeam[i].color = Color.green;
                }
            }
        }
        if (PlayerPrefs.GetInt("existsChampion") == 1)
        {
            //Champion
            championFlagTeam.sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("championTeam")];
            championNameTeam.text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("championTeam")];
            if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("championTeam")] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
            {
                championNameTeam.color = Color.green;
            }
        }
    }

    public void PlayQuarterFinal()
    {
        System.Random random = new System.Random();
        
        int winnerQuarter2 = random.Next(2,4);
        semifinalFlagTeam[1].sprite = quarterFlagTeam[winnerQuarter2].sprite;
        semifinalNameTeam[1].text = quarterNameTeam[winnerQuarter2].text;
        PlayerPrefs.SetInt("SemiFinalTeam" + 1, PlayerPrefs.GetInt("QuarterTeam" + winnerQuarter2));

        int winnerQuarter3 = random.Next(4, 6);
        semifinalFlagTeam[2].sprite = quarterFlagTeam[winnerQuarter3].sprite;
        semifinalNameTeam[2].text = quarterNameTeam[winnerQuarter3].text;
        PlayerPrefs.SetInt("SemiFinalTeam" + 2, PlayerPrefs.GetInt("QuarterTeam" + winnerQuarter3));

        int winnerQuarter4 = random.Next(6, 8);
        semifinalFlagTeam[3].sprite = quarterFlagTeam[winnerQuarter4].sprite;
        semifinalNameTeam[3].text = quarterNameTeam[winnerQuarter4].text;
        PlayerPrefs.SetInt("SemiFinalTeam" + 3, PlayerPrefs.GetInt("QuarterTeam" + winnerQuarter4));

        PlayerPrefs.SetInt("existsSemifinal", 1);
        PlayerPrefs.SetInt("hasToPlayQuarterPlayer", 0);

    }
    public void PlaySemiFinal()
    {
        System.Random random = new System.Random();
        int winnerSemi2 = random.Next(2, 4);

        finalFlagTeam[1].sprite = semifinalFlagTeam[winnerSemi2].sprite;
        
        finalNameTeam[1].text = semifinalNameTeam[winnerSemi2].text;
        Debug.Log("Team: " + semifinalNameTeam[winnerSemi2].text);
        PlayerPrefs.SetInt("FinalTeam" + 1, PlayerPrefs.GetInt("SemiFinalTeam" + winnerSemi2));

        PlayerPrefs.SetInt("existsFinal", 1);
        PlayerPrefs.SetInt("hasToPlaySemiFinalPlayer", 0);

    }
    public void PlayFinal()
    {
        //Equipo que gane, sacar numero

        //championFlagTeam.sprite = semifinalFlagTeam[0].sprite;
        //championNameTeam.text = semifinalNameTeam[0].text;
        //championNameTeam.color = Color.green;
        PlayerPrefs.SetInt("existsChampion", 1);
        PlayerPrefs.SetInt("hasToPlayFinalPlayer", 0);
        PlayerPrefs.SetString("PlayerLose", "Yes");

    }

    public void obtenerIDTeamAI(TextMeshProUGUI nameTeam)
    {
        Debug.Log(nameTeam.text);
        for (int i = 0; i < 10; i++)
        {
            if (nameTeam.text.Equals(TeamUI.instance.nameTeam[i]))
            {
                PlayerPrefs.SetInt("valueAIHistory", i);
            }
        }
    }
}
