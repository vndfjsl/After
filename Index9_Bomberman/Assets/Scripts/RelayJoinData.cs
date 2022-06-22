using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RelayJoinData
{
    public string ipv4Address;
    public ushort port; // 0~65535
    public Guid allocationID;
    public byte[] allocationIDByte;
    public byte[] connectData;
    public byte[] hostConnectData;
    public byte[] key;
    public string joinCode; // 입장 코드
}
