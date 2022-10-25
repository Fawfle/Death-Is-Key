using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snap : MonoBehaviour
{
	[SerializeField] private float units = 1;
	[SerializeField] private float offset = 0.5f;

	private void Start()
	{
		enabled = false;
	}
	private void OnDrawGizmos()
	{
		SnapToGrid();
	}

	private void SnapToGrid()
	{
		if (enabled == false) return;
		var position = new Vector3(Mathf.Round(units * (transform.position.x - offset)) / units + offset, Mathf.Round(units * (transform.position.y - offset)) / units + offset, transform.position.z);
		transform.position = position;
	}
}
