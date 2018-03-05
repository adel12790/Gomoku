using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ChallengeManager : Singleton<ChallengeManager> {

    public UnityEvent ChallengeStarted;
    public UnityEvent ChallengeTurnTaken;
    public UnityEvent ChallengeWon;
    public UnityEvent ChallengeLost;

    private string challengeID;

    void Start()
    {
        ChallengeStartedMessage.Listener += OnChallengeStarted;
        ChallengeTurnTakenMessage.Listener += OnChallengeTurnTaken;
        ChallengeWonMessage.Listener += OnChallengeWon;
        ChallengeLostMessage.Listener += OnChallengeLost;
    }

    public bool IsChallengeActive { get; private set; }

    public string HeartsPlayerName { get; private set; }

    public string HeartsPlayerId { get; private set; }

    public string SkullsPlayerName { get; private set; }

    public string SkullsPlayerId { get; private set; }

    public string CurrentPlayerName { get; private set; }

    public PieceType CurrentPlayerPieceType
    {
        get
        {
            if (IsChallengeActive)
            {
                return CurrentPlayerName == HeartsPlayerName ? PieceType.Heart : PieceType.Skull;
            }
            else
            {
                return PieceType.Empty;
            }
        }
    }

    public PieceType[] Fields { get; private set; }

    private void OnChallengeStarted(ChallengeStartedMessage message)
    {
        IsChallengeActive = true;
        challengeID = message.Challenge.ChallengeId;
        HeartsPlayerName = message.Challenge.Challenger.Name;
        HeartsPlayerId = message.Challenge.Challenger.Id;
        SkullsPlayerName = message.Challenge.Challenged.First().Name;
        SkullsPlayerId = message.Challenge.Challenged.First().Id;
        CurrentPlayerName = message.Challenge.NextPlayer == HeartsPlayerId ? HeartsPlayerName : SkullsPlayerName;
        Fields = message.Challenge.ScriptData.GetIntList("fields").Cast<PieceType>().ToArray();
        ChallengeStarted.Invoke();
        Debug.Log("challenge ID: " + challengeID);
        Debug.Log("Message " + message.JSONString);

    }

    private void OnChallengeTurnTaken(ChallengeTurnTakenMessage message)
    {
        CurrentPlayerName = message.Challenge.NextPlayer == HeartsPlayerId ? HeartsPlayerName : SkullsPlayerName;
        Fields = message.Challenge.ScriptData.GetIntList("fields").Cast<PieceType>().ToArray();
        ChallengeTurnTaken.Invoke();
        Debug.Log("Message " + message.JSONString);

    }

    private void OnChallengeWon(ChallengeWonMessage message)
    {
        IsChallengeActive = false;
        ChallengeWon.Invoke();
        Debug.Log("Message " + message.JSONString);

    }

    private void OnChallengeLost(ChallengeLostMessage message)
    {
        IsChallengeActive = false;
        ChallengeLost.Invoke();
        Debug.Log("Message " + message.JSONString);

    }

    public void Move(int x, int y)
    {
        LogChallengeEventRequest request = new LogChallengeEventRequest();
        request.SetChallengeInstanceId(challengeID);
        request.SetEventKey("Move");
        request.SetEventAttribute("X", x);
        request.SetEventAttribute("Y", y);
        request.Send(OnMoveSuccess, OnMoveError);
        Debug.Log("Move Request: " + request.JSONString);

    }

    private void OnMoveSuccess(LogChallengeEventResponse response)
    {
        print(response.JSONString);
    }

    private void OnMoveError(LogChallengeEventResponse response)
    {
        print(response.Errors.JSON.ToString());
    }
}
