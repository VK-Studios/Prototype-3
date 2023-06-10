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

    public PlayerControls input;
	private InputAction move;
	private InputAction fire;
	private InputAction dash;
	private InputAction interact;

	private void Awake() {
		input = new PlayerControls();
	}

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
	void Update()
    {
        //input
		moveDirection = move.ReadValue<Vector2>();
    }

	private void FixedUpdate() {
		//movement
		rb.velocity = new Vector2(moveDirection.x * movementSpeed, moveDirection.y * movementSpeed);
	}


	private void Fire(InputAction.CallbackContext context) {
		Debug.Log("Bazinga");
	}

	private void Dash(InputAction.CallbackContext context) {
		Debug.Log("Zoom");
	}

	private void Interact(InputAction.CallbackContext context) {
		Debug.Log("poke");
	}
}
