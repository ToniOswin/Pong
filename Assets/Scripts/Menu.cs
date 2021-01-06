using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    

    public Button onePlayer;
    public Button twoPlayers;
    public static bool multiplayer;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void Start1Player()
    {
        SceneManager.LoadScene("Pong");
        multiplayer = false;

    }
    public void Start2Player()
    {
        SceneManager.LoadScene("Pong");
        multiplayer = true;
    }

    public void close()
    {
        Application.Quit();
    }
}
