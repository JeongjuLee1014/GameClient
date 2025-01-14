using UnityEngine;

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

    //// 사용자 정보 속성
    //public string Id { get; private set; }
    //public string NickName { get; private set; }
    //public string SessionId { get; private set; }

    //private int numCoins;
    //private int numStars;
    //private int numEnergies;

    //public int NumCoins
    //{
    //    get => numCoins;
    //    set => numCoins = Mathf.Max(0, value); // 음수 방지
    //}

    //public int NumStars
    //{
    //    get => numStars;
    //    set => numStars = Mathf.Max(0, value); // 음수 방지
    //}

    //public int NumEnergies
    //{
    //    get => numEnergies;
    //    set => numEnergies = Mathf.Max(0, value); // 음수 방지
    //}

    // private 생성자: 외부에서 인스턴스화하지 못하도록 제한
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
}
