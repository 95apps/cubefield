using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public float moveSpeed;
    public GameObject block;
    public GameObject player;
    
    private float playerX;
    private float playerY;
    private float playerZ;

    private float deltaTime = 0;

    private Rigidbody playerRb;
    public Transform playerTransform;

    private bool inGame;
    public GameObject canvas;
    public Text score;
    public Text highscore;
    public int myScore;
    private int blocksToSpawn = 10;

    public GameObject blocks;
   
    public GameObject UIButtonInputAxis;

    public float move = 0;
    float rotationZ;
    private bool moveLeft = false;
    private bool moveRight = false;

    // Use this for initialization
    void Start () {
  
        playerRb = GetComponent<Rigidbody>();
        highscore.text = PlayerPrefs.GetInt("Highscore").ToString();
        inGame = false;
    }

    public void StartGame()
    {
        playerRb.useGravity = false;
        playerRb.constraints = RigidbodyConstraints.FreezePositionY;
        playerRb.constraints = RigidbodyConstraints.None;
        playerRb.freezeRotation = true;
        
        canvas.SetActive(false);
        inGame = true;
    }

    public void EndGame()
    {

        myScore = 0;
        blocksToSpawn = 10;
        inGame = false;
        
          

    }

    void SpawnBlocks (int blocksToSpawn)
    {

        for(int i = 0; i < blocksToSpawn; i++){
            GameObject newBlock =Instantiate(block, new Vector3(playerX + Random.Range(-200, 200), player.transform.position.y, playerZ + Random.Range(125, 175)), Quaternion.Euler(0, 0, 0));
            newBlock.transform.parent = blocks.transform;
        }
    }
	
	// Update is called once per frame

    void Update ()
    {


        if (myScore > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", myScore);
            highscore.text = myScore.ToString();
        }

        if(playerTransform.position.y % 200 == 0)
        {
            playerRb.useGravity = false;
        }

        switch (myScore)
        {
            case 500:
                blocksToSpawn = 20;
                break;
            case 1000:
                blocksToSpawn = 40;
                break;
            case 2000:
                blocksToSpawn = 60;
                break;
        }
        
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        playerZ = player.transform.position.z;

        if (inGame)
        {
            if (deltaTime %2 == 0 && inGame == true)
            {
                myScore += 1;
                score.text = myScore.ToString();
            }
            if (deltaTime % 30 == 0)
            {
                SpawnBlocks(blocksToSpawn);
            }
        }

        //MOVEMENT STUFF

        if (Input.GetKey("a") || moveLeft && move != -1)
        {

            move = Mathf.Lerp(move, -2, Time.deltaTime);
            float z = move * -15.0f; // might be negative, just test it
       


        }
        else if (move != 0)
        {
            move = Mathf.Lerp(move, 0, Time.deltaTime);
            float z = move * -15.0f; // might be negative, just test it
   
        }

        if (Input.GetKey("d") || moveRight && move != 1)
        {
            move = Mathf.Lerp(move, 2, Time.deltaTime);
            float z = move * -15.0f; // might be negative, just test it
 

        }
        else if (move != 0)
        {
            move = Mathf.Lerp(move, 0, Time.deltaTime);
            float z = move * -15.0f; // might be negative, just test it
      
        }

        playerRb.velocity = new Vector3(move * moveSpeed, playerRb.velocity.y, 0);

        rotationZ = move * 15;
        rotationZ = Mathf.Clamp(rotationZ, -15, 15);
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ);
    }

    public void MoveLeft()
    {
        moveLeft = true;
    }

    public void MoveRight()
    {
        moveRight = true;
    }

    public void DontMove()
    {
        moveLeft = false;
        moveRight = false;
    }

    void LateUpdate()
    {
        


    }

    void FixedUpdate () {

       




        transform.Translate(0f, 0f, (moveSpeed) *Time.deltaTime);
         
         
   
    


        deltaTime += 1;

        if(deltaTime == -10)
        {
            //endgame wait 100 frames
            canvas.SetActive(true);
            foreach (Transform block in blocks.transform)
            {
                Destroy(block.gameObject);
            }
        }
    
    }

    void OnCollisionEnter(Collision col)
    {
        deltaTime = -110;
        EndGame();
        playerRb.useGravity = true;
        
    }
}
