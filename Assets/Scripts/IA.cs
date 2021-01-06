using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IA : MonoBehaviour
{
    
    [SerializeField]
    BallManager ballScript;
    public  GameObject actualBall;
    
    float speed = 6f;
    private float yLimit;
    bool finish;
    // Start is called before the first frame update
    void Start()
    {
        yLimit = GetScreenHeight(gameObject.transform) / 2;
        
    }

    // Update is called once per frame
    void Update()
    {
        actualBall = ballScript.actualBall.gameObject;
        finish = ballScript.finished;
        if(!finish)
        {
            if (Mathf.Abs(actualBall.transform.position.y - transform.position.y) < 0.2)
            {
                return;
            }

            if (actualBall.transform.position.y < transform.position.y && gameObject.transform.position.y > (-yLimit - gameObject.transform.localScale.y / 2))
            {
                transform.position += new Vector3(0, -speed * Time.deltaTime, 0);
            }
            else if (actualBall.transform.position.y > transform.position.y && gameObject.transform.position.y < (yLimit - gameObject.transform.localScale.y / 2))
            {
                transform.position += new Vector3(0, speed * Time.deltaTime, 0);
            }
        }
        

    }
    public float GetScreenHeight(Transform player)
    {
        float cameraDistance = player.position.z - Camera.main.transform.position.z;
        float height = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraDistance)).y - Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistance)).y;
        return height;

    }
}
