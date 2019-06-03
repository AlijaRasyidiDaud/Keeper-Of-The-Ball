using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	public Rigidbody2D rb;
	public bool inPlay;
	public Transform paddle;
	public float speed;
	public Transform explosion;
	public Transform extraLive;
	public GameManager gm;

	// Use this for initialization
	void Start () {
		
		// Ambli komonen rigid body
		rb = GetComponent<Rigidbody2D> ();
		
	}
	
	// Update is called once per frame
	void Update () {

		// Cek apakah game over ?
		if (gm.gameOver)
		{
			inPlay = false;
			rb.velocity = Vector2.zero;
			transform.position = paddle.position;
			return;
		}
		
		// Cek jika kondisi bola sedang main
		if (!inPlay)
		{
			// Posisi inisial di paddle
			transform.position = paddle.position;

			// Reset bounciness ke 1.1
			PhysicsMaterial2D bounce = new PhysicsMaterial2D("Bouncy");
			bounce.bounciness = 1.1f;
			bounce.friction = 0f;
			gameObject.GetComponent<Collider2D> ().sharedMaterial = bounce;
		}

		// Perintah menembakan bola
		if (Input.GetButtonDown ("Jump") && !inPlay)
		{
			inPlay = true;
			rb.AddForce (Vector2.up * speed);
		}

		// Ubah bounciness ke 1 jika sudah terlalu cepat
		if (rb.velocity.magnitude >= 15f)
		{
			PhysicsMaterial2D bounce = new PhysicsMaterial2D("Bouncy2");
			bounce.bounciness = 1f;
			bounce.friction = 0f;
			gameObject.GetComponent<Collider2D> ().sharedMaterial = bounce;
		}

		// Debug.Log (rb.velocity.magnitude);

	}

	// Cek jika bola jatuh ke bawah
	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.CompareTag ("Bottom"))
		{
			rb.velocity = Vector2.zero;
			inPlay = false;
			gm.UpdateLives (-1);
		}
	}

	// Fungsi jika menabrak collision (brick atau dll) brick pecah
	void OnCollisionEnter2D (Collision2D other)
	{

		// Bila menabrak brick
		if (other.transform.CompareTag ("Brick")) // Pada Collision2D tak bisa langsung ambil Comparetag
		{
			FindObjectOfType<AudioManager> ().Play("Brick"); // Efek kena brick
			BrickScript brickScript = other.gameObject.GetComponent<BrickScript>(); // Ambil script nya brick
			// Cek jika hit point
			if (brickScript.hitPoint > 1) // Brick belum hancur, kurangi hit point brick
			{
				brickScript.BreakBrick ();
			} else // Brick hancur
			{
				// Memunculkan extra live secara random saat brick hancur
				int rd = Random.Range (1,101); // Menggambil nilai acak dari 1 sampai 100 (nilai maks pada int eksklusif)
				if (rd > 10 && rd < 20)
				{
					Instantiate (extraLive, other.transform.position, other.transform.rotation);
				}	

				Transform newExplosion = Instantiate (explosion, other.transform.position, other.transform.rotation);
				Destroy (newExplosion.gameObject, 2.5f); // Instantiate efek pecah
				gm.UpdateScore (brickScript.points); // Ambil nilai point brick dari BrickScript dan diupdate ke gm
				gm.UpdateNumberOfBrick (); // Cek banyak brick yang tersisa
				Destroy (other.gameObject);
			}
		}

		// Bila menabrak paddle
		if (other.transform.CompareTag ("Paddle"))
		{
			FindObjectOfType<AudioManager> ().Play("Paddle");
		}

		// Bila menabrak wall
		if (other.transform.CompareTag ("Wall"))
		{
			FindObjectOfType<AudioManager> ().Play("Wall");
		}
	}
}
