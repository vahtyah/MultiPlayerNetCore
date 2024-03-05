using System;
using Sirenix.OdinInspector;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    [BoxGroup("Components")]
    [SerializeField]
    private Rigidbody rb;

    [BoxGroup("Components")]
    [SerializeField]
    private Animator anim;

    [BoxGroup("Components")]
    [SerializeField]
    private LayerMask groundMask;

    [BoxGroup("Weapon Settings")]
    [SerializeField]
    private Transform rightHand;

    [BoxGroup("Weapon Settings")]
    [SerializeField]
    private Weapon weapon;
    

    public PlayerAnimationComponent Animation { get; private set; }
    public PlayerMovementComponent Movement { get; private set; }
    public PlayerStateComponent State { get; private set; }
    public PlayerWeaponComponent Weapon { get; private set; }
    public bool IsDead { get; set; }

    private void Awake()
    {
        Animation = new PlayerAnimationComponent(anim);
        Movement = new PlayerMovementComponent(this);
        State = new PlayerStateComponent(this);
        Weapon = new PlayerWeaponComponent(this);
        Weapon.SetWeapon(weapon);
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (IsOwner)
            State.Update();
    }

    private void FixedUpdate()
    {
        if (IsOwner)
            State.FixedUpdate();
    }

    public Rigidbody GetRb() => rb;
    public LayerMask GetGroundMask() => groundMask;
    public Transform GetRightHand() => rightHand;
}