using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;
using Language.Lua;

public class PlayerMovement : MonoBehaviour
{
    public UnityEvent onReload;
    private Vector2 mouse;
    private Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private float moveSpeed;

    [SerializeField] Animator bodyAnimator;
    [SerializeField] Animator headAnimator;
    [SerializeField] private FieldOfView fieldOfView;

    private Transform headTransform;
    private Vector3 initialHeadPos;

    Quaternion lookCorrection2D = Quaternion.LookRotation(Vector3.up, -Vector3.back);

    Vector3 aimDirection = Vector3.zero;
    Vector3 prevAimDirection = Vector3.zero;

    float prevAngleDeg;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        headTransform = transform.GetChild(0).transform;
    }

    //private Coroutine LookCoroutine;

    //private void StartRotation()
    //{
    //    if (LookCoroutine != null)
    //    {
    //        StopCoroutine(LookCoroutine);
    //    }
    //    LookCoroutine = StartCoroutine(LookAt());
    //}

    //private IEnumerator LookAt()
    //{
    //    var mousePos = Camera.main.ScreenToWorldPoint(mouse);
    //    mousePos.z = 0f;
    //    Quaternion lookRotation = Quaternion.LookRotation(mousePos - headTransform.position) * lookCorrection2D;

    //    Vector3 aimDirection = (mousePos - headTransform.position).normalized;
    //    float distanceFromMouse = Vector2.Distance(mousePos, headTransform.position);

    //    // Get Angle in Degrees
    //    float AngleDeg = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

    //    float time = 0;

    //    while (time < 1)
    //    {
    //        headTransform.rotation = Quaternion.Slerp(transform.GetChild(0).transform.rotation, Quaternion.Euler(0, 0, AngleDeg + 90f), time) * lookCorrection2D;
    //        time += Time.deltaTime * 1f;
    //        yield return null;
    //    }
    //}

    // Update is called once per frame
    void FixedUpdate()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(mouse);
        mousePos.z = 0f;

        Vector3 aimDirection = (mousePos - headTransform.position).normalized;
        float distanceFromMouse = Vector2.Distance(mousePos, headTransform.transform.position);

        // Get Angle in Degrees
        float AngleDeg = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        float offset = 90f;
        float angleWithOffset = AngleDeg + offset;

        //transform.rotation = Quaternion.Euler(0, 0, AngleDeg);
        if (Time.timeScale != 0)
        {
            headAnimator.SetFloat("direction", angleWithOffset);
            //StartRotation();
            headTransform.eulerAngles = new Vector3(0, 0, angleWithOffset);
        }

        bodyAnimator.SetBool("isCharging", GameManager.instance.isBatteryCharging);

        // Move Object
        if (!GameManager.instance.isBatteryCharging)
        {
            if (headTransform.gameObject.GetComponent<SpriteRenderer>().enabled == false) headTransform.gameObject.GetComponent<SpriteRenderer>().enabled = true;
            rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);

            bodyAnimator.SetFloat("walkingX", moveInput.x);
            bodyAnimator.SetFloat("walkingY", moveInput.y);
        } else
        {
            if (headTransform.gameObject.GetComponent<SpriteRenderer>().enabled == true) headTransform.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            StartCoroutine(PlayReloadAnimation()); //timer for animation
        }

        if (Time.timeScale != 0)
        {
            fieldOfView.SetAimDirection(aimDirection);
            fieldOfView.SetOrigin(headTransform.position);
        }
    }

    IEnumerator PlayReloadAnimation()
    {
        yield return new WaitForSeconds(1f); //change this as needed *****
        GameManager.instance.isBatteryCharging = false; //figure a better way for this along with PlayerCameraBattery script ********
        StopCoroutine(PlayReloadAnimation());
    }

    private void OnLook(InputValue value)
    {
        mouse = value.Get<Vector2>();
    }

    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    private void OnReload(InputValue value)
    {
        onReload?.Invoke();
    }
}
