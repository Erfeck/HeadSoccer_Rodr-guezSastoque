using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Options : MonoBehaviour
{
    public GameObject panelOptions, panelVolumen, panelMusica, panelControles, panelHistorial;
    public TextMeshProUGUI textoVolumen, textoMusica;
    private bool isPanelSoundActive;
    private AudioManager audioManager;
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();

        ButtonVolverAtras();

        isPanelSoundActive = false;
    }
    void Update()
    {
        Debug.Log("Volumen : " + PlayerPrefs.GetString("VolumenTexto"));
        Debug.Log("Nusica: " + PlayerPrefs.GetString("MusicaTexto"));
        textoVolumen.text = PlayerPrefs.GetString("VolumenTexto", "10");
        textoMusica.text = PlayerPrefs.GetString("MusicaTexto", "10");
    }

    public void ButtonVolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ButtonVolverAtras()
    {
        panelOptions.SetActive(true);
        panelVolumen.SetActive(false);
        panelMusica.SetActive(false);
        panelControles.SetActive(false);
        panelHistorial.SetActive(false);
    }
    public void ButtonVolumen()
    {
        panelOptions.SetActive(false);
        panelVolumen.SetActive(true);
        isPanelSoundActive = true;
    }
    public void ButtonMusica()
    {
        panelOptions.SetActive(false);
        panelMusica.SetActive(true);
        isPanelSoundActive = false;
    }
    public void ButtonControles()
    {
        panelOptions.SetActive(false);
        panelControles.SetActive(true);
    }
    public void ButtonHistorial()
    {
        panelOptions.SetActive(false);
        panelHistorial.SetActive(true);
    }

    public void ButtonRestarSonido(TextMeshProUGUI textoNumero)
    {
        if (int.TryParse(textoNumero.text, out int numero))
        {
            if (numero > 0)
            {
                numero--;
                textoNumero.text = numero.ToString();
                if (isPanelSoundActive)
                {
                    PlayerPrefs.SetFloat("Volumen", ParsearVolumen(numero));
                    PlayerPrefs.SetString("VolumenTexto", numero.ToString());
                }
                else
                {
                    PlayerPrefs.SetFloat("Musica", ParsearVolumen(numero));
                    PlayerPrefs.SetString("MusicaTexto", numero.ToString());
                }
                audioManager.CambiarVolumen();
            }
            
        }
    }
    public void ButtonSumarSonido(TextMeshProUGUI textoNumero)
    {
        if (int.TryParse(textoNumero.text, out int numero))
        {
            if (numero < 10)
            {
                numero++;
                textoNumero.text = numero.ToString();
                if (isPanelSoundActive)
                {
                    PlayerPrefs.SetFloat("Volumen", ParsearVolumen(numero));
                    PlayerPrefs.SetString("VolumenTexto", numero.ToString());
                }
                else
                {
                    PlayerPrefs.SetFloat("Musica", ParsearVolumen(numero));
                    PlayerPrefs.SetString("MusicaTexto", numero.ToString());
                }
                audioManager.CambiarVolumen();
            }

        }
    }

    public float ParsearVolumen(int numero)
    {
        return (float) numero / 10f;
    }
}
