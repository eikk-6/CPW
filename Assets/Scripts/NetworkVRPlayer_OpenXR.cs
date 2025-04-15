using UnityEngine;
using Fusion;
using UnityEngine.InputSystem;
using Unity.XR.CoreUtils;
using System.Collections; // 꼭 추가
using System;


public class NetworkVRPlayer_OpenXR : NetworkBehaviour
{
    [Header("Settings")]
    public InputActionReference moveAction;
    public Transform playerTransform;
    public float moveSpeed = 2.0f;

    private Camera cachedMainCamera;
    private bool cameraInitialized = false;

    public override void Spawned()
    {
        if (Object.HasInputAuthority)
        {
            Runner.SetPlayerObject(Object.InputAuthority, GetComponent<NetworkObject>());
        }

        StartCoroutine(SetupCamera());
    }

    private IEnumerator SetupCamera()
    {
        yield return new WaitForSeconds(0.2f); // 스폰 직후 살짝 대기

        cachedMainCamera = GetComponentInChildren<Camera>();

        if (cachedMainCamera != null)
        {
            cachedMainCamera.tag = "MainCamera"; // 강제 태그
            cameraInitialized = true;
            Debug.Log("[NetworkVRPlayer] MainCamera 연결 완료.");
        }
        else
        {
            Debug.LogError("[NetworkVRPlayer] MainCamera를 찾을 수 없습니다!");
        }
    }


    private void Update()
    {
        if (!Object.HasInputAuthority) return;
        if (!cameraInitialized) return;
        if (moveAction == null || playerTransform == null) return;

        Vector2 inputVector = moveAction.action.ReadValue<Vector2>();
        Vector3 moveDirection = new Vector3(inputVector.x, 0, inputVector.y);

        Vector3 forward = cachedMainCamera.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = cachedMainCamera.transform.right;
        right.y = 0;
        right.Normalize();

        Vector3 move = (forward * moveDirection.z + right * moveDirection.x) * moveSpeed * Time.deltaTime;
        playerTransform.position += move;
    }
}
