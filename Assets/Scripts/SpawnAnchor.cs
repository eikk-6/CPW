using UnityEngine;
using Unity.Netcode;
using System.Collections.Generic;

public class SpawnAnchor : NetworkBehaviour
{
    public int anchorIndex;

    public static List<SpawnAnchor> AllAnchors = new List<SpawnAnchor>();

    private void Awake()
    {
        AllAnchors.Add(this);
    }

    private void OnDestroy()
    {
        AllAnchors.Remove(this);
    }
}