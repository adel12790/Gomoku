using GameSparks.Api.Messages;
using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour {

    [SerializeField]
    private Button playButton;

    void Start()
    {
        playButton.onClick.AddListener(Play);
        MatchNotFoundMessage.Listener += OnMatchNotFound;
        ChallengeStartedMessage.Listener += OnChallengeStarted;
    }

    void OnDestroy()
    {
        ChallengeStartedMessage.Listener -= OnChallengeStarted;
    }

    private void OnChallengeStarted(ChallengeStartedMessage obj)
    {
        LoadingManager.Instance.LoadNextScene();
        Debug.Log(obj.JSONString);
    }

    private void OnMatchNotFound(MatchNotFoundMessage obj)
    {
        UnblockInput();
        Debug.Log(obj.JSONString);

    }

    private void Play()
    {
        BlockInput();
        MatchmakingRequest request = new MatchmakingRequest();
        request.SetMatchShortCode("QUIZ_BATTLE");
        request.SetSkill(1);
        request.Send(OnMatchmakingSuccess, OnMatchmakingError);
        Debug.Log("Match Making Request: " + request.JSONString);
    }

    private void OnMatchmakingSuccess(MatchmakingResponse response)
    {
        Debug.Log(response.JSONString);

    }

    private void OnMatchmakingError(MatchmakingResponse response)
    {
        UnblockInput();
        Debug.Log("Matching error response: " + response.JSONString);

    }

    private void BlockInput()
    {
        playButton.interactable = false;
    }
    private void UnblockInput()
    {
        playButton.interactable = true;
    }
}
