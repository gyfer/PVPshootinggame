using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class health : NetworkBehaviour
{
    
    [SyncVar]public bool isred;
    [SyncVar] public bool ispreparing=false,isready=false,isreadyall=false;
    public GameObject[] came2;
    public Text m_Text,h_text,t_text;
    public const int maxHealth = 100;
    [SyncVar(hook = "OnChangeHealth")]
    public int currentHealth = maxHealth;
    public Slider healthSlider;
    public Canvas health1;
    public bool destroyOnDeath = false;
    public GameObject reset1, reset2,inspect,defend1,defend2,camera1,camera3,network; 
    public NetworkStartPosition[] spawnPoints;
    public GameObject me;
    bool recreat=true;
    public float time1=10,time2;
    public  void OnStart()
    {
        //GetComponent<MeshRenderer>().material.color = Color.blue;
        if (isred)
        {
              transform.position = reset1.transform.position;
                GetComponent<MeshRenderer>().material.color = Color.red;
        }

        else
        {
               transform.position = reset2.transform.position;
                 GetComponent<MeshRenderer>().material.color = Color.blue;
        }
         reset1= GameObject.FindGameObjectWithTag("reset1");
         reset2 = GameObject.FindGameObjectWithTag("reset2");
        //net.GetComponent<NetworkManagerHUD>().showGUI = false;
    }

    void Start()
    {
        
        reset1 = GameObject.FindGameObjectWithTag("reset1");
        reset2 = GameObject.FindGameObjectWithTag("reset2");
        defend1 = GameObject.FindGameObjectWithTag("defend1");
        defend2 = GameObject.FindGameObjectWithTag("defend2");
        inspect = GameObject.FindGameObjectWithTag("inspect");
      
        if (isServer){
                came2 = GameObject.FindGameObjectsWithTag("Player");
                int f = came2.Length;
                //isred = true;
                for (int i = 0; i <= f-1; i++) {
                came2[i].GetComponent<health>().ispreparing = true;
                //Debug.Log(i);
                if (i % 2 == 1)
                {
                    came2[i].GetComponent<health>().isred = false;
                }
                else
                {
                    came2[i].GetComponent<health>().isred = true;
                }
                 
        }
        }
        if (isLocalPlayer)
        {
            defend1.GetComponent<defendjh>().health1.gameObject.GetComponent<lookat>().playerCamera = gameObject;
            defend2.GetComponent<defendjh>().health1.gameObject.GetComponent<lookat>().playerCamera = gameObject;
            came2 = GameObject.FindGameObjectsWithTag("Player");
            int f = came2.Length;
            for (int i = 0; i <= f - 1; i++)
            {


                came2[i].GetComponent<health>().health1.gameObject.GetComponent<lookat>().playerCamera = gameObject;


            }
        }

        //Invoke("born", 1);
        // spawnPoints = FindObjectsOfType<NetworkStartPosition>();

        //TakeDamage(100);
    }
    int t = 0;
    [SyncVar]public bool isdeath=false;
    void Update()
    {
         
        if (ispreparing)
        {
            Rpcborn();
            
                if (isServer)
            {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isready = true;
            }
                    //Debug.Log("eee");
                came2 = GameObject.FindGameObjectsWithTag("Player");
                int f = came2.Length;
                //isred = true;
                isreadyall = true;
                for (int i = 0; i <= f - 1; i++)
                {
                    
                    if (!came2[i].GetComponent<health>().isready)
                    {
                        isreadyall = false;
                    }


                }
                if (isreadyall)
                {
                    for (int i = 0; i <= f - 1; i++)
                    {
                        came2[i].GetComponent<health>().ispreparing = false;
                        came2[i].GetComponent<health>().isready = false;

                    }
                }
            }

           
        }
        
            if (isdeath)
            {
            if (isLocalPlayer) {
                t_text.text = time2.ToString();
                if (transform.position != reset1.transform.position && transform.position != reset2.transform.position)
                {
                transform.position = inspect.transform.position;
                }

                }
                   
                if (time2 <= 0.0f)
                {
                Respawn();
                isdeath = false;
                currentHealth = maxHealth;
                OnChangeHealth(currentHealth);
                Respawn();
            }
                time2 -= Time.deltaTime;
            }
        if (h_text.IsActive() && !isdeath)
        {
            Respawn();
        }
            
            
    }
    
    public void Rpcborn()
    {
        if (isred)
        {
            transform.position = reset1.transform.position;
        }
        else
        {
            transform.position = reset2.transform.position;
            
        }
    }
        public void TakeDamage(int damage)
    {

        if (isServer == false) return;// 血量

        currentHealth -= damage;
        //m_Text.text = currentHealth.ToString();
        if (currentHealth <= 0)
        {
            isdeath = true;
            time2 = time1;
            if (destroyOnDeath)
            {
                Destroy(this.gameObject); return;
            }
            Rpcdeath();
            Invoke("RpcRespawn", time1);
            Invoke("Respawn", time1);
#pragma warning disable UNT0016 // Unsafe way to get the method name
            //RpcRespawn();
#pragma warning restore UNT0016 // Unsafe way to get the method name
            //RpcRespawn();
            /*Vector3 spawnPosition = Vector3.zero;
            if (isred)
            {
                spawnPosition = reset1.transform.position;
            }
            else
            {
                spawnPosition = reset2.transform.position;
            }
            transform.position = spawnPosition;*/

            // Debug.Log("Dead");

        }

    }
    void OnChangeHealth(int health)
    {
        healthSlider.value = health / (float)maxHealth;
        if (isLocalPlayer)
        { 
               m_Text.text = health.ToString();
        }
            
    }
    public Vector3 spawnPosition;

    [ClientRpc]
    public void RpcRespawn()
    {
         
        if (isLocalPlayer == false) return;

        spawnPosition = Vector3.zero;

        //reset1.x = 1000;
            if (isred)
            {
              spawnPosition = reset1.transform.position;
            }
            else
            {
               spawnPosition = reset2.transform.position;
            }
            
        
        
        isdeath = false;
        transform.position = spawnPosition;
        transform.position = spawnPosition;
        transform.position = spawnPosition;
        
        h_text.gameObject.SetActive(false);
        t_text.gameObject.SetActive(false);
        
        currentHealth = maxHealth;
        OnChangeHealth(currentHealth);
    }
    public void Respawn()
    {

        if (isLocalPlayer == false) return;

        spawnPosition = Vector3.zero;

        //reset1.x = 1000;
        if (isred)
        {
            spawnPosition = reset1.transform.position;
        }
        else
        {
            spawnPosition = reset2.transform.position;
        }



        isdeath = false;
        transform.position = spawnPosition;
        transform.position = spawnPosition;
        transform.position = spawnPosition;

        h_text.gameObject.SetActive(false);
        t_text.gameObject.SetActive(false);

        currentHealth = maxHealth;
        OnChangeHealth(currentHealth);
    }
    [ClientRpc]
public void Rpcdeath()
{
    if (isLocalPlayer == false) return;
        isdeath = true;
        
        time2 = 10;
        spawnPosition = Vector3.zero;
        transform.position = inspect.transform.position;
        h_text.gameObject.SetActive(true);
        t_text.gameObject.SetActive(true);

      
    }
}
