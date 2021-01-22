using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BallController : MonoBehaviour
{

    private Rigidbody rb;

    public float speed;

    private int score;
    private bool finish;

    private bool playerIsOnGround = true;

    public Text scoreText;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        BasicMovement();
        Jump();
    }


    private void BasicMovement()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(x, 0, z);

        rb.AddForce(movement * speed);
    }
    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && playerIsOnGround)
        {
            rb.AddForce(new Vector3(0, 5, 0), ForceMode.Impulse);
            playerIsOnGround = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Points") 
        {
            other.gameObject.SetActive(false);
            score++;
            scoreText.text = "Score: " + score;
        }
        
        if (score == 3 && other.gameObject.name == "Finish") 
        {
            other.gameObject.SetActive(false);
            SceneManager.LoadScene("Level2");
        }

        if (score == 6 && other.gameObject.name == "Finish") 
        {
            other.gameObject.SetActive(false);
            Debug.Log("Finish");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            playerIsOnGround = true;
        }
    }
}
