using Unity.Netcode;
using UnityEngine;

public class Weapon : NetworkBehaviour, IWeapon
{
    [SerializeField] private WeaponData weaponData;
    [SerializeField] private Transform shootPos;
    [SerializeField] private ParticleSystem muzzle;
    [SerializeField] private GameObject bulletPrefab;

    CountdownTimer cooldownTimer;

    private void Start()
    {
        cooldownTimer = new CountdownTimer(weaponData.Cooldown);
        cooldownTimer.Start();
    }

    public bool CanShoot()
    {
        cooldownTimer.Tick(Time.deltaTime);
        return cooldownTimer.IsFinished && InputManager.NormalAttack;
    }
    
    public void Shoot()
    {
        muzzle.Play();
        cooldownTimer.Reset();
        if (IsServer)
        {
            SpawnBullet();
        }
        else
        {
            ShootServerRpc();
        }
    }
    
    [ServerRpc]
    void ShootServerRpc()
    {
        SpawnBullet();
    }
    
    private void SpawnBullet()
    {
        var bulletObj = Instantiate(bulletPrefab, shootPos.position, Quaternion.identity);
        bulletObj.GetComponent<Projectile>().LookAt(InGameManager.Instance.GetReticle().position);
        bulletObj.GetComponent<NetworkObject>().Spawn();
    }
}