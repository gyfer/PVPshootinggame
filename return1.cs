using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Runtime.InteropServices;
public class return1 : MonoBehaviour
{
    public GameObject detail;
    public bool islock;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Click);
        Cursor.visible = true;
    }

    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.visible = true;
        }
        
          
        
    }
    public void Click()
    {
        detail.SetActive(false);
    }
}
