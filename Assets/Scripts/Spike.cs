using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
	Animator anim;
	Collider2D coll;
	public bool switchOnMove;
	public bool active = true;
	void Switch()
	{
		active = !active;
		coll.enabled = active;
		anim.Play(active ? "Activate" : "Deactivate");
	}

	private void Start()
	{
		coll = GetComponent<Collider2D>();
		anim = GetComponent<Animator>();
		if (switchOnMove) PlayerMovement.instance.onMove += Switch;
	}

	private void OnDisable()
	{
		if (switchOnMove) PlayerMovement.instance.onMove += Switch;
	}
}
