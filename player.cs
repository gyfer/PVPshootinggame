using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
//为了能让脚本在连上局域网的同时还能分别控制物体，所以要继承 NetworkBehaviour
public class player : NetworkBehaviour
{
	public GameObject[] came;
	public float traSpeed = 3;  //移动的速度
	public float rotSpeed = 120;  //一秒旋转的角度
	public GameObject bulletPre;   //子弹的prefab
	public Transform bulletTrans;
	public Text m_Text;
	[SyncVar] public bool isleaving=false;
	[SyncVar] public bool canleave = true;
	[SyncVar] public bool nodemage = false;
	//生成子弹的位置
	public AudioSource audioSource;
	public AudioClip fx_shoot;
	public float time3 = 0,time4;
	public Text b_text;

	void Start()

	{
		
		if (isLocalPlayer)
		{
			
			gameObject.GetComponent<health>().m_Text.gameObject.SetActive(true);
			b_text.gameObject.SetActive(true);
		}
		//GetComponent<health>().RpcRespawn();
		}
    public GameObject imagecenter;
	public bool isoutput;
	int t;int k,h=0;
	void Update()
	{
		t++;
        if (!canleave)
        {
         b_text.text = time3.ToString();
		time3 -= Time.deltaTime;
        if (time3 <= 0 && !canleave)
        {
			canleave = true;
			time3 = 0;
		    b_text.text = "";

		}
        }
		
		if (isLocalPlayer)
        {
			if (Input.GetKeyDown(KeyCode.LeftShift))
		{
			imagecenter.SetActive(isoutput);
			isoutput = !isoutput;
		}
			if (Input.GetMouseButtonDown(0))
			{
                if (!gameObject.GetComponent<health>().isdeath)
                {
                 CmdFire();
				audioSource = GetComponent<AudioSource>();
				audioSource.PlayOneShot(fx_shoot);
				}
				
			}
			if (!isleaving&&canleave)
			{
				if (Input.GetMouseButtonDown(1))
				{
					if (!gameObject.GetComponent<health>().isdeath)
					{
						Cmdleave();
					}

				}
			}
            if (isleaving)
            {
				gameObject.GetComponent<walk>().moveDirection = transform.forward * 100;
				gameObject.GetComponent<walk>().moveDirection.y = 0;
				time4 += Time.deltaTime;
                if (time4 >= 1)
                {
					endleaving();
					time4 = 0;
				}

			}
			if (nodemage)
            {
				k++;
				
				
				if (k>=3)
                {
					CmdFire();
					k = 0;
                }
				
			}

		}
		
    }
	[Command]
	void Cmdleave()
    {
		time3 = 10;
		h = 0;
		isleaving = true;
		gameObject.GetComponent<walk>().leaving = true;
		//gameObject.GetComponent<walk>().moveDirection = transform.forward * 1000;
		//gameObject.GetComponent<walk>().moveDirection.y = 0;
		//gameObject.GetComponent<Rigidbody>().velocity = transform.forward * 100;
		Invoke("endleaving", 1f);
    }

	void endleaving()
    {
		canleave = false;
		isleaving = false;
		nodemage = false;
		//gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		gameObject.GetComponent<walk>().leaving = false;
		gameObject.GetComponent<walk>().moveDirection = Vector3.zero;
		h = 0;
	}
	// isLocalPlayer 是 NetworkBehaviour 的内置属性

	//朝某个方向移动
	//围绕某轴旋转
	//public GameObject reset1, reset2;

	[Command]
	void CmdFire()
	{
		

		GameObject bullet = Instantiate(bulletPre, this.transform.position+transform.forward * 5, this.transform.rotation) as GameObject;
		bullet.GetComponent<Rigidbody>().velocity = transform.forward * 100;
		//bullet.GetComponent<MeshRenderer>().material.color = Color.blue;
		bullet.GetComponent<bullet>().isred1 = this.GetComponent<health>().isred;
		bullet.GetComponent<bullet>().me = this.gameObject;
		bullet.GetComponent<bullet>().isplayerout = true;
		Destroy(bullet, 6);   //2秒后销毁子弹
		NetworkServer.Spawn(bullet);
		
	}
}
