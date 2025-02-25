using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class netHeadCPU : MonoBehaviour
{
    private GameObject _ball;

    void Start()
    {
        _ball = GameObject.FindGameObjectWithTag("Ball");
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ball")
        {   
            //_ball.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(200, 4));
        }
    }
}
