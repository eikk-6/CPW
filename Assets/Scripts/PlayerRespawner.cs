using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class PlayerRespawner : NetworkBehaviour
{
    public AnchorInteractor anchorInteractor;

    public void Respawn()
    {
        if (!IsOwner || anchorInteractor == null) return;

        int anchorIndex = anchorInteractor.selectedAnchorIndex.Value;

        if (anchorIndex < 0) return;

        var targetAnchor = SpawnAnchor.AllAnchors
            .FirstOrDefault(a => a.anchorIndex == anchorIndex);

        if (targetAnchor != null)
        {
            transform.position = targetAnchor.transform.position;
            transform.rotation = targetAnchor.transform.rotation;
        }
        else
        {
            Debug.LogWarning("Spawn anchor not found!");
        }
    }
}
