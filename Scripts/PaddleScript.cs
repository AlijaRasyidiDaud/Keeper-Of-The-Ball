using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

	public float speed;
    public float rightSide;
    public float leftSide;
    public GameManager gm;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Cek apakah game over ?
        if (gm.gameOver)
        {
            transform.position = new Vector2 (0f, transform.position.y);
            return;
        }

        // Input sumbu-x paddle
        float horizontal = Input.GetAxis ("Horizontal");

        // Pergerakan paddle pada sumbu-x
        transform.Translate (Vector2.right * horizontal * Time.deltaTime * speed);

        // Kontrol agar paddle tidak tembus layar kiri dan kanan
        if (transform.position.x < leftSide)
        {
            transform.position = new Vector2 (leftSide, transform.position.y);
        }
        if (transform.position.x > rightSide)
        {
            transform.position = new Vector2 (rightSide, transform.position.y);
        }
	}

    void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Extra Live"))
        {
            gm.UpdateLives (1);
            Destroy (other.gameObject);
        }
    }

}
