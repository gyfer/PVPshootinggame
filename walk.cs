using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class walk : NetworkBehaviour
{
    public float speed = 0.5F;
    public float jumpSpeed = 400.0F;
    public float gravity = 20.0F;
    [SyncVar] public bool leaving = false;
    public Vector3 moveDirection = Vector3.zero;
    CharacterController controller;

    public AudioSource audioSource;
    public AudioClip fx_walk;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (isLocalPlayer)
        {
            if (!leaving)
            {
               if (controller.isGrounded)
            {
                moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
                moveDirection = transform.TransformDirection(moveDirection);
                moveDirection *= speed;
                moveDirection.y = 0;
                if (Input.GetButton("Jump"))
                    moveDirection.y = jumpSpeed;
                
            }
           
            }
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * Time.deltaTime);
            if (moveDirection.x!=0|| moveDirection.z != 0)
            {
                if (!audioSource.isPlaying)
                {
                    audioSource = GetComponent<AudioSource>();
                    audioSource.PlayOneShot(fx_walk);
                }
               
            }
            else
            {
                //audioSource.Stop();
            }
            
        }
    }
}
