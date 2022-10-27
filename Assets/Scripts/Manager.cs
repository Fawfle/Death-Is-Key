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

    private GameObject player;
    private Vector3 playerStartPos;

    public List<int> moveList;
    public int moveListIndex = 0;

    public int moves;


    private void Awake()
	{
        if (instance != null && instance != this) { Destroy(this); }
        instance = this;
    }
	void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        movesText = GameObject.Find("MoveDisplay").GetComponent<TMP_Text>();
        loseScreenAnim = GameObject.Find("Retry Screen").GetComponent<Animator>();
        transitionAnim = GameObject.Find("Transition Screen").GetComponent<Animator>();
        moveLayout = GameObject.Find("Move Layout");
        GameObject.Find("Retry Button").GetComponent<Button>().onClick.AddListener(Reset);

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
        movesText.text = $"{moves}";
    }

	public void Move()
	{
        SetMoves(moves - 1);
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
        IEnumerator DeathRoutine()
		{
            moveListIndex++;
            if (moveListIndex >= moveList.Count) { Lose(); yield break; }

            AnkhAnimator.instance.PlayAnimation();
            yield return new WaitForSeconds(AnkhAnimator.instance.anim.GetCurrentAnimatorStateInfo(0).length);
            player.transform.position = playerStartPos;

            Destroy(moveLayout.transform.GetChild(0).gameObject);
            SetMoves(moveList[moveListIndex]);
            PlayerMovement.instance.ChangeState("idle");
		}
        StartCoroutine(DeathRoutine());
	}

    public void Lose()
	{
		loseScreenAnim.Play("Slide In");
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
