using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AnkhAnimator : MonoBehaviour
{
    public AudioSource mainSound, breakSound;
    public static AnkhAnimator instance;
    public Action Respawn;

    private  Image image;
    public Animator anim;

    public List<Sprite> sprites;
    public Sprite sprite;
    public int spriteIndex;

	private void Awake()
	{
        if (instance != null && instance != this) { Destroy(this); }
        instance = this;

        image = GetComponent<Image>();
        anim = GetComponent<Animator>();

        sprite = sprites[0];
        spriteIndex = 0;
    }

    void ResetSprite()
	{
        image.sprite = sprites[0];
    }

    void CrackAnkh()
	{
        //print(sprites.IndexOf(sprite));
        breakSound.Play();
        spriteIndex++;
        image.sprite = sprites[spriteIndex];
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
        mainSound.Play();
        anim.Play("Effect");
	}
}
