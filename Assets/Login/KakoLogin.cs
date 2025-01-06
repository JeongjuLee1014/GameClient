using UnityEngine;
using UnityEngine.SceneManagement;

public class KakaoLogin : MonoBehaviour
{
    private void OnMouseDown()
    {
        string sessionId = LoginValues.GetSessionId();
        string url = "https://localhost:7032/api/login/kakao?session_id=" + sessionId;

        Application.OpenURL(url);

        // LoginComplete ������ �α����� �Ϸ��ϰ� �Ѵ�
        SceneManager.LoadScene("LoginComplete");
    }
}