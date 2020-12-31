using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;
public class reset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(Click);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {
        SceneManager.LoadScene("enter");
        Application.Quit();
    }
}
