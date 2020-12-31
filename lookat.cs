using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookat: MonoBehaviour
{

	public GameObject playerCamera;
	// Update is called once per frame
	void Update()
	{
		transform.LookAt(playerCamera.transform);
	}
}
