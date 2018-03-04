using GameSparks.Api.Requests;
using GameSparks.Api.Responses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {

    [SerializeField]
    private InputField userNameInput;
    [SerializeField]
    private InputField passwordInput;
    [SerializeField]
    private Button loginButton;
    [SerializeField]
    private Button registerButton;
    [SerializeField]
    private Text errorMessageText;

    void Awake()
    {
        loginButton.onClick.AddListener(Login);
        registerButton.onClick.AddListener(Register);
    }

    private void Login()
    {
        BlockInput();
        AuthenticationRequest request = new AuthenticationRequest();
        request.SetUserName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnLoginSuccess, OnLoginError);
    }

    private void OnLoginSuccess(AuthenticationResponse response)
    {
        if(!response.HasErrors)
            LoadingManager.Instance.LoadNextScene();
        Debug.Log(response.JSONString);
    }

    private void OnLoginError(AuthenticationResponse response)
    {
        UnblockInput();
        errorMessageText.text = response.Errors.JSON.ToString();
        Debug.Log(response.JSONString);

    }

    private void Register()
    {
        BlockInput();
        RegistrationRequest request = new RegistrationRequest();
        request.SetUserName(userNameInput.text);
        request.SetDisplayName(userNameInput.text);
        request.SetPassword(passwordInput.text);
        request.Send(OnRegistrationSuccess, OnRegistrationError);
    }

    private void OnRegistrationSuccess(RegistrationResponse response)
    {
        Login();
        Debug.Log(response.JSONString);

    }

    private void OnRegistrationError(RegistrationResponse response)
    {
        UnblockInput();
        errorMessageText.text = response.Errors.JSON.ToString();
        Debug.Log(response.JSONString);

    }

    private void BlockInput()
    {
        userNameInput.interactable = false;
        passwordInput.interactable = false;
        loginButton.interactable = false;
        registerButton.interactable = false;
    }

    private void UnblockInput()
    {
        userNameInput.interactable = true;
        passwordInput.interactable = true;
        loginButton.interactable = true;
        registerButton.interactable = true;
    }
}
