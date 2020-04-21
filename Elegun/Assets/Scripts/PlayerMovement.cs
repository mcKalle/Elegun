using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public float moveSpeed = 5f;
	public float smoothFactor = 0.8f;

	public Rigidbody2D rb;
	public Camera cam;

	Vector2 movement;
	Vector2 mousePos;

	// Update is called once per frame
	void Update()
	{
		movement.x = Input.GetAxisRaw("Horizontal");
		movement.y = Input.GetAxisRaw("Vertical");

		mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
	}

	void FixedUpdate()
	{

		//rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

		RotatePlayer();

		rb.velocity = new Vector2(Mathf.Lerp(0, Input.GetAxis("Horizontal") * moveSpeed, smoothFactor), Mathf.Lerp(0, Input.GetAxis("Vertical") * moveSpeed, smoothFactor));

		//Vector2.Lerp(rb.position, rb.position + movement * moveSpeed * Time.fixedDeltaTime, );
		//rb.transform.position = Vector3.Lerp(rb.transform.position, pos, Speed * Time.deltaTime);
	}

	void RotatePlayer()
	{
		Vector2 lookDir = mousePos - rb.position;
		float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg + 90;
		rb.rotation = angle;
	}

	#region external control
	public void UpdateSpeed(float newMoveSpeed)
	{
		moveSpeed = newMoveSpeed;
	}
	#endregion
}
