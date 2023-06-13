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
	private InputAction interact;

	private Animator anim;

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
		anim = GetComponent<Animator>();
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

		interact = input.Player.Interact;
		interact.Enable();
		interact.performed += Interact;
	}

	private void OnDisable() {
		move.Disable();
		fire.Disable();
		interact.Disable();
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

			anim.SetInteger("xInput", Mathf.RoundToInt(moveDirection.x));
			anim.SetInteger("yInput", Mathf.RoundToInt(moveDirection.y));
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


	private void Interact(InputAction.CallbackContext context) {
		Debug.Log("poke");
	}
}
