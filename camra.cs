using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using UnityEngine.UI;
public class camra : NetworkBehaviour
{
    public GameObject playerCamera;
    void start()
    {
        if (!isLocalPlayer)
        {
            //Destroy(,0);
            //InternalEditorUtility.AddTag(tag);

            //gameObject.
        }
    }
    public override void OnStartLocalPlayer()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;

        playerCamera.SetActive(true);       //激活自己的相机
    }
}
