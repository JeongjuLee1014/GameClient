using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class User
{
    // 사용자 정보 속성
    //public string id; // id는 삭제
    public string nickName;
    public string sessionId;
    public int numCoins;
    public int numStars;
    public int numEnergies;

    // Singleton 인스턴스
    private static User instance;

    // Singleton 인스턴스를 반환
    public static User Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new User();
            }
            return instance;
        }
    }

    
    private User()
    {
        Reset();
    }

    // User 인스턴스 초기화 (데이터 리셋)
    public void Reset()
    {
        UpdateUserData(string.Empty, string.Empty, 0, 0, 0);
    }

    // 사용자 데이터를 업데이트하는 메서드
    public void UpdateUserData(string nickName, string sessionId, int numCoins, int numEnergies, int numStars )
    {
        Debug.Log($"Updating User Data: NickName={nickName}, SessionId={sessionId}, NumCoins={numCoins}, NumEnergies={numEnergies}, NumStars={numStars}");
        //this.id = id;
        this.nickName = nickName;
        this.sessionId = sessionId;
        this.numCoins = numCoins;
        this.numEnergies = numEnergies;
        this.numStars = numStars;
        
    }

    // GET 요청: 사용자 데이터 가져오기
    //System.Action<bool> callback : 요청 성공 여부를 콜백으로 전달하여 후속 처리를 유연하게 구현.
    public IEnumerator GetUser(string serverUrl, string sessionId, System.Action<bool> callback)
    {
        string url = $"{serverUrl}/api/users/session/{sessionId}";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log($"Response data: {request.downloadHandler.text}");
                User loadedUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                UpdateUserData(
                    loadedUser.nickName,
                    loadedUser.sessionId,
                    loadedUser.numCoins,
                    loadedUser.numStars,
                    loadedUser.numEnergies
                );
                callback(true);
            }
            else
            {
                Debug.LogError($"Failed to get user data: {request.error}");
                callback(false);
            }
        }
    }

    // PUT 요청: 사용자 데이터 업데이트
    public IEnumerator PutUser(string serverUrl, User newData, System.Action<bool> callback)
    {
        string url = $"{serverUrl}/api/users/session/{newData.sessionId}";
        string jsonData = JsonUtility.ToJson(newData);

        using (UnityWebRequest request = new UnityWebRequest(url, "PUT"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("User data successfully updated.");
                UpdateUserData(
                    newData.nickName,
                    newData.sessionId,
                    newData.numCoins,
                    newData.numStars,
                    newData.numEnergies
                );
                callback(true);
            }
            else
            {
                Debug.LogError($"Failed to update user data: {request.error}");
                callback(false);
            }
        }
    }
}
