using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NaverLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = LoginValues.CreateSessionId();

        LoginValues loginValues = LoginValues.Get();
        loginValues.sessionId = sessionId;
        LoginValues.Set(loginValues);

        //string url = Constant.SERVER_URL + "/api/login/naver?sessionId=" + sessionId;
        string url = $"{Constant.SERVER_URL}/api/login/naver?session_id={Uri.EscapeDataString(sessionId)}";

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }
}