using UnityEngine;

public class StateManager : MonoBehaviour
{
    public static StateManager Instance;

    private string PlayerName;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // LoadPlayerName()
    }

    public void SetPlayerName(string name) {
        PlayerName = name;
    }

    public string GetPlayerName() {
        return PlayerName;
    }


}
