﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5.0f;

    [SerializeField]
    private Animator playerAnim;

    public List<Animal> followTrail = new List<Animal>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    private void HandleMovement() {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        if (ShouldIFlip(moveX)) {
            FlipMe();
        }

        Vector3 moveDir = new Vector3(moveX, 0.0f, moveY) * moveSpeed;

        transform.Translate(moveDir * Time.deltaTime);
    }

    public void AcceptFollower(Animal follower) {
        Debug.Log("adding follower");
        followTrail.Add(follower);
    }

    public void MakeChain() {
        for (var i = 0; i < followTrail.Count; i++) {
            var follower = followTrail[i];
            var followerSmooth = follower.mySmooth;

            if (i == 0) {
                followerSmooth._target = gameObject.transform;
            } else {
                followerSmooth._target = followTrail[i-1].transform;
            }
        }
    }

    private bool ShouldIFlip(float moveX) {
        if (moveX > 0) {
            return transform.localScale.x == -1;
        } else if (moveX < 0) {
            return transform.localScale.x == 1;
        } else {
            return false;
        }
    }

    private void FlipMe() {
        var scale = transform.localScale;
        transform.localScale = new Vector3(scale.x * -1, scale.y, scale.z); 
    }
}
