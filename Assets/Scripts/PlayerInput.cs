using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Split player controls between movement and input scripts
public class PlayerInput : MonoBehaviour
{

	private PlayerControls input;
	private InputAction fire;

	public Animator torsoAnim;
	public float atkCooldown = .5f;
	private float atkCoolCounter = 0;
	public int atkDamage;

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
		if (atkCoolCounter > 0) {
			atkCoolCounter -= Time.deltaTime;
		}
	}

	private void Fire(InputAction.CallbackContext context) {

		if (atkCoolCounter <= 0) {
			torsoAnim.SetTrigger("attack");
			atkCoolCounter = atkCooldown;
		}

	}

}
