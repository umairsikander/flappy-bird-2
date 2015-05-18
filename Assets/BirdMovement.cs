using UnityEngine;
using System.Collections;

public class BirdMovement : MonoBehaviour {

	Vector3 velocity = Vector3.zero;
	public float flapSpeed    = 100f;
	public float forwardSpeed = 1f;

	bool didFlap = false;

	Animator animator;

	public bool dead = false;
	float deathCooldown;

	public bool godMode = false;

	// Use this for initialization
	void Start () {
		animator = transform.GetComponentInChildren<Animator>();

		if(animator == null) {
			Debug.LogError("Didn't find animator!");
		}
	}

	// Do Graphic & Input updates here
	void Update() {
		if(dead) {
			deathCooldown -= Time.deltaTime;    //for restart the game by using space or mouse clic

			if(deathCooldown <= 0) {             //same
				if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
					Application.LoadLevel( Application.loadedLevel );
				}
			}
		}
		else {	                                   //for reapeat flap for space and mouse button.
			if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0) ) {
               didFlap = true;
	    }
      }
	}

	
	// Do physics engine updates here
	void FixedUpdate () {

		if(dead)      //for bird death(one position)
			return;

		GetComponent<Rigidbody2D>().AddForce( Vector2.right * forwardSpeed );   //moving to right otherwise only bird moving

		if(didFlap) {                                                          //for flap of bird using space button or mouse left click
			GetComponent<Rigidbody2D>().AddForce( Vector2.up * flapSpeed );
			animator.SetTrigger("DoFlap");


			didFlap = false;
		}
	}

//	void OnCollisionEnter2D(Collision2D collision) {    //for bird death animation
//		if(godMode)
//			return;
//
//		animator.SetTrigger("Death");
//		dead = true;
//		deathCooldown = 0.5f;
//	}
}
