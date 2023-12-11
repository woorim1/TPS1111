using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public Transform obFollow; //���� ��ü�� ��ġ����
    public float FollowSpeed = 10; //���� �ӵ�
    public float sensitivity = 100; //���콺 ����
    public float clampAngle = 70; //�ִ� ����

    private float rotX; //��ǲ���� ����
    private float rotY;

    public Transform realCamera; //ī�޶� ����
    public Vector3 dirNomalized; //����
    public Vector3 finalDir; //������

    public float minDistance; //�ּ� �Ÿ�
    public float maxDistance; //�ִ� �Ÿ�
    public float finalDistance; //���� �Ÿ�

    public float smoothness = 10; //�ε巯��

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
