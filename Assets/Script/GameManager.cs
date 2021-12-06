using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerTurn playerTurn = default;
    [SerializeField] HitWhiteBall whiteBall = default;
    [SerializeField] Text playerTurnText = default;

    [SerializeField] BallContainer ballContainerP1 = default;
    [SerializeField] BallContainer ballContainerP2 = default;

    [SerializeField] List<PhysicBody> balls = new List<PhysicBody>();

    [SerializeField] bool correctBallEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Mathf.RoundToInt(Time.time));

        playerTurn = (PlayerTurn)Random.Range((int)PlayerTurn.Player1, (int)PlayerTurn.Player2 + 1);

        playerTurnText.text = playerTurn.ToString();
    }

    private void LateUpdate()
    {
        if(!whiteBall.CanShoot && AllBallsAreStoped())
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        if (correctBallEnter)
        {
            NextShot();
        }
        else
        {
            ChangeTurn();
        }
    }

    public void ChangeTurn()
    {
        switch (playerTurn)
        {
            case PlayerTurn.Player1:
                playerTurn = PlayerTurn.Player2;
                ballContainerP1.SetListenPhysicWorld(false);
                ballContainerP2.SetListenPhysicWorld(true);
                break;
            case PlayerTurn.Player2:
                playerTurn = PlayerTurn.Player1;
                ballContainerP1.SetListenPhysicWorld(true);
                ballContainerP2.SetListenPhysicWorld(false);
                break;
        }

        playerTurnText.text = playerTurn.ToString();
        whiteBall.CanShoot = true;
        correctBallEnter = false;
    }

    public void NextShot()
    {
        whiteBall.CanShoot = true;
        correctBallEnter = false;
    }

    private bool AllBallsAreStoped()
    {
        for (int i = 0; i < balls.Count; i++)
        {
            if(balls[i].GetAcceleration() > 0.1f)
            {
                return false;
            }
        }

        return true;
    }

    public void CorrectBallWasEnter()
    {
        correctBallEnter = true;
    }
}

public enum PlayerTurn { Player1, Player2 };
