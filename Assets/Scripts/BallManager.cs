using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class BallManager : MonoBehaviour
{
    //creacion bola
    [SerializeField]
    private GameObject ball;

    [SerializeField]
    private GameObject ballSpawner0;

    [SerializeField]
    private GameObject ballSpawner1;

    [SerializeField]
    private GameObject ballSpawner2;

    private float xLimit;
    public GameObject actualBall;
    public Ball ballScript;
    private int[] posiblesY = { -4, 4 };
    private int[] posiblesX = { -4, 4 };

    //puntiacion 
    [SerializeField]
    TextMeshProUGUI score;
    [SerializeField]
    TextMeshProUGUI score2;
    [SerializeField]
    TextMeshProUGUI victory;
    [SerializeField]
    Button menu;

    int scorePlayer1 = 0;
    int scorePlayer2 = 0;
    public bool finished;

    public MovePlayer twoPlayerModeScript;
    public IA onePlayerModeScript;
    public AudioSource scorePoint;

    // Start is called before the first frame update
    void Start()
    {
        ballScript = ball.gameObject.GetComponent<Ball>();
        scorePoint = gameObject.GetComponent<AudioSource>();
        xLimit = GetScreenWide(gameObject.transform) / 2;
        CreateBall(ballSpawner0, posiblesX[Random.Range(0, 1)]);

        score.SetText(scorePlayer1.ToString());
        finished = false;

        if(Menu.multiplayer == false)
        {
            onePlayerModeScript.enabled = true;
            twoPlayerModeScript.enabled = false;
            
        }
        else if(Menu.multiplayer ==true)
        {
            onePlayerModeScript.enabled = false;
            twoPlayerModeScript.enabled = true;
        }
    }



    // Update is called once per frame
    void Update()
    {
        if(!finished)
        {
            if (actualBall.transform.position.x >= xLimit)
            {

                scorePlayer1++;
                score.SetText(scorePlayer1.ToString());
                scorePoint.Play();
                Destroy(actualBall);
                if (scorePlayer1 == 10)
                {
                    victory.SetText("YOU WON PLAYER 1");
                    menu.gameObject.SetActive(true);
                    finished = true;
                }
                else
                {
                    CreateBall(ballSpawner1, posiblesX[1]);
                }

            }
            else if (actualBall.transform.position.x <= -xLimit)
            {

                scorePlayer2++;
                score2.SetText(scorePlayer2.ToString());
                scorePoint.Play();
                Destroy(actualBall);
                if (scorePlayer2 == 10)
                {
                    victory.SetText("YOU WON PLAYER 2");
                    menu.gameObject.SetActive(true);
                    finished = true;
                }
                else
                {
                    CreateBall(ballSpawner2, posiblesX[0]);
                }
            }
        }

        if (Input.GetKey("escape"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
    
    public void Win(GameObject winner)
    {
        victory.SetText("You won " + winner.ToString());

    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CreateBall(GameObject ballSpawner,float x)
    {

        actualBall = Instantiate(ball, ballSpawner.transform.position, Quaternion.identity);
        ballScript = actualBall.gameObject.GetComponent<Ball>();
        ballScript.actualSpawn = ballSpawner;
        ballScript.direction = new Vector2(x, posiblesY[Random.Range(0,1)]);
        ballScript.LastToColl = ballSpawner.transform.parent.gameObject;
    }


    public float GetScreenWide(Transform x)
    {
        float cameraDistance = x.position.z - Camera.main.transform.position.z;
        float Wide = Camera.main.ViewportToWorldPoint(new Vector3(1f, 1f, cameraDistance)).x
            - Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraDistance)).x;
        return Wide;

    }
}
