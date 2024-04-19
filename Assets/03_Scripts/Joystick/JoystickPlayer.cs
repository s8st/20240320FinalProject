using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickPlayer : MonoBehaviour
{
    public float speed;
    //public VariableJoystick variableJoystick;
    public FixedJoystick fixedJoystick;
    //public Rigidbody rb;
    public Rigidbody2D rb;
    public Vector2 moveDir;

    private void Update()
    {
        // moveDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        //float x = Input.GetAxisRaw("Horizontal") + fixedJoystick.Horizontal; 
        //float y =Input.GetAxisRaw("Vertical") + fixedJoystick.Vertical;

        float x = fixedJoystick.Horizontal;
        float y = fixedJoystick.Vertical;

        //moveDir = new Vector2(x, y);

        //moveDir.Normalize();
    }

    public void FixedUpdate()
    {
        //Vector3 direction = Vector3.forward * fixedJoystick.Vertical + Vector3.right * fixedJoystick.Horizontal;
        //   Vector2 direction = Vector2.up * fixedJoystick.Vertical + Vector2.right * fixedJoystick.Horizontal;
        //   rb.AddForce(direction * speed * Time.fixedDeltaTime/*, ForceMode.VelocityChange*/);

    //    rb.MovePosition(rb.position + moveDir * speed);

    }
}