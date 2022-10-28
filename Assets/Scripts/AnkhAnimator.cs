using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnkhAnimator : MonoBehaviour
{
    public static AnkhAnimator instance;
    public Action Respawn;

    private  Image image;
    public Animator anim;

    public List<Sprite> sprites;
    public Sprite sprite;

	private void Awake()
	{
        if (instance != null && instance != this) { Destroy(this); }
        instance = this;

        image = GetComponent<Image>();
        anim = GetComponent<Animator>();

        sprite = sprites[0]; 
    }

    void ResetSprite()
	{
        image.sprite = sprites[0];
    }

    void CrackAnkh()
	{
        //print(sprites.IndexOf(sprite));
        image.sprite = sprites[sprites.IndexOf(sprite) + 1];
	}

    void RespawnEvent()
	{
        Respawn?.Invoke();
	}

    void GiveControl()
    {
        PlayerMovement.instance.locked = false;
    }

    public void PlayAnimation()
	{
        anim.Play("Effect");
	}
}
