using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NickName : MonoBehaviour
{
    public InputField inputField;

    void Start()
    {
        inputField.onEndEdit.AddListener(OnInputEndEdit);
    }

    //private void OnInputEndEdit(string text)
    //{
    //    if (Input.GetKeyDown(KeyCode.Return))
    //    {
    //        StartCoroutine(SetNickName(text));
    //    }
    //}
    private void OnInputEndEdit(string text)
    {
        if (!string.IsNullOrEmpty(text))
        {
            // 닉네임이 비어있지 않을 경우 바로 처리
            StartCoroutine(SetNickName(text));
        }
        else
        {
            Debug.LogWarning("닉네임이 비어 있습니다.");
        }
    }

    public IEnumerator SetNickName(string nickName)
    {
        // 서버로 전송
        yield return StartCoroutine(SendPutUserRequest(nickName));
        SceneManager.LoadScene("Home");
    }

    //public IEnumerator SendPutUserRequest(string nickName)
    //{
    //    LoginValues loginValues = LoginValues.Get();

    //    string sessionId = loginValues.sessionId;

    //    // PUT 요청을 보낼 URL
    //    string url = $"{Constant.SERVER_URL}/api/users/session/{sessionId}";

    //    // 현재 User 데이터를 업데이트
    //    User.Instance.UpdateUserData(
    //        //User.Instance.id,
    //        nickName,
    //        sessionId,
    //        User.Instance.numCoins,
    //        User.Instance.numStars,
    //        User.Instance.numEnergies
    //    );

    //    // JSON 데이터로 변환
    //    string jsonData = JsonUtility.ToJson(User.Instance);
    //    Debug.Log($"Request Data: {jsonData}");

    //    using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
    //    {
    //        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
    //        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
    //        request.downloadHandler = new DownloadHandlerBuffer();
    //        request.SetRequestHeader("Content-Type", "application/json");

    //        // 서버로 요청을 보내고 응답을 기다립니다.
    //        yield return request.SendWebRequest();

    //        // 응답을 처리합니다.
    //        if (request.result == UnityWebRequest.Result.Success)
    //        {
    //            Debug.Log("사용자 닉네임이 성공적으로 업데이트되었습니다.");
    //        }
    //        else
    //        {
    //            Debug.LogError("서버 요청에 실패했습니다: " + request.error);
    //        }
    //    }
    //}
    public IEnumerator SendPutUserRequest(string nickName)
    {
        LoginValues loginValues = LoginValues.Get();

        string sessionId = loginValues.sessionId;

        string serverUrl = Constant.SERVER_URL;

        User.Instance.UpdateUserData(
            nickName,
            sessionId,
            User.Instance.numCoins,
            User.Instance.numStars,
            User.Instance.numEnergies
        );

        yield return StartCoroutine(User.Instance.PutUser(serverUrl, User.Instance, success =>
        {
            if (success)
            {
                Debug.Log("Nickname updated successfully.");
            }
            else
            {
                Debug.LogError("Failed to update nickname.");
            }
        }));
    }
}