using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPU : MonoBehaviour
{
    public float rangeDefense;
    public float speed;
    public Transform defense, checkGround;
    private GameObject _ball;
    private Rigidbody2D rbCPU;
    public bool canShootAI, canHeadAI, grounded, isFrozenAI;
    public LayerMask ground_layer;
    public GameObject congeladoAI;
    private int typeAI;
    private int hashJump, hashMoveRight, hashMoveLeft;
    public Animator _aniAI;

    private float timer;
    private bool defensePhase;

    void Start()
    {
        hashJump = Animator.StringToHash("JumpAI");
        hashMoveRight = Animator.StringToHash("MoveRightAI");
        hashMoveLeft = Animator.StringToHash("MoveLeftAI");

        isFrozenAI = false;
        _ball = GameObject.FindGameObjectWithTag("Ball");
        rbCPU = GetComponent<Rigidbody2D>();

        congeladoAI.SetActive(false);
        System.Random random = new System.Random();
        typeAI = random.Next(0, 3);

        if (typeAI == 2)
        {
            defensePhase = true;
            timer = 45f;
        }
    }

    void Update()
    {
        //_aniAI.SetBool(hashMoveLeft, true);
        if (!isFrozenAI)
        {
            congeladoAI.SetActive(false);
            if (typeAI == 0)
            {
                MoveDefensa();
            }
            else if (typeAI == 1)
            {
                MoveAtaque();
            }
            else if (typeAI == 2)
            {
                HandleDefenseAttackSequence();
            }
            MoveDefensa();
            if (canShootAI)
            {
                Shoot();
            }
            if (canHeadAI && grounded)
            {
                Jump();
            }
            
        } else
        {
            BlockMoveIA();
        }
    }

    private void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground_layer);

        if (grounded) {
            _aniAI.SetBool(hashJump, false);
        }
    }

    public void BlockMoveIA()
    {
        rbCPU.velocity = Vector2.zero;
        congeladoAI.SetActive(true);
    }

    public void MoveDefensa()
    {
        _aniAI.SetBool(hashMoveLeft, true);
        if (Mathf.Abs(_ball.transform.position.x - transform.position.x) < rangeDefense)
        {
            if (_ball.transform.position.x > transform.position.x && _ball.transform.position.y < -0.5f)
            {
                rbCPU.velocity = new Vector2(-speed, rbCPU.velocity.y);
                
            }
            else if (_ball.transform.position.y >= -0.5f && transform.position.x <= defense.position.x)
            {
                rbCPU.velocity = new Vector2(0, rbCPU.velocity.y);
            }
            else
            {
                rbCPU.velocity = new Vector2(speed, rbCPU.velocity.y);
            }
        }
        else
        {
            if (Mathf.Abs(transform.position.x - defense.position.x) > 0.1f)
            {
                if (transform.position.x > defense.position.x)
                {
                    rbCPU.velocity = new Vector2(-speed, rbCPU.velocity.y);
                }
                else if (transform.position.x < defense.position.x)
                {
                    rbCPU.velocity = new Vector2(speed, rbCPU.velocity.y);
                }
            }
            else
            {
                rbCPU.velocity = new Vector2(0, rbCPU.velocity.y);
            }
        }
    }
    void MoveAtaque()
    {
        _aniAI.SetBool(hashMoveLeft, true);
        if (_ball == null) return; // Evita errores si el balón no está asignado

        float ballX = _ball.transform.position.x;
        float cpuX = transform.position.x;
        float targetX = 0f; // La CPU siempre quiere volver a x = 0 por defecto

        // Si el balón está a la derecha de la CPU, ir directamente hacia él
        if (ballX > cpuX)
        {
            targetX = ballX; // Ir directamente al balón
        }

        // Si la CPU ya está en su destino, no moverse
        if (Mathf.Abs(cpuX - targetX) < 0.1f) return;

        // Controlar el movimiento de forma más directa
        if (cpuX < targetX)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }
        else if (cpuX > targetX)
        {
            transform.position += Vector3.left * speed * Time.deltaTime;
        }

    }

    private void Shoot()
    {
        _aniAI.SetTrigger("ShootAI");
        canHeadAI = false; // Deshabilita el disparo temporalmente

        // Simula un disparo al balón siempre hacia la izquierda
        Vector2 shootDirection = new Vector2(-1, 0.5f); // Dispara hacia la izquierda con un poco de elevación
        _ball.GetComponent<Rigidbody2D>().AddForce(shootDirection * 10f); // Puedes ajustar la fuerza multiplicando el vector

        // Espera 1 segundo antes de permitir otro disparo
    }

    public void Jump()
    {
        _aniAI.SetBool(hashJump, true);
        rbCPU.velocity = new Vector2(-speed, 11);
    }

    private void HandleDefenseAttackSequence()
    {
        timer -= Time.deltaTime;

        if (defensePhase)
        {
            MoveDefensa();
            if (timer <= 0f)
            {
                defensePhase = false;
                timer = 45f;
            }
        }
        else
        {
            MoveAtaque();
            if (timer <= 0f)
            {
                defensePhase = true;
                timer = 45f;
            }
        }
    }
}
