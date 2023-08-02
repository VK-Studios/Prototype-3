using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class pointAtk : MonoBehaviour
{

    public GameObject attack;

    private Vector3 mousePos;
	public Transform atkLoc;
	private Vector3 objPos;
	private float angle;

	private float tan;
	private bool rotating = true;
	public bool frozen = false;

	private Vector3 effectPos;

	// Start is called before the first frame update
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
		mousePos = Mouse.current.position.value;

		mousePos.z = 10f;

		objPos = Camera.main.WorldToScreenPoint(transform.position);
		mousePos.x = mousePos.x - objPos.x;
		mousePos.y = mousePos.y - objPos.y;

		angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;

		if (!frozen) {
			transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		}
		effectPos = transform.position;

	}

}
