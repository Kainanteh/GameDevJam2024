using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraController : MonoBehaviour
{

 /*   private const float MIN_FOLLOW_Y_OFFSET = 2f;
    private const float MAX_FOLLOW_Y_OFFSET = 15f;
*/
    public static CameraController Instance {

        get; private set;

    }

    [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

    private Vector3 targetFollowOffset;
    CinemachineTransposer cinemachineTransposer;

    public float MaxX;
    public float MinX;
    public float MaxY;
    public float MinY;
    public float MaxZ;
    public float MinZ;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
        targetFollowOffset = cinemachineTransposer.m_FollowOffset;
    }


    public float moveSpeed = 5f;

    void Update()
    {

        if (Nivel.Instance.Tutorial == true) { return; }

        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * moveSpeed * Time.deltaTime);

        // Movimiento vertical
        float verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalInput * moveSpeed * Time.deltaTime);

        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, MinX, MaxX);
        position.y = Mathf.Clamp(position.y, MinY, MaxY);
        position.z = Mathf.Clamp(position.y, MinZ, MaxZ);
        transform.position = position;
    }

    /*    private void Update()
{

    HandleMovement();
    HandleRotation();
    HandleZoom();

}*/


    /* private void HandleMovement()
     {

         Vector2 inputMoveDir = InputManager.Instance.GetCameraMoveVector();


         float moveSpeed = 10f;

         Vector3 moveVector = transform.forward * inputMoveDir.y + transform.right * inputMoveDir.x;
         transform.position += moveVector * moveSpeed * Time.deltaTime;


     }

     private void HandleRotation()
     {

         float rotationSpeed = 100f;

         Vector3 rotationVector = new Vector3(0, 0, 0);

         rotationVector.y = InputManager.Instance.GetCameraRotateAmount();


         transform.eulerAngles += rotationVector * rotationSpeed * Time.deltaTime;



     }

     private void HandleZoom()
     {

         float zoomIncreaseAmount = 1f;

         targetFollowOffset.y += InputManager.Instance.GetCameraZoomAmount() * zoomIncreaseAmount;

         targetFollowOffset.y = Mathf.Clamp(targetFollowOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);

         float zoomSpeed = 5f;
         cinemachineTransposer.m_FollowOffset = Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFollowOffset, zoomSpeed * Time.deltaTime);

     }*/

    public float GetCameraHeight()
    {
        return targetFollowOffset.y;
    }

}
