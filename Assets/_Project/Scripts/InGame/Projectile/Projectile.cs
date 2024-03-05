using System;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private float speed;
    private CountdownTimer countdownTimer;
    protected ProjectileTypes projectileType;

    protected void Awake()
    {
        countdownTimer = new CountdownTimer(3f);
        countdownTimer.Start();
    }

    private void Update()
    {
        if(!IsServer) return;
        transform.Translate(Vector3.forward * (speed * Time.deltaTime));
        countdownTimer.Tick(Time.deltaTime);
        if (countdownTimer.IsFinished && IsOwner)
        {
            RequestDespawnServerRpc();
        }
    }

    [ServerRpc]
    private void RequestDespawnServerRpc()
    {
        GetComponent<NetworkObject>().Despawn(destroy: true);
    }
    
    public Projectile SetPosition(Vector3 position)
    {
        transform.position = position;
        return this;
    }
    
    public Projectile LookAt(Vector3 position)
    {
        transform.LookAt(position);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        return this;
    }

    public ProjectileTypes GetProjectileType() =>
        projectileType;
}