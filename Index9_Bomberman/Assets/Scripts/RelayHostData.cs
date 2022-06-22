using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RelayHostData
{
    public string IPv4Address;
    public ushort port; // 0~65535
    public Guid AllocationID;
    public byte[] AllocationIDBytes;
    public byte[] connectData;
    public byte[] key;
    public string joinCode; // 입장 코드
}
