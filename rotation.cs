using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Runtime.InteropServices;
public class rotation : NetworkBehaviour
{
    public enum RotationAxes
    {
        MouseXAndY = 0,
        MouseX = 1,
        MouseY = 2
    }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityHor = 9f;
    public float sensitivityVert = 9f;
    public bool islock=true;

    public float minmumVert = -45f;
    public float maxmumVert = 45f;

    private float _rotationX = 0;
    [DllImport("user32.dll")]
    public static extern int SetCursorPos(int x, int y);
    // Use this for initialization
    void Start()
    {
        SetCursorPos(1090, 651);
    }

    // Update is called once per frame

    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            islock = !islock;
        }
       
        if (islock){
                SetCursorPos(1000, 500 );
                Cursor.visible = false;
        }
        else
        {
            Cursor.visible = true;
        }
        
        if (isLocalPlayer)
        {
            if (axes == RotationAxes.MouseX)
            {
                transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityHor, 0);
            }
            else if (axes == RotationAxes.MouseY)
            {
                _rotationX = _rotationX - Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

                float rotationY = transform.localEulerAngles.y;

                transform.localEulerAngles = new Vector3(-_rotationX, rotationY, 0);
            }
            else
            {
                _rotationX -= Input.GetAxis("Mouse Y") * sensitivityVert;
                _rotationX = Mathf.Clamp(_rotationX, minmumVert, maxmumVert);

                float delta = Input.GetAxis("Mouse X") * sensitivityHor;
                float rotationY = transform.localEulerAngles.y + delta;

                transform.localEulerAngles = new Vector3(_rotationX, rotationY, 0);
            }
        }
    }
}
