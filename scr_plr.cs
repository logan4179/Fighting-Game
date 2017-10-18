using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scr_plr : MonoBehaviour 
{
	public float rotSpd = 2.0f;
	public float normalSpd = 0.2f;

	public float jumpForce = 10.0f;
	public float boostForce = 25.2f;

	public bool isGrounded = false;

	Rigidbody rb = null;

	//Alarms ------------------------------------
	public int incBoostAlarm = 500;

	// Use this for initialization
	void Start () 
	{
		rb = GetComponent<Rigidbody> ();

	}

	// Update is called once per frame
	void Update () 
	{
		getInput ();
		moveAlarms ();

	}

	void OnCollisionEnter (Collision colObj)
	{
		if (colObj.gameObject.name == "ground") 
		{
			isGrounded = true;
		}
	}
	public void getInput()
	{
		// Directional Movement ---------------------------
		if (Input.GetKey (KeyCode.W)) 
		{
			transform.Translate (0, 0, normalSpd);
		}
		else if (Input.GetKey (KeyCode.S)) 
		{
			transform.Translate (0, 0, -normalSpd);
		}

		if (Input.GetKey (KeyCode.A)) 
		{
			transform.Translate (-normalSpd, 0, 0);
		}
		else if (Input.GetKey (KeyCode.D)) 
		{
			transform.Translate (normalSpd, 0, 0);
		}
		// Rotational Movement ---------------------------
		if(Input.GetKey(KeyCode.Q))
		{
			transform.Rotate (0, -rotSpd, 0);
		}
		else if (Input.GetKey (KeyCode.E)) 
		{
			transform.Rotate (0, rotSpd, 0);
		}
		//Jumping ----------------------------------------
		if (Input.GetKeyDown (KeyCode.Space) && isGrounded) 
		{
			Invoke ("jump", 0.2f);
			isGrounded = false;
		} 
		//Boosting ---------------------------------------
		else if (Input.GetKey (KeyCode.Space) && !isGrounded && incBoostAlarm > 0) 
		{
			rb.AddForce (0, boostForce, 0);
			incBoostAlarm--;
		}
	}

	void jump()
	{
		rb.velocity = new Vector3 (0, jumpForce, 0);
	}

	void moveAlarms()
	{
		if (incBoostAlarm < 500 && !Input.GetKey (KeyCode.Space)) 
		{
			incBoostAlarm++;
		}
		else if (incBoostAlarm > 500) 
		{
			incBoostAlarm = 500;
		}
	}
}