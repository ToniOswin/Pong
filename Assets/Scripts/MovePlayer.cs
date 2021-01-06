using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    private float speed = 10f;
    private float yLimit;

    KeyCode keyUp;
    KeyCode keyDown;
    // Start is called before the first frame update
    void Start()
    {

        
        if(gameObject.tag == "player2")
        {
            keyUp = KeyCode.UpArrow;
            keyDown = KeyCode.DownArrow;
        }
        else if(gameObject.tag == "player1")
        {
            keyUp = KeyCode.W;
            keyDown = KeyCode.S;
        }

        yLimit = GetScreenHeight(gameObject.transform) / 2;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObject.transform.position.y < (yLimit - gameObject.transform.localScale.y / 2))
        {
            if (Input.GetKey(keyUp))
            {
                gameObject.transform.Translate(Vector2.up * Time.deltaTime * speed);
            }
            
        }
        if(gameObject.transform.position.y > (-yLimit + gameObject.transform.localScale.y/2))
        {
            if (Input.GetKey(keyDown))
            {
                gameObject.transform.Translate(Vector2.down * Time.deltaTime * speed);
            }
        }
        
    }


    //get screenLimits

    public float GetScreenHeight(Transform player)
    {
        float cameraDistance = player.position.z - Camera.main.transform.position.z;
        float height = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraDistance)).y - Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistance)).y;
        return height;

    }
}
