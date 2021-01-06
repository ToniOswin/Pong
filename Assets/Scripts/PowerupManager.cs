using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] powerUp;

    private GameObject nextPowerUp;
    private GameObject actualPowerUp;
    private BallManager managerScript;
    private GameObject lastToColl;
    private GameObject affected;
    private Vector2 PowerUpPosition;
    private float yLimit;
    private bool PUready;
    public AudioSource sound;
    
    // Start is called before the first frame update
    void Start()
    {
        managerScript = GameObject.Find("GameManager").GetComponent<BallManager>();
        

        yLimit = GetScreenHeight(gameObject.transform) / 2;
        nextPowerUp = powerUp[Random.Range(0, powerUp.Length)];
        StartCoroutine(ShowPowerUp());
    }

    // Update is called once per frame
    void Update()
    {
        if(PUready)
        {
            StartCoroutine(ShowPowerUp());
        }
    }

     public IEnumerator ShowPowerUp()
    {
        
        PUready = false;
        PowerUpPosition = new Vector2(0, Random.Range(-yLimit+1, yLimit-1));
        yield return new WaitForSeconds(10);
        actualPowerUp = Instantiate(nextPowerUp, PowerUpPosition, Quaternion.identity);
        nextPowerUp = powerUp[Random.Range(0, powerUp.Length)];
    }

    public IEnumerator BigPU()
    {

        lastToColl = managerScript.ballScript.LastToColl;
        
        sound.Play();
        Destroy(GameObject.FindGameObjectWithTag("PU"));
        float scaleY = lastToColl.transform.localScale.y;
        lastToColl.transform.localScale = new Vector3(lastToColl.transform.localScale.x, scaleY * 2, lastToColl.transform.localScale.z);
        yield return new WaitForSeconds(5);
        lastToColl.transform.localScale = new Vector3(lastToColl.transform.localScale.x, scaleY, lastToColl.transform.localScale.z);
        PUready = true;
    }

    public IEnumerator SmallPU()
    {
        lastToColl = managerScript.ballScript.LastToColl;
        if (lastToColl.gameObject.CompareTag("player1"))
        {
            affected = GameObject.FindGameObjectWithTag("player2");
        }
        else if (lastToColl.gameObject.CompareTag("player2"))
        {
            affected = GameObject.FindGameObjectWithTag("player1");
        }
        
        sound.Play();
        Destroy(GameObject.FindGameObjectWithTag("PU"));
        float scaleY = affected.transform.localScale.y;
        affected.transform.localScale = new Vector3(affected.transform.localScale.x, scaleY / 2, affected.transform.localScale.z);
        yield return new WaitForSeconds(5);
        affected.transform.localScale = new Vector3(affected.transform.localScale.x, scaleY, affected.transform.localScale.z);
        PUready = true;
    }
    
    public void GetPU(string type)
    {
        StartCoroutine(type);
    }

    public float GetScreenHeight(Transform y)
    {
        float cameraDistance = y.position.z - Camera.main.transform.position.z;
        float height = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraDistance)).y
            - Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistance)).y;
        return height;

    }

}
