using UnityEngine;
using UnityEngine.SceneManagement;

public class GoogleLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = LoginValues.CreateSessionId();
        
        LoginValues loginValues = LoginValues.Get();
        loginValues.sessionId = sessionId;
        LoginValues.Set(loginValues);

        string url = Constants.SERVER_URL + "/api/login/google?sessionId=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete 씬에서 로그인을 완료하게 한다
        SceneManager.LoadScene("LoginComplete");
    }
}