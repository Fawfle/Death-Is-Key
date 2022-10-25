using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float timeToMove = 1f;
	public LayerMask collisionLayer, victoryLayer;

	private string state = "idle";

	private Vector3 moveEndPos;
	private Vector3 moveStartPos;
	private float moveLerpTime;

	private float pushTimer;

	private void Start()
	{
		
	}

	private void Update()
	{
		switch (state) {
			case "idle":

				float horizontalInput = Input.GetAxisRaw("Horizontal");
				float verticalInput = Input.GetAxisRaw("Vertical");

				if (horizontalInput == 0 && verticalInput == 0) return;

				moveEndPos = transform.position;

				if (horizontalInput != 0) moveEndPos += new Vector3(horizontalInput, 0, 0);
				else if (verticalInput != 0) moveEndPos += new Vector3(0, verticalInput, 0);

				Collider2D collision = Physics2D.OverlapCircle(moveEndPos, 0.2f, collisionLayer);
				// if collision check for "special" collisions
				if (collision)
				{
					if (collision.TryGetComponent(out PushBlock pushBlock))
					{
						// pushblock.push returns a bool based on if block can be pushed
						if(!pushBlock.Push(moveEndPos - transform.position)) ChangeState("pushing");
					}
				} else ChangeState("moving");

				break;
			case "moving":
				moveLerpTime += Time.deltaTime;

				float t = moveLerpTime / timeToMove;
				// ease out function
				t = Mathf.Sin(t * Mathf.PI * 0.5f);
				transform.position = Vector3.Lerp(moveStartPos, moveEndPos, t);

				if (Vector3.Distance(transform.position, moveEndPos) < 0.05f)
				{
					transform.position = moveEndPos;
					ChangeState("idle");
				}
			break;
			case "pushing":
				pushTimer -= Time.deltaTime;
				if (pushTimer < 0) ChangeState("idle");
			break;
		}
	}

	private void ChangeState(string newState)
	{
		state = newState;

		if (state == "idle")
		{
			if (Physics2D.OverlapCircle(transform.position, 0.2f, victoryLayer)) print("win");
		}

		if (state == "moving")
		{
			moveLerpTime = 0;
			moveStartPos = transform.position;
		}

		if (state == "pushing")
		{
			pushTimer = 0.2f;
		}
	}
}
