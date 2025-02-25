using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netHeadPlayer : MonoBehaviour
{
    private GameObject _ball, _player;

    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {
            if (_player.GetComponent<Player>().canHead)
            {
                _ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(300, 400));
            }   
        }
    }
}
