using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLossPanel : MonoBehaviour {

    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private Text winText;
    [SerializeField]
    private Text lossText;
    [SerializeField]
    private Button backButton;

    void Awake()
    {
        ChallengeManager.Instance.ChallengeWon.AddListener(OnChallengeWon);
        ChallengeManager.Instance.ChallengeLost.AddListener(OnChallengeLost);
        backButton.onClick.AddListener(OnBackButtonClick);
        Hide();
    }

    private void Show()
    {
        content.gameObject.SetActive(true);
    }

    private void Hide()
    {
        content.gameObject.SetActive(false);
    }

    private void OnChallengeWon()
    {
        winText.enabled = true;
        lossText.enabled = false;
        Show();
    }

    private void OnChallengeLost()
    {
        winText.enabled = false;
        lossText.enabled = true;
        Show();
    }

    private void OnBackButtonClick()
    {
        LoadingManager.Instance.LoadPreviousScene();
    }
}
