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

	private void Awake() {
		input = new PlayerControls();
	}

	private void OnEnable() {
		move = input.Player.Move;
		move.Enable();
	}

	private void OnDisable() {
		move.Disable();
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
}
