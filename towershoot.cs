using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using UnityEngine.Networking;

public class towershoot: NetworkBehaviour
{
    public GameObject[] came1;
    public bool isred;
    public GameObject bulletPre,targetplayer;
    public Vector3 positiont;
    public float dis;
    public int t;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t++;
        if (t >= 30)
        {
            fire();
            t = 0;
        }
        //InvokeRepeating("fire", 10, 1000000000000);
    }
    public void fire()
    {
        
        came1 = GameObject.FindGameObjectsWithTag("Player");
        int f = came1.Length;
        for (int i = 0; i <= f-1; i++)
        {

            dis = (transform.position - came1[i].transform.position).sqrMagnitude;
            if (dis <= 60000&&came1[i].GetComponent<health>().isred!=isred)
            {
            positiont = came1[i].transform.position;
            transform.LookAt(positiont);
                targetplayer = came1[i];
            CmdFire();
               return;

        }
        }
        //float dis = (transform.position - player.position).sqrMagnitude
        //if
    }
    
    [Command]
    //添加一个方法
    void CmdFire()
    {
        GameObject bullet = Instantiate(bulletPre, this.transform.position , this.transform.rotation) as GameObject;
        bullet.GetComponent<Rigidbody>().velocity = transform.forward * 100;
        bullet.GetComponent<MeshRenderer>().material.color = Color.black;
        bullet.GetComponent<bullet>().target = targetplayer;
        bullet.GetComponent<bullet>().isfollow = true;
        Destroy(bullet, 6);   //2秒后销毁子弹
        NetworkServer.Spawn(bullet);
    }
}
