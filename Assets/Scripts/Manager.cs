using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class Manager : MonoBehaviour
{
    public static Manager instance;
    private TMP_Text movesText;
    private Animator loseScreenAnim, transitionAnim;
    private GameObject moveLayout;
    public GameObject ankhMoveDisplay;
    private Button retryButton;

    private GameObject player;
    private Vector3 playerStartPos;

    public List<int> moveList;
    public int moveListIndex = 0;

    public int moves;

    public int keys = 0;

    public bool tutorial = false;


    private void Awake()
	{
        if (instance != null && instance != this) { Destroy(this); }
        instance = this;
    }
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (!tutorial)
        {
            movesText = GameObject.Find("MoveDisplay").GetComponent<TMP_Text>();
        }
        loseScreenAnim = GameObject.Find("Retry Screen").GetComponent<Animator>();
        transitionAnim = GameObject.Find("Transition Screen").GetComponent<Animator>();
        moveLayout = GameObject.Find("Move Layout");
        retryButton = GameObject.Find("Retry Button").GetComponent<Button>();
        retryButton.onClick.AddListener(Reset);

        playerStartPos = player.transform.position;

        for (int i = 1; i< moveList.Count; i++)
		{
            GameObject g = Instantiate(ankhMoveDisplay, moveLayout.transform);
            g.transform.GetChild(0).GetComponent<TMP_Text>().text = moveList[i].ToString();
		}

        SetMoves(moveList[0]);
    }

    public void SetMoves(int newMoves)
	{
        moves = newMoves;
        if (!tutorial) movesText.text = $"{moves}";
    }

	public void Move()
	{
        if (!tutorial) SetMoves(moves - 1);
    }

    public void CheckMoves()
	{
        if (moves <= 0)
		{
            Lose();
		}
	}

    public void Die()
	{
        moveListIndex++;
        if (moveListIndex >= moveList.Count) { Lose(); return; }
        AnkhAnimator.instance.PlayAnimation();
	}

    public void Respawn()
	{
        player.transform.position = playerStartPos;

        Destroy(moveLayout.transform.GetChild(0).gameObject);
        SetMoves(moveList[moveListIndex]);
        PlayerMovement.instance.ChangeState("idle");
        PlayerMovement.instance.locked = true;
    }

	private void OnEnable()
	{
        AnkhAnimator.instance.Respawn += Respawn;
	}

	private void OnDisable()
	{
        AnkhAnimator.instance.Respawn -= Respawn;
    }



	public void Lose()
	{
		loseScreenAnim.Play("Slide In");
        PlayerMovement.instance.anim.Play("Lose");
	}

    public void Continue()
	{
        loseScreenAnim.Play("Slide In");
        // reuse lose screen
        loseScreenAnim.gameObject.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = " YOU WIN!";
        retryButton.onClick.RemoveAllListeners();
        retryButton.transform.GetChild(0).gameObject.GetComponent<TMP_Text>().text = "CONTINUE";
        retryButton.onClick.AddListener(() => StartCoroutine(LoadNextScene()));
    }

    IEnumerator LoadNextScene()
	{
        transitionAnim.Play("Fade In");
        yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.R))
		{
            Reset();
		}
	}

	public void Reset()
	{
        IEnumerator ResetRoutine()
        {
            transitionAnim.Play("Fade In");
            yield return new WaitForSeconds(transitionAnim.GetCurrentAnimatorStateInfo(0).length);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        StartCoroutine(ResetRoutine());
    }
}
