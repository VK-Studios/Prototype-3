using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Split player controls between movement and input scripts
public class PlayerInput : MonoBehaviour
{

	private PlayerControls input;
	private InputAction fire;

	// User input code init
	// Using new input system, not old one 
	private void OnEnable() {

		fire = input.Player.Fire;
		fire.Enable();
		fire.performed += Fire;

	}

	private void OnDisable() {
		fire.Disable();

	}

	void Start()
    {
        
    }


    void Update()
    {
        
    }
}
