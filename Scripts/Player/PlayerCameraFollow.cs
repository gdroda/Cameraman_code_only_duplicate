using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCameraFollow : MonoBehaviour
{
    private Func<Vector3> GetCameraFollowPositionFunc;
    [SerializeField] private float cameraMoveSpeed;
    [SerializeField] private float cameraPlayerDistance;
    Vector2 mouse;

    private void Start()
    {
        Vector3 pos = GameManager.instance.GetPlayerTransform().position;
        pos.z = transform.position.z;
        transform.position = pos;
    }

    public void Setup(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    // v is this needed? v
    public void SetGetCameraFollowPositionFunc(Func<Vector3> GetCameraFollowPositionFunc)
    {
        this.GetCameraFollowPositionFunc = GetCameraFollowPositionFunc;
    }

    void Update()
    {
        //This is practically player.transform
        Vector3 cameraFollowPosition = GetCameraFollowPositionFunc();
        cameraFollowPosition.z = transform.position.z;

        //Mouse position and direction
        var mousePos = Camera.main.ScreenToWorldPoint(mouse);
        mousePos.z = 0f;
        Vector3 aimDirection = (mousePos - cameraFollowPosition).normalized;
        aimDirection.z = 0f;

        //HERE WE KIND OF WANT TO TURN THE 0.XX VALUES OF AIMDIRECTION TO -1s AND 1s SO WE CAN MULTIPLY WITH LIKE 0.4f AND GET CONSTANT DISTANCE
        //INSTEAD WE'RE CLAMPING AT 0.4F FOR NOW WHICH MEANS CAMERA WILL MOVE UP TO 0.4F DISTANCE MOUSE-PLAYER, PAST THAT WILL BE IGNORED

        //Adding distance towards look direction
        //cameraFollowPosition = cameraFollowPosition + aimDirection * cameraPlayerDistance;

        cameraFollowPosition = cameraFollowPosition + Vector3.ClampMagnitude(aimDirection, 0.4f) * cameraPlayerDistance;

        Vector3 cameraMoveDir = (cameraFollowPosition - transform.position).normalized;
        float distance = Vector3.Distance(cameraFollowPosition, transform.position);

        if (distance > 0)
        {
            Vector3 newCameraPosition =  transform.position + cameraMoveDir * distance * cameraMoveSpeed * Time.deltaTime;

            float distanceAfterMoving = Vector3.Distance(newCameraPosition, cameraFollowPosition);

            if (distanceAfterMoving > distance)
            {
                //Overshot the target
                newCameraPosition = cameraFollowPosition;
            }

            transform.position = newCameraPosition;
        }
    }

    private void OnLook(InputValue value)
    {
        mouse = value.Get<Vector2>();
    }
}
