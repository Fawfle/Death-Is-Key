using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushBlock : MonoBehaviour
{
	public LayerMask collisionLayer;
	public float pushDuration = 0.1f;

	public bool Push(Vector3 offsetPos)
	{
		Vector3 endPos = transform.position + offsetPos;
		Collider2D collision = Physics2D.OverlapCircle(endPos, 0.2f, collisionLayer);
		if (collision != null)
		{
			if (collision.CompareTag("Pit")) collision.gameObject.GetComponent<Pit>().BlockCollision(gameObject);
			else return true;
		}
		StartCoroutine(PushRoutine(endPos));
		return false;
	}

	public IEnumerator PushRoutine(Vector3 endPos)
	{
		float lerpTimer = 0;
		Vector3 startPos = transform.position;

		while (lerpTimer < pushDuration)
		{
			lerpTimer += Time.deltaTime;
			float t = lerpTimer / pushDuration;
			t = Mathf.Sin(t * Mathf.PI * 0.5f);
			transform.position = Vector3.Lerp(startPos, endPos, t);
			yield return null;
		}

		transform.position = endPos;
	}
}
