using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
public class defendjh : NetworkBehaviour
{
    [SyncVar]
    public bool isred;
    public const int maxHealth = 500;
    public GameObject[] came3;
    //[SyncVar]
    //public int currentHealth = maxHealth;
    public bool destroyOnDeath = true;
    //public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public Slider healthSlider1;
    public Canvas health1;
    public GameObject network;
    public GameObject enter;
    // Start is called before the first frame update
    void Start()
    {
        enter = GameObject.FindGameObjectWithTag("camera1");
        network = GameObject.FindGameObjectWithTag("network");
        //enter.SetActive(false);
        currentHealth = 500;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int damage)
    {

        if (isServer == false) return;// 血量的处理只在服务器端执行

        currentHealth -= damage;
        OnChangeHealth(currentHealth);
        //m_Text.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            if (destroyOnDeath)
            {
                //Destroy(this.gameObject);
                //NetworkServer.DisconnectAll();
                
                network.GetComponent<NetworkManager>().StopServer();
                enter.SetActive(false);
                SceneManager.LoadScene("demo1");
                //Application.Quit();
                //Destroy(network);

                return;
            }

            //currentHealth = maxHealth;
            // Debug.Log("Dead");
           // RpcRespawn();
        }

    }
    void OnChangeHealth(int health)
    {
        healthSlider1.value = (float)health / maxHealth;
        

    }
}
