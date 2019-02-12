using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class PlayerControl : MonoBehaviour {

    private Rigidbody rb;

    public Text countText;
    public float speed;
    public int count;
    public int countBlue;
    public int countRed;
    public int countYellow;

    private Vector3 startPos;

    //End game timer
    public Text time;
    float timeLeft = 15.0f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count = 0;
        countBlue = 0;
        countRed = 0;
        countYellow = 0;
        SetCountText();
        startPos = rb.position;
        LeftTime();
        
    }

   
    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movment = new Vector3(moveHorizontal, 0.0f, moveVertical);

        rb.AddForce(movment * speed);

        LeftTime();


    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pickups"))
        {
            other.gameObject.SetActive(false);
            countBlue = countBlue + 1;
            SetCountText();
        }
            
        if (other.gameObject.CompareTag("PickupsRed"))
        {
            other.gameObject.SetActive(false);
            countRed = countRed + 5;
            SetCountText();
        }

        if (other.gameObject.CompareTag("PickupsYellow"))
        {
            other.gameObject.SetActive(false);
            countYellow = countYellow + 10;

            SetCountText();
        }

        if (other.gameObject.CompareTag("Reset"))
        {
            resetGameState();
        }

    }

    void SetCountText()
    {
        count = countBlue + countRed + countYellow;
        countText.text = "Score: " + count.ToString();

    }

    void resetGameState()
    {
        count = 0;
        SetCountText();
        rb.position = startPos;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        SceneManager.LoadScene(0);

    }

    void LeftTime()
    {

        timeLeft -= (Time.deltaTime);

        time.text = "Time Left: " + Mathf.Round(timeLeft);

        if (timeLeft < 0)
        {
            SceneManager.LoadScene(0);
        }
    }


}






