using UnityEngine;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class bullet : NetworkBehaviour
{
    [SyncVar]
    public bool isred1;
    [SyncVar] public bool juji=false;
    [SyncVar] public bool isfollow = false;
    [SyncVar] public bool isplayerout = false;
    public GameObject me;
    public GameObject target;
    
    //当子弹与物体碰撞时
    void Start()
    {
        
        if (isred1)
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

        }
        else
        {
            gameObject.GetComponent<MeshRenderer>().material.color = Color.blue;
        }
    }
    void Update()
    {
        if (isfollow)
        {
            transform.LookAt(target.transform.position);
            gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 200;
        }
    }
   
    void OnCollisionEnter(Collision other)
    {
        if (!isServer) return;
        if (!juji)
        {
            if (isplayerout)
            {
                if ((transform.position - me.transform.position).sqrMagnitude >= 30000)
                {
                    return;
                }
            }

        }
        GameObject hit = other.gameObject;
        player player1= hit.GetComponent<player>();
        health health = hit.GetComponent<health>();
        defendjh jh = hit.GetComponent<defendjh>();
        if (hit != me)
        {
            
            if (health != null)
            {
            if (player1.isleaving)
            {
                player1.nodemage = true;
            }
            if (player1.nodemage)
            {
                Destroy(gameObject);
                return;
            }
                if (health.isdeath) return;
                health.TakeDamage(4);
                Destroy(gameObject);
            }
            if (jh != null)
            {
                if (isred1 != hit.GetComponent<defendjh>().isred)
                {
                    jh.TakeDamage(10);
                    Destroy(gameObject);
                }

            }

        }


    }
}

