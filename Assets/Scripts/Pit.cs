using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pit : MonoBehaviour
{
	public void BlockCollision(GameObject block)
	{
		gameObject.layer = 0;
		SpriteRenderer blockRender = block.GetComponent<SpriteRenderer>();
		blockRender.color = new Color32(150, 150, 150, 255);
		blockRender.sortingOrder = -1;
		block.GetComponent<Collider2D>().enabled = false;
	}
}
