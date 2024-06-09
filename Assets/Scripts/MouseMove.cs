using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MouseMove : MonoBehaviour {

    public GameObject player;
    Rigidbody2D rb;
    float moveForce = 2;
    private bool isGrounded;
    private bool onLadder;
    private bool isFacingLeft;
    private bool isFacingRight;
    private Vector2 facingLeft;
    public float jumpTimeCounter;
    public float jumpTime;
    public float airTime;
    private bool isJumping;

    static public int totalLives = 3;
    public int lives;   

     void OnCollisionEnter2D(Collision2D col){
        if (col.gameObject.tag == ("Ground"))
        {
            isGrounded = true;
            Debug.Log("on ground");
        }   

        if (col.gameObject.tag == ("Ladder")){
            onLadder = true;
            Debug.Log("on ladder");
        }

        if(col.gameObject.tag == "YarnBall"){ //replace with invisible object
            ScoreScript.scoreValue += 100;
            Debug.Log("score: " + ScoreScript.scoreValue);
        }

        if (col.gameObject.tag == "YarnBall"){
            lives--;
            Debug.Log("Lives minus one");
        }
      
    }
    void OnCollisionExit2D(Collision2D col){
          if (col.gameObject.tag == ("Ground")){
            isGrounded = false;
            Debug.Log("off ground");
          }

          if (col.gameObject.tag == ("Ladder")){
            onLadder = false;
            Debug.Log("off ladder");
          }
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        facingLeft = new Vector2(-transform.localScale.x, transform.localScale.y);

        Scene currentScene = SceneManager.GetActiveScene(); //active scene
        lives = totalLives;
    }

    // Update is called once per frame
    void Update()
    {
      Vector3 position = transform.position;

      if (Input.GetKey(KeyCode.RightArrow)){
        isFacingRight = true;
        isFacingLeft = false;
        rb.AddForce(transform.right * (-moveForce), ForceMode2D.Force);    
      }
      else if (Input.GetKey(KeyCode.LeftArrow)) {
        isFacingRight = false;
        isFacingLeft = true;
        rb.AddForce(-transform.right * (-moveForce), ForceMode2D.Force); 
      }

      if (isFacingLeft){
        transform.localScale = facingLeft;
      }

      if (isFacingRight){
          if (isGrounded == true && Input.GetKeyDown(KeyCode.Space)){
          isJumping = true;
          jumpTimeCounter = jumpTime;
          rb.velocity = Vector2.up * 5;

      }

      if (Input.GetKey(KeyCode.Space) && isJumping == true) {
          if (jumpTimeCounter > 0){
          rb.velocity = Vector2.up * 5;
          jumpTimeCounter -= Time.deltaTime;
          }
          else {
            isJumping = false;
          }
      }
      
      if (Input.GetKeyUp(KeyCode.Space)){
        isJumping = false;
      }
      }
      
      if (Input.GetKeyUp(KeyCode.Space)){
        isJumping = false;
      }
      

      if (onLadder && Input.GetKey(KeyCode.UpArrow)){
        rb.AddForce(transform.up * (moveForce), ForceMode2D.Force);
      }//wef
      
      else if (!onLadder){
        
      }

      if (lives == 0){
            Debug.Log("Game over");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
       }
      
      if (position.x >= 7){
          Debug.Log("out of bounds");
          position.x = 7;
          transform.position = position;
      }

      if (position.x <= -7){
          Debug.Log("out of bounds");
          position.x = -7;
          transform.position = position;
      }
    }
}


