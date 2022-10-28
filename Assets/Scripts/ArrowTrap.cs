using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
	Animator anim;
	public GameObject arrow;

	public Vector2 direction;

	IEnumerator SpawnArrow(float time)
	{
		yield return new WaitForSeconds(time);
		RaycastHit2D collision = Physics2D.Raycast(transform.position + new Vector3(0, (direction.y < 0) ? -1f : 0f), direction);
		// lock player if they "got hit"
		//print(collision.transform.gameObject.name);
		if (collision.transform.gameObject.CompareTag("Player")) PlayerMovement.instance.ChangeState("locked");
		if (direction.y < 0) anim.Play("Shoot-2");
		else anim.Play("Shoot");
		GameObject arr = Instantiate(arrow, transform.position + new Vector3(0, (direction.y < 0) ? -1f : 0f), Quaternion.identity);
		SpriteRenderer arrRenderer = arr.GetComponent<SpriteRenderer>();

		if (direction.y < 0) { arr.transform.rotation = Quaternion.Euler(0, 0, -90); }
		if (direction.y > 0) { arr.transform.rotation = Quaternion.Euler(0, 0, -90); arrRenderer.flipX = true; }
		

		arr.GetComponent<Rigidbody2D>().velocity = direction * 30;
		if (direction.y == 0) arrRenderer.flipX = direction.x < 0;
	}

	void Shoot()
	{
		StartCoroutine(SpawnArrow(PlayerMovement.instance.timeToMove - 0.05f));
	}

	private void Start()
	{
		anim = GetComponent<Animator>();
		PlayerMovement.instance.onMove += Shoot;
	}

	private void OnDisable()
	{
		PlayerMovement.instance.onMove -= Shoot;
	}
}
