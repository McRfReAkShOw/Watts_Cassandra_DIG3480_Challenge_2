using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rb2d;
    Animator anim;

    bool isLevel2;

    public float speed;
    public float jumpForce;


    public Transform startMarker;

    public Text score;
    public Text winText;
    public Text livesText;
    public Text countText;
    private AudioSource musicSource;
    public AudioClip musicClip;
    

    private int scoreValue = 0;
    private int count;
    private int lives;

    public bool isGrounded;
    private bool facingRight = true;

    // Start is called before the first frame update
    void Start()
    {
        musicSource = GetComponent<AudioSource>();
        rb2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
        lives = 3;
        SetCountText();
        anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("quit");
            Application.Quit();
        }

        rb2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        SetCountText();

        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }

    }
    

    void SetCountText()
    {
        if (scoreValue >= 4 && !isLevel2)
        {
            Debug.Log("Level2");
            transform.position = new Vector2(startMarker.position.x, startMarker.position.y);
            lives = 3;
            scoreValue = 0;
            isLevel2 = true;
        }
        
        if (scoreValue >= 8 && isLevel2 == true)
        {
            winText.text = "You've Won! Game Created by Cassandra Watts";
            musicSource.PlayOneShot(musicClip);
        }
        livesText.text = "Lives: " + lives.ToString();

        if (lives <= 0)
        {
            winText.text = "Game Over!";
            Destroy(gameObject);
        }
    }
   


        private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Hit");
            collision.gameObject.SetActive(false);
            lives = lives - 1;
            SetCountText();
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
            if (Input.GetKeyDown(KeyCode.W))
            {
                anim.SetInteger("State", 3);
                rb2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            }
                isGrounded = false;
            if (Input.GetKeyUp (KeyCode.W))
            {
                anim.SetInteger("State", 0);
            }
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp (KeyCode.D))
        {
            anim.SetInteger("State", 0);
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetInteger("State", 0);

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 2);
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            anim.SetInteger("State", 0);
        }

    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x = Scaler.x * -1;
        transform.localScale = Scaler;
    }




}
