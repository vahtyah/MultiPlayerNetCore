using System;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] Vector3 distance;
    // [SerializeField] private Transform playerTransform;
    //[SerializeField] float smoothTime;
    //Vector3 curVelo;
    Vector3 offset;

    private Transform playerPos;
    private bool isPlayerPosSet = false;

    private void Start()
    {
        offset = transform.position - playerPos.position;
    }

    private void LateUpdate()
    {
        if(!isPlayerPosSet) return;
        Vector3 targetPos = playerPos.position + offset;
        transform.position = targetPos;
        //transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref curVelo, smoothTime, Mathf.Infinity, Time.deltaTime) ;
    }
}