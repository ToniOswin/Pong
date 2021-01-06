using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float force = 1f;
    public float[] directionX = { -3, 3 };
    public bool isMoving;
    public GameObject actualSpawn;

    public Vector2 direction;
    private float yLimit;
    AudioSource bounce;

    //contador
    int timeToStart = 3;
    public TextMeshProUGUI countDown;


    //powerups
    public GameObject LastToColl;
    
    // Start is called before the first frame update
    void Start()
    {
        countDown = GameObject.Find("CountDown").GetComponent<TextMeshProUGUI>();

        yLimit = GetScreenHeight(gameObject.transform) / 2;
        isMoving = false;
        StartCoroutine(countdown());
        bounce = gameObject.GetComponent<AudioSource>();

    }

    public IEnumerator countdown()
    {
        
        timeToStart = 3;
        
        while (timeToStart >= 0)
        {
            countDown.SetText(timeToStart.ToString());
            yield return new WaitForSeconds(1);
            timeToStart -= 1;
        }
        countDown.SetText(" ");
        isMoving = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(isMoving == true)
        {
            gameObject.transform.Translate(direction * force * Time.deltaTime);
        }
        else if (isMoving == false)
        {
            gameObject.transform.position = new Vector2 (gameObject.transform.position.x,actualSpawn.transform.position.y );
        }

        if (gameObject.transform.position.y >= (yLimit - gameObject.transform.localScale.y/2))
        {
            direction.y = - Mathf.Abs(direction.y);
        }
        if(gameObject.transform.position.y <= (-yLimit + gameObject.transform.localScale.y / 2))
        {
            direction.y = Mathf.Abs(direction.y);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        //obtener el punto de colision de la pelota y la barra
        float playerYColl = collision.gameObject.transform.position.y; 
        float ballYColl = gameObject.transform.position.y; ;
        float collisionPoint = ballYColl - playerYColl ;
        //bools para determinar la direccion de rebote
        bool pegaArriba = collisionPoint > 0;
        bool pegaDerecha = direction.x > 0;
        //la nueva Y sera proporcional al hecho de que si golpea arriba del todo volveria en un angulo de 45grados(la Y = X)
        float porcentage = collisionPoint/(collision.gameObject.transform.localScale.y/2);
        float newYdirection = Mathf.Abs(direction.x) * porcentage;
        //ajusto la velocidad de salida a la de entrada, ya que al reducir la Y se reduce la velocidad si no la escalo
        float velIn = Mathf.Abs(direction.x) + Mathf.Abs(direction.y);
        float velFi = Mathf.Abs(direction.x) + Mathf.Abs(newYdirection);
        float VelDif = velIn - velFi;
        float xPor = (100 * Mathf.Abs(direction.x)) / velFi;
        float yPor = 100 - xPor;
        float xDif = (VelDif * xPor) / 100;
        float yDif = (VelDif * yPor) / 100;
        //al vector de salida le sumo la diferencia necesaria para no reducir la velocidad
        Vector2 collisionDirection = new Vector2(Mathf.Abs(direction.x) +xDif, Mathf.Abs(newYdirection) + yDif);
        //ajusto la direccion con los bools
        if(!pegaArriba)
        {
            collisionDirection.y = -collisionDirection.y;
        }
        if (pegaDerecha)
        {
            collisionDirection.x = -collisionDirection.x;
        }
        direction = collisionDirection;
        //aumento la velocidad con cada choque
        force += 0.1f;
        LastToColl = collision.gameObject;
        bounce.Play();
        
    }


    //get limits
    public float GetScreenHeight(Transform y)
    {
        float cameraDistance = y.position.z - Camera.main.transform.position.z;
        float height = Camera.main.ViewportToWorldPoint(new Vector3(0f, 1f, cameraDistance)).y 
            - Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, cameraDistance)).y;
        return height;

    }


}
