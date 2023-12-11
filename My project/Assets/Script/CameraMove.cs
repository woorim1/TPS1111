using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform obFollow; //따라갈 물체의 위치정보
    public float FollowSpeed = 10; //따라갈 속도
    public float sensitivity = 100; //마우스 감도
    public float clampAngle = 70; //최대 각도

    private float rotX; //인풋받을 변수
    private float rotY;

    public Transform realCamera; //카메라 정보
    public Vector3 dirNomalized; //방향
    public Vector3 finalDir; //최종값

    public float minDistance; //최소 거리
    public float maxDistance; //최대 거리
    public float finalDistance; //최종 거리

    public float smoothness = 10; //부드러움

    public GameObject playerObj;

    private void Start()
    {
        rotX = transform.localRotation.eulerAngles.x;
        rotY = transform.localRotation.eulerAngles.y;

        dirNomalized = new Vector3(0.2f, 0.1f, -0.9f);
        finalDistance = realCamera.localPosition.magnitude;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        rotX += -(Input.GetAxis("Mouse Y")) * sensitivity * Time.deltaTime;
        rotY += (Input.GetAxis("Mouse X")) * sensitivity * Time.deltaTime;

        rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

        Quaternion rot = Quaternion.Euler(rotX, rotY, 0);

        transform.rotation = rot;


        playerObj.transform.localRotation = Quaternion.Euler(0, rotY, 0);
    }

    private void LateUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, obFollow.position, FollowSpeed * Time.deltaTime);

        finalDir = transform.TransformPoint(dirNomalized * maxDistance);

        RaycastHit hit;

        if(Physics.Linecast(transform.position,finalDir,out hit))
        {
            finalDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
        }
        else
        {
            finalDistance = maxDistance;
        }
        realCamera.localPosition = Vector3.Lerp(realCamera.localPosition, dirNomalized * finalDistance, Time.deltaTime * smoothness);
    }
}
