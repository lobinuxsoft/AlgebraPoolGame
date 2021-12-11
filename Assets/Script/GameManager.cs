using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] PlayerTurn playerTurn = default;
    [SerializeField] HitWhiteBall whiteBall = default;
    [SerializeField] Text playerTurnText = default;
    [SerializeField] Text resultMatchText = default;
    [SerializeField] GameObject buttonContainer = default;

    [SerializeField] BallContainer ballContainerP1 = default;
    [SerializeField] BallContainer ballContainerP2 = default;

    [SerializeField] List<PhysicBody> balls = new List<PhysicBody>();

    [SerializeField] bool correctBallEnter = false;

    // Start is called before the first frame update
    void Start()
    {
        Random.InitState(Mathf.RoundToInt(Time.time));

        playerTurn = (PlayerTurn)Random.Range((int)PlayerTurn.Jugador1, (int)PlayerTurn.Jugador2 + 1);

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
            case PlayerTurn.Jugador1:
                playerTurn = PlayerTurn.Jugador2;
                ballContainerP1.SetListenPhysicWorld(false);
                ballContainerP2.SetListenPhysicWorld(true);
                break;
            case PlayerTurn.Jugador2:
                playerTurn = PlayerTurn.Jugador1;
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

    public void ResultMatch(string text)
    {
        resultMatchText.text = text;
        resultMatchText.gameObject.SetActive(true);
        buttonContainer.gameObject.SetActive(true);
    }

}

public enum PlayerTurn { Jugador1, Jugador2 };
