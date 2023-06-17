using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


public class PlayerMovement : MonoBehaviour
{

    public float movementSpeed = 5f;
    public Rigidbody2D rb;
	Vector2 moveDirection = Vector2.zero;

	private PlayerControls input;
	private InputAction move;
	private InputAction fire;
	private InputAction dash;

	public Animator legsAnim;
	public Animator torsoAnim;
	private float dir = 1f;

	//dash
	Vector2 dashDirection = Vector2.zero;

	private float activeMoveSpeed;
	public float dashSpeed;

	public float dashLength = .5f;
	public float dashCooldown = 1f;

	private float dashCounter;
	private float dashCoolCounter;

	private void Awake() {
		input = new PlayerControls();
	}

	private void Start() {
		activeMoveSpeed = movementSpeed;
	}

	// User input code init
	// Using new input system, not old one
	private void OnEnable() {
		move = input.Player.Move;
		move.Enable();

		fire = input.Player.Fire;
		fire.Enable();
		fire.performed += Fire;

		dash = input.Player.Dash;
		dash.Enable();
		dash.performed += Dash;
	}

	private void OnDisable() {
		move.Disable();
		fire.Disable();
		
	}

	// Update is called once per frame
	void Update() {
		
		if (dashCounter == dashLength) {
			dashDirection = move.ReadValue<Vector2>();
		}
		
		if (dashCounter > 0) {
			dashCounter -= Time.deltaTime;

			
			moveDirection = dashDirection;

			if (dashCounter <= 0) {
				activeMoveSpeed = movementSpeed;
				dashCoolCounter = dashCooldown;
			}

		} else {
			//IMPORTANT MOVEMENT CODE 
			moveDirection = move.ReadValue<Vector2>();

			legsAnim.SetInteger("xInput", Mathf.RoundToInt(moveDirection.x));
			legsAnim.SetInteger("yInput", Mathf.RoundToInt(moveDirection.y));

			if (moveDirection.x == 0 && moveDirection.y >= 0.01) {
				//up
				dir = 1;
				//Debug.Log("1");
			} else if (moveDirection.x == 0 && moveDirection.y <= -0.01) {
				//down
				dir = 2;
				//Debug.Log("2");
			} else if (moveDirection.x >= 0.01) {
				//right
				dir = 3;
				//Debug.Log("3");
			} else if (moveDirection.x <= -0.01) {
				//left
				dir = 4;
				//Debug.Log("4");
			}

			legsAnim.SetFloat("lastInput", dir);

			if (moveDirection.x != 0 || moveDirection.y != 0) {
				legsAnim.SetBool("isMoving", true);
			} else {
				legsAnim.SetBool("isMoving", false);
			}
		}

		if(dashCoolCounter > 0) {
			dashCoolCounter -= Time.deltaTime;
		}
    }

	private void FixedUpdate() {
		//movement
		rb.velocity = new Vector2(moveDirection.x * activeMoveSpeed, moveDirection.y * activeMoveSpeed);
	}


	private void Fire(InputAction.CallbackContext context) {
		Debug.Log("Bazinga");
	}

	private void Dash(InputAction.CallbackContext context) {
		
		if(dashCoolCounter <= 0 && dashCounter <= 0) {
			activeMoveSpeed = dashSpeed;
			dashCounter = dashLength;
		}

	}
}
