using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroyOnLoad : MonoBehaviour
{
	public int destroyIndex;
	public bool destroyAtLast = false;

	private void Awake()
	{
		if (GameObject.FindGameObjectsWithTag(gameObject.tag).Length > 1)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		if (destroyAtLast) destroyIndex = SceneManager.sceneCountInBuildSettings - 1;
	}

	private void OnEnable()
	{
		SceneManager.sceneLoaded += SceneManager_sceneLoaded;
	}
	private void OnDisable()
	{
		SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
	}

	private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
	{
		if (SceneManager.GetActiveScene().buildIndex == destroyIndex)
		{
			Destroy(gameObject);
		}
	}
}