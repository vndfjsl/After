using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class TestManager : MonoBehaviour
{
    [SerializeField] string joinCode;

    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 300));

        if(!NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        {
            StartButtons();
        }
        else
        {
            StatusLabel();
        }

        GUILayout.EndArea();
    }

    static string ipAddress = "";

    public async void StartButtons()
    {
        ipAddress = GUILayout.TextField(ipAddress, 20);

        if(GUILayout.Button("Host"))
        {
            if(RelayManager.Instance.IsRelayEnabled) await RelayManager.Instance.SetUpRelay();

            NetworkManager.Singleton.StartHost();
        }
        if (GUILayout.Button("Client"))
        {
            if (RelayManager.Instance.IsRelayEnabled) await RelayManager.Instance.JoinRelay(joinCode);
            NetworkManager.Singleton.StartClient();
        }
        if (GUILayout.Button("Server")) NetworkManager.Singleton.StartServer();
    }

    static void StatusLabel()
    {
        var mode = NetworkManager.Singleton.IsHost ? "Host" : (NetworkManager.Singleton.IsServer ? "Server" : "Client");
        GUILayout.Label("mode:" + mode);
    }
}
