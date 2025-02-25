using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject panelTransition;
    public bool startingGame;

    //private AudioManager audioManager;

    void Start()
    {
        //audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        if (startingGame)
        {
            StartCoroutine(CargarPantalla());
            startingGame = false;
        }
        if (!AudioManager.isMusicSounds)
        {
            AudioManager.instance.PlayTheme();
            AudioManager.isMusicSounds = true;
        }
        
    }
    void Update()
    {
    }

    IEnumerator CargarPantalla()
    {
        yield return new WaitForSeconds(2f);
        panelTransition.SetActive(false);
        startingGame = true;
    }

    public void ButtonExhibition()
    {
        SceneManager.LoadScene("Exhibition");
    }
    public void ButtonHistory()
    {
        if (PlayerPrefs.GetInt("existsTournament", 0) == 0)
        {
            SceneManager.LoadScene("HistorySelectTeam");
        }
        else
        {
            SceneManager.LoadScene("HistoryTournament");
        }
        
    }
    public void ButtonOptions()
    {
        SceneManager.LoadScene("Options");
    }
}