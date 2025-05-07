using UnityEngine;
using Unity.Netcode;
using UnityEngine.XR;
using System.Collections.Generic;

public class AnchorInteractor : NetworkBehaviour
{
    public NetworkVariable<int> selectedAnchorIndex = new NetworkVariable<int>(
        -1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    private UnityEngine.XR.InputDevice rightHand;

    void Start()
    {
        var inputDevices = new List<UnityEngine.XR.InputDevice>();
        InputDevices.GetDevicesAtXRNode(XRNode.RightHand, inputDevices);
        if (inputDevices.Count > 0)
            rightHand = inputDevices[0];
    }

    private void Update()
    {
        if (!IsOwner || !rightHand.isValid) return;

        bool triggerPressed = false;
        if (rightHand.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
        {
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, 5f))
            {
                var anchor = hit.collider.GetComponent<SpawnAnchor>();
                if (anchor != null)
                {
                    SetSpawnPointServerRpc(anchor.anchorIndex);
                }
            }
        }
    }

    [ServerRpc]
    void SetSpawnPointServerRpc(int index)
    {
        selectedAnchorIndex.Value = index;
    }
}
