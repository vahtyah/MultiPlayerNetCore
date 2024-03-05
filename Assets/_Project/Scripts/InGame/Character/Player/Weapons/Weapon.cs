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
        SpawnBulletServerRPC();
    }

    [ServerRpc]
    void SpawnBulletServerRPC()
    {
        var bulletObj = Instantiate(bulletPrefab);
        bulletObj.GetComponent<Projectile>().SetPosition(shootPos.position).LookAt(InGameManager.Instance.GetReticle().position);
        bulletObj.GetComponent<NetworkObject>().Spawn();
    }
}