using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    PowerupManager powerUpManager;
    
    [SerializeField]
    string type;
    // Start is called before the first frame update
    void Start()
    {
        
        powerUpManager = GameObject.Find("GameManager").GetComponent<PowerupManager>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        
        powerUpManager.GetPU(type);
    }

}
