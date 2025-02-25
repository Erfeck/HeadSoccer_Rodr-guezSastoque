using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    private GameObject _player;
    private GameObject _cpu;
    public GameObject goals;

    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _cpu = GameObject.FindGameObjectWithTag("AI");
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "checkTop")
        {
            Debug.Log("Larguero");
            audioManager.PlaySound(audioManager.goalpost);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            _player.GetComponent<Player>().canShoot = true;
        }
        if (collision.gameObject.tag == "canShootAI")
        {
            _cpu.GetComponent<CPU>().canShootAI = true;
        }
        if (collision.gameObject.tag == "canHeadAI")
        {
            _cpu.GetComponent<CPU>().canHeadAI = true;
        }
        if (collision.gameObject.tag == "GoalsRight")
        {
            audioManager.PlaySound(audioManager.goal);
            if (!GameController.instance.isScore && !GameController.instance.matchEnd)
            {
                if (GameController.instance.isDoubleGoalActive)
                {
                    Instantiate(goals, new Vector3(0, -2, 0), Quaternion.identity);
                    GameController.numberGoalsLeft = GameController.numberGoalsLeft + 2;
                    GameController.instance.isScore = true;
                    GameController.instance.ContinueMatch(false);
                } else
                {
                    Instantiate(goals, new Vector3(0, -2, 0), Quaternion.identity);
                    GameController.numberGoalsLeft++;
                    GameController.instance.isScore = true;
                    GameController.instance.ContinueMatch(false);
                }
                
            }
        }
        if (collision.gameObject.tag == "GoalsLeft")
        {
            audioManager.PlaySound(audioManager.goal);
            if (!GameController.instance.isScore && !GameController.instance.matchEnd)
            {
                Instantiate(goals, new Vector3(7, -2, 0), Quaternion.identity);
                GameController.numberGoalsRight++;
                GameController.instance.isScore = true;
                GameController.instance.ContinueMatch(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            _player.GetComponent<Player>().canShoot = false;
        }
        if (collision.gameObject.tag == "canShootAI")
        {
            _cpu.GetComponent<CPU>().canShootAI = false;
        }
        if (collision.gameObject.tag == "canHeadAI")
        {
            _cpu.GetComponent<CPU>().canHeadAI = false;
        }
    }
}
