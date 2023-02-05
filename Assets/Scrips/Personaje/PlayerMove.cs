using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    public Animator animation;

    public InputActionProperty cMovimiento;
    public CharacterController controller;

    public float speed = 5;
    public float turnSmoothTime = 0.1f;

    float turnSmoothVelocity;

    private void Start()
    {
        cMovimiento.action.Enable();
    }


    void Update()
    {
        float horizontal = cMovimiento.action.ReadValue<Vector2>().x;
        float vertical = cMovimiento.action.ReadValue<Vector2>().y;

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;

            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(direction * speed * Time.deltaTime);
        }
        animation.SetBool("Corriendo", direction.magnitude > 0);
    }

}
