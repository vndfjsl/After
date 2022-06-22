using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.Netcode;
using Unity.Services.Authentication;
using Unity.Services.Core;
using Unity.Services.Core.Environments;
using Unity.Services.Relay;
using Unity.Services.Relay.Models;
using UnityEngine;

public class RelayManager : MonoBehaviour
{
    public static RelayManager Instance { get; private set; }

    public bool IsRelayEnabled => Transport != NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

    [SerializeField] string _environment = "";

    const int _maxConnections = 10;
    

    public UnityTransport Transport => NetworkManager.Singleton.gameObject.GetComponent<UnityTransport>();

    private void Awake()
    {
        // if (Instance != null) return;
        Instance = this;
    }

    public async Task<RelayHostData> SetUpRelay()
    {
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(_environment); // 환경변수
        await UnityServices.InitializeAsync(options);

        if(!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        Allocation allocation = await Relay.Instance.CreateAllocationAsync(_maxConnections);

        RelayHostData relayHostData = new RelayHostData()
        {
            key = allocation.Key,
            port = (ushort)allocation.RelayServer.Port,
            AllocationID = allocation.AllocationId,
            AllocationIDBytes = allocation.AllocationIdBytes,
            IPv4Address = allocation.RelayServer.IpV4,
            connectData = allocation.ConnectionData
        };

        relayHostData.joinCode = await Relay.Instance.GetJoinCodeAsync(relayHostData.AllocationID);

        Transport.SetRelayServerData(relayHostData.IPv4Address, relayHostData.port, relayHostData.AllocationIDBytes, relayHostData.key, relayHostData.connectData);

        return relayHostData;
    }

    public async Task<RelayJoinData> JoinRelay(string joinCode)
    {
        InitializationOptions options = new InitializationOptions().SetEnvironmentName(_environment); // 환경변수
        await UnityServices.InitializeAsync(options);

        if (!AuthenticationService.Instance.IsSignedIn)
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync();
        }

        JoinAllocation joinAllocation = await Relay.Instance.JoinAllocationAsync(joinCode);

        RelayJoinData data = new RelayJoinData()
        {
            key = joinAllocation.Key,
            port = (ushort)joinAllocation.RelayServer.Port,
            allocationID = joinAllocation.AllocationId,
            allocationIDByte = joinAllocation.AllocationIdBytes,
            ipv4Address = joinAllocation.RelayServer.IpV4,
            connectData = joinAllocation.ConnectionData,
            hostConnectData = joinAllocation.HostConnectionData,
            joinCode = joinCode
        };

        data.joinCode = await Relay.Instance.GetJoinCodeAsync(data.allocationID);

        Transport.SetRelayServerData(data.ipv4Address, data.port, data.allocationIDByte, data.key, data.connectData);

        return data;
    }
}
