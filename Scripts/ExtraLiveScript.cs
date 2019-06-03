using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLiveScript : MonoBehaviour {

	public float speed;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
		// Gerakan ke bawah
		transform.Translate (new Vector2 (0f,-1f) * Time.deltaTime * speed);

		// Hilangkan objek jika tak terambil
		if (transform.position.y < -5.5)
		{
			Destroy (gameObject);
		}

	}
}
