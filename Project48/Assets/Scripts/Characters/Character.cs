using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]

public class Character : MonoBehaviour
{
    protected CharacterController cc;
    protected Animator animator;

    protected Vector3 movement;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected float angularSpeed;
    [SerializeField]
    protected float gravity;

    protected void Awake()
    {
        cc = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    protected void Update()
    {
        cc.Move(Time.deltaTime * speed * movement);
        ResolveVelocity();
        Rotate();
        Gravity();
    }

    protected void ResolveVelocity()
    {
        float moveSpeed = new Vector2(cc.velocity.x, cc.velocity.z).magnitude;
        animator.SetFloat("Move Speed", moveSpeed);
    }

    protected void Rotate()
    {
        Vector3 lookAt = new Vector3(movement.x, 0, movement.z);
        if (lookAt != Vector3.zero)
        {
            Quaternion curRotation = transform.rotation;
            Quaternion targetRotation = Quaternion.LookRotation(lookAt);
            transform.rotation = Quaternion.Slerp(curRotation, targetRotation, Time.deltaTime * angularSpeed);
        }
    }

    protected void Gravity()
    {
        if (cc.isGrounded)
            movement.y = -0.05f;
        else
            movement.y -= gravity;
    }
}
