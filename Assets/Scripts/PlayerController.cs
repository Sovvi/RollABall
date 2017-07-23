using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

	public float speed;
	public Text countText;
	public Text winText;
	public AudioClip pickup;
	public AudioClip youwin;

	private Rigidbody rb;
	private int count;
	private AudioSource source;
	private float volLowRange = .5f;
	private float volHighRanfe = 1.0f;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		count = 0;
		SetCountText ();
		winText.text = "";
		source = GetComponent<AudioSource>();
	}

	void Update()
	{
		if (Input.GetKey ("escape")) //Makes it so you can quit
			Application.Quit ();
	}

	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal"); //inputs and movement
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

		rb.AddForce (movement * speed);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.CompareTag ("Pick Up"))
			{
				other.gameObject.SetActive (false);
				count = count + 1; //adds number to counter
				SetCountText ();
				source.PlayOneShot(pickup, .5f); //plays pickup sound
			}
	}

	void SetCountText ()
	{
		countText.text = "Count: " + count.ToString ();
		if (count >= 8) //if the score is 8 or more
		{
			winText.text = "You Win! Hit ESC to exit"; //prints win text
			source.PlayOneShot (youwin, 1.0f); //plays win sound
		}
	}
}
