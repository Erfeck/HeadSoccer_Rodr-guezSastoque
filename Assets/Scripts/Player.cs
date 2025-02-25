using UnityEngine;

public class Player : MonoBehaviour
{

    public float horialAxis, speed;
    private Rigidbody2D rb;
    public bool canShoot, grounded, canHead, isFrozenPlayer;
    public GameObject _ball;
    public Transform checkGround;
    public LayerMask ground_layer;
    private int hashJump, hashMoveRight, hashMoveLeft;
    public Animator _aniPlayer;
    public GameObject congeladoPlayer;

    // Start is called before the first frame update
    void Start()
    {
        isFrozenPlayer = false;
        congeladoPlayer.SetActive(false);
        rb = GetComponent<Rigidbody2D>();
        _ball = GameObject.FindGameObjectWithTag("Ball");
        hashJump = Animator.StringToHash("Jump");
        hashMoveRight = Animator.StringToHash("MoveRight");
        hashMoveLeft = Animator.StringToHash("MoveLeft");
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozenPlayer)
        {
            horialAxis = 0;
            rb.velocity = Vector2.zero; // Detiene la IA
            congeladoPlayer.SetActive(true);
        } else
        {
            congeladoPlayer.SetActive(false);

            //Usar input get axis != 0
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                _aniPlayer.SetBool(hashMoveRight, false);
                _aniPlayer.SetBool(hashMoveLeft, false);
            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                _aniPlayer.SetBool(hashMoveRight, false);
                _aniPlayer.SetBool(hashMoveLeft, true);
            }
            if (Input.GetKeyDown(KeyCode.D))
            {
                _aniPlayer.SetBool(hashMoveLeft, false);
                _aniPlayer.SetBool(hashMoveRight, true);
            }

            horialAxis = Input.GetAxis("Horizontal") * 20.0f;
            if (grounded)
            {
                canHead = false;
            }
            else
            {
                canHead = true;
            }

            if (Input.GetKeyDown(KeyCode.W))
            {
                Jump();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Shoot();
            }
        }
    }

    public void FixedUpdate()
    {
        rb.velocity = new Vector2(Time.deltaTime * speed * horialAxis, rb.velocity.y);
        grounded = Physics2D.OverlapCircle(checkGround.position, 0.2f, ground_layer);

        if (grounded)
        {
            _aniPlayer.SetBool(hashJump, false);
        }
    }

    public void Move(int value)
    {
        //_aniPlayer.SetBool(hashMoveRight, true);
        horialAxis = value;
    }
    public void StopMove()
    {
        //_aniPlayer.SetBool(hashMoveRight, false);
        horialAxis = 0;
    }

    public void Shoot()
    {
        if (canShoot)
        {
            _aniPlayer.SetTrigger("Shoot");
            _ball.GetComponent<Rigidbody2D>().AddForce(new Vector2(800, 900));
        }
    }

    public void Jump()
    {
        if (grounded)
        {
            _aniPlayer.SetBool(hashJump, true);
            canHead = true;
            rb.velocity = new Vector2(rb.velocity.x, 17);
        }
    }
}
