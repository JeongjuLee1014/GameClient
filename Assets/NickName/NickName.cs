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

    private void OnInputEndEdit(string text)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine(SetNickName(text));
        }
    }

    public IEnumerator SetNickName(string nickName)
    {
        // ������ ����
        yield return StartCoroutine(SendPutUserRequest(nickName));
        SceneManager.LoadScene("Home");
    }

    public IEnumerator SendPutUserRequest(string nickName)
    {
        LoginValues loginValues = LoginValues.Get();
        
        string sessionId = loginValues.sessionId;

        // PUT ��û�� ���� URL
        string url = $"{Constant.SERVER_URL}/api/users/session/{sessionId}";

        // ���� User �����͸� ������Ʈ
        User.Instance.UpdateUserData(
            User.Instance.id,
            nickName,
            sessionId,
            User.Instance.numCoins,
            User.Instance.numStars,
            User.Instance.numEnergies
        );

        // JSON �����ͷ� ��ȯ
        string jsonData = JsonUtility.ToJson(User.Instance);

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // ������ ��û�� ������ ������ ��ٸ��ϴ�.
            yield return request.SendWebRequest();

            // ������ ó���մϴ�.
            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("����� �г����� ���������� ������Ʈ�Ǿ����ϴ�.");
            }
            else
            {
                Debug.LogError("���� ��û�� �����߽��ϴ�: " + request.error);
            }
        }
    }
}