using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GroupsUI : MonoBehaviour
{
    public Image[] flagTeam;
    public TextMeshProUGUI[] nameTeam, textPoints;
    private int[] teamsGroupA, teamsGroupB;

    void Start()
    {
        if (PlayerPrefs.GetInt("existGroups", 0) == 0)
        {
            SetupGroupStage();
        }
        
    }
    void Update()
    {
        GetUIGroupStage();
    }

    public void SetupGroupStage()
    {
        int[] numeros = Enumerable.Range(0, 10).ToArray(); // Crea un array con los números del 0 al 9
        System.Random random = new System.Random();
        numeros = numeros.OrderBy(x => random.Next()).ToArray(); // Mezcla los números de forma aleatoria

        teamsGroupA = numeros.Take(5).ToArray(); // Toma los primeros 5 números
        for (int i = 0; i < teamsGroupA.Length; i++)
        {
            PlayerPrefs.SetInt("GroupA" + i, teamsGroupA[i]);
        }
        teamsGroupB = numeros.Skip(5).ToArray(); // Toma los siguientes 5 números
        for (int i = 0; i < teamsGroupB.Length; i++)
        {
            PlayerPrefs.SetInt("GroupB" + i, teamsGroupB[i]);
        }

        PlayerPrefs.SetInt("existGroups", 1);
    }

    public void GetUIGroupStage()
    {
        GameObject groupA = GameObject.FindGameObjectWithTag("GroupA");
        for (int i = 0; i < 5; i++)
        {
            groupA.GetComponent<GroupsUI>().flagTeam[i].sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("GroupA" + i)];
            groupA.GetComponent<GroupsUI>().nameTeam[i].text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("GroupA" + i)];
            if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("GroupA" + i)] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
            {
                groupA.GetComponent<GroupsUI>().nameTeam[i].color = Color.green;
                groupA.GetComponent<GroupsUI>().textPoints[i].color = Color.green;
            }
        }
        GameObject groupB = GameObject.FindGameObjectWithTag("GroupB");
        for (int i = 0; i < 5; i++)
        {
            groupB.GetComponent<GroupsUI>().flagTeam[i].sprite = TeamUI.instance.flagTeam[PlayerPrefs.GetInt("GroupB" + i)];
            groupB.GetComponent<GroupsUI>().nameTeam[i].text = TeamUI.instance.nameTeam[PlayerPrefs.GetInt("GroupB" + i)];
            if (TeamUI.instance.nameTeam[PlayerPrefs.GetInt("GroupB" + i)] == TeamUI.instance.nameTeam[PlayerPrefs.GetInt("valueHistory", 1) - 1])
            {
                groupB.GetComponent<GroupsUI>().nameTeam[i].color = Color.green;
                groupB.GetComponent<GroupsUI>().textPoints[i].color = Color.green;
            }
        }

    }

    public void ButtonNextGroup()
    {
        SceneManager.LoadScene("HistoryGroupStage");
    }
}