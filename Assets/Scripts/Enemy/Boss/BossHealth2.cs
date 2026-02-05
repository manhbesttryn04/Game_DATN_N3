using UnityEngine;
using System.Collections;

public class BossHealth2 : EnemyHealth 
{
    [Header("Cài đặt Phase Laser")]
    public GameObject laserPrefab;    
    public Transform laserSpawnPoint; 
    private bool laserTriggered = false;
    private bool isFiringLaser = false;

    // Sử dụng override để ghi đè hàm Update đã chuyển sang virtual ở lớp cha
    protected override void Update()
    {
        // Gọi logic SaveLoad và Slider của lớp cha (Bây giờ đã hết lỗi)
        base.Update(); 

        // Logic Phase Laser khi máu dưới 50%
        if (!Die && !laserTriggered && _Enemy.Health.Current <= (_Enemy.Health.Max / 2))
        {
            StartCoroutine(LaserPhaseRoutine());
        }
    }

    IEnumerator LaserPhaseRoutine()
    {
        laserTriggered = true;
        isFiringLaser = true;

        Debug.Log("Boss: Đang vận công bắn Laser...");
        yield return new WaitForSeconds(1f); 

        if (laserPrefab != null && laserSpawnPoint != null)
        {
            GameObject laser = Instantiate(laserPrefab, laserSpawnPoint.position, laserSpawnPoint.rotation);
            laser.transform.parent = this.transform; 
            Destroy(laser, 2.5f); 
        }

        yield return new WaitForSeconds(2.5f);
        isFiringLaser = false;
    }

    public override void Dead()
    {
        // Không cho phép chết khi đang bắn laser để tránh lỗi logic
        if (isFiringLaser) return;

        base.Dead();
    }
}