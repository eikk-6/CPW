using UnityEngine;

public class BatteryHealth : MonoBehaviour
{
    public int batteryHp = 5;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Weapon"))
        {
            batteryHp -= 1;
            Debug.Log("Battery hit! Current HP: " + batteryHp);

            WeaponController weapon = other.GetComponent<WeaponController>();
            if (weapon != null)
            {
                weapon.PushBack(transform.position); //무기 사용 플레이어 뒤로 밀려남
            }

            // 체력이 0 이하가 되면 오브젝트 제거
            if (batteryHp <= 0)
            {
                Debug.Log("배터리 체력 0! 오브젝트 제거");

                // 배터리가 파괴되면 플레이어 게임 종료
                PlayerRespawner respawner = GameObject.FindWithTag("Player")?.GetComponent<PlayerRespawner>();
                if (respawner != null && respawner.IsOwner)
                {
                    respawner.EndGameServerRpc();
                }

                Destroy(gameObject);
            }
        }
    }
}
