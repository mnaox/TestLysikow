using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
public class CamController : MonoBehaviour
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    private float speedRotation = 1.0f; //скорость вращения камеры мышкой
    [SerializeField]
    public Vector3 CameraOffset;
    private Vector3 _offset;
    private float x = 0.0f;
    private float y = 0.0f;
    private Quaternion rotation;
    [SerializeField]
    private float smoothSpeed = 0.125f; //плавность движения камеры при смене фокуса
    private bool mouseActive = true;
    public Controller _scheme = new Controller();
    void Start()
    {
        _offset = CameraOffset;
    }
    
    void Update()
    {
        if (Input.GetMouseButton(0) && target && !EventSystem.current.IsPointerOverGameObject() && mouseActive)
        {
            x += Input.GetAxis("Mouse X") * speedRotation;
            y -= Input.GetAxis("Mouse Y") * speedRotation;
            rotation = Quaternion.Euler(y, x, 0);
            transform.rotation = rotation;
            Vector3 position = rotation * _offset + target.position;
            transform.position = position;
        }
        else
        {
            Vector3 position = this.gameObject.transform.rotation * _offset + target.position;
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, position, smoothSpeed);
            transform.position = smoothedPosition;
        }

    }


    public void SetCameraTarget(Transform targetObj, Vector3 offset) // установка новой точки фокуса для камеры
    {
        _offset = offset;
        target = targetObj;
        StartCoroutine(blockInput(2));    }
    IEnumerator blockInput(int time) //блокировка возможности вращать объект
    {
        mouseActive = false;
        yield return new WaitForSeconds(time);
        mouseActive = true;
    }
}