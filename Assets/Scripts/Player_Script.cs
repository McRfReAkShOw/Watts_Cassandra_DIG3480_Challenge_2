using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Script : MonoBehaviour
{
    private Rigidbody2D rd2d;

    public float speed;
    public float jumpForce;

    public Text score;
    public Text winText;
    public Text livesText;

    private int scoreValue = 0;
    private int count;
    private int lives;

    public bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        winText.text = "";
       

        
        lives = 3;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float verMovement = Input.GetAxis("Vertical");
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }

        rd2d.AddForce(new Vector2(hozMovement * speed, verMovement * speed));
        if (scoreValue >= 4)
        {
            winText.text = "You've Won! Game Created by Cassandra Watts";
        }
        livesText.text = "Lives: " + lives.ToString();
        
        
    }

       
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = scoreValue.ToString();
            Destroy(collision.collider.gameObject);
        }
       
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            isGrounded = true;
            if (Input.GetKey (KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isGrounded = false;
            }
        }
    }
     


}
