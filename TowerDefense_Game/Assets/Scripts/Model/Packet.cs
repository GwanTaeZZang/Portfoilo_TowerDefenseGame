using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketBase
{
    public string packet = "";
}

public class ReqMapData : PacketBase
{
    public int id;
}

public class ResMapData : PacketBase
{
    public string result;
    public MapData data;
}