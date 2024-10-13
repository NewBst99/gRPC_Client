using System.Collections;
using System.Collections.Generic;
using grpc_client;
using UnityEngine;

public class SaveLoadManager : MonoBehaviour
{
    private StubClient stubclient;
    private string host = "localhost";
    private int port = 9090;

    // Start is called before the first frame update
    void Start()
    {
        stubclient = new StubClient(host, port);
    }

    // For Save Button
    public void OnSaveButtonClick()
    {
        stubclient.saveUserInfo(1, "user1", 10, "2024-10-13");
        stubclient.saveItemRelation(1, "c100", 1);
        stubclient.saveItemRelation(1, "c101", 3);
        stubclient.saveMapProgress(1, 0, -40, -8, 0);
    }

    // For Load Button
    public void OnLoadButtonClick()
    {
        stubclient.getUserInfo(1);
    }

    private async void OnDestroy()
    {
        // SaveLoadManager가 파괴될 때 StubClient 종료
        if (stubclient != null)
        {
            await stubclient.ShutdownAsync();  // 비동기 메서드 호출
        }
    }
}
