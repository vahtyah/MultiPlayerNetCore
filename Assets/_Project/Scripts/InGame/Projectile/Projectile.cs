using System;
using Unity.Netcode;
using UnityEngine;

public class Projectile : NetworkBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    private CountdownTimer countdownTimer;
    protected ProjectileTypes projectileType;

    protected void Awake()
    {
        rb.isKinematic = false;
        countdownTimer = new CountdownTimer(3f);
    }

    protected virtual void OnEnable()
    {
        countdownTimer.Reset();
    }

    protected virtual void Update()
    {
        rb.velocity = transform.forward * speed;
        countdownTimer.Tick(Time.deltaTime);
        if (countdownTimer.IsFinished)
        {
            RequestDespawn();
        }
    }
    
    private void RequestDespawn()
    {
        if (IsServer)
        {
            // Despawn the projectile directly if we're on the server
            Despawn();
        }
        else
        {
            // If we're a client, request the server to despawn the projectile
            RequestDespawnServerRpc();
        }
    }

    [ServerRpc]
    private void RequestDespawnServerRpc()
    {
        Despawn();
    }

    private void Despawn()
    {
        // Despawn the network object
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