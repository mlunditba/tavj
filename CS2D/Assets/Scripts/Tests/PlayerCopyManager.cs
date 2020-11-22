﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCopyManager : MonoBehaviour
{
    private CharacterController characterController;
    
    private int? respawnSnapshotSeq;
    private Vector3? respawnPosition;
    
    public Animator animator;
    private const float Epsilon = 0.0001f;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        //animator.SetBool("Shooting", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MovePlayerCopy(Vector3 position, Quaternion rotation)
    {
        var transf = transform;
        //Debug.Log($"{transf.position.x} {transf.position.y} {transf.position.z} {position.x} {position.y} {position.z}");
        var delta = transf.position - position;
        SetAnimatorMovementParameters(delta);
        characterController.Move(-delta);
        //transf.position = position;
        transf.rotation = rotation;
    }

    public void SetAnimatorMovementParameters(Vector3 delta)
    {
        var deltaX = delta.x;
        var deltaZ = delta.z;
        //Debug.Log(deltaX + " " + deltaZ);

        if(deltaX > Epsilon) animator.SetFloat("Horizontal Movement", 1);
        else if(deltaX < -Epsilon) animator.SetFloat("Horizontal Movement", -1);
        else animator.SetFloat("Horizontal Movement", 0);
        
        if(deltaZ > Epsilon) animator.SetFloat("Vertical Movement", 1);
        else if(deltaZ < -Epsilon) animator.SetFloat("Vertical Movement", -1);
        else animator.SetFloat("Vertical Movement", 0);
    }

    public void TriggerDeathAnimation()
    {
        animator.SetTrigger("Dying");
    }

    public void TriggerRespawnAnimation()
    {
        animator.SetTrigger("Respawn");
    }

    public void MovePlayerCopyDirect(Vector3 newPosition)
    {
        characterController.Move(newPosition - transform.position);
    }

    public int? RespawnSnapshotSeq
    {
        get => respawnSnapshotSeq;
        set => respawnSnapshotSeq = value;
    }

    public Vector3? RespawnPosition
    {
        get => respawnPosition;
        set => respawnPosition = value;
    }
}