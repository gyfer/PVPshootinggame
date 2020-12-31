using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
public class camera2 : NetworkBehaviour
{
    // Start is called before the first frame update
    NetworkIdentity parentIdentity; 
    
    void Start()
    {
        parentIdentity = transform.parent.GetComponent<NetworkIdentity>();
        if (!parentIdentity.isLocalPlayer)
        {
            //Destroy(gameObject,0);
            
            //InternalEditorUtility.AddTag(tag);

            //gameObject.
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
