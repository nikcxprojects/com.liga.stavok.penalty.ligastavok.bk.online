using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Controllable
{
    [SerializeField] private FixedJoystick _joystick;

    protected override void Movement()
    {
        //_rigidbody.velocity = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * _speed;
        _rigidbody.velocity = _joystick.Direction * _speed;
    }

    protected override void Rotation()
    {
        var vectorToTarget = _rigidbody.velocity;
        if (vectorToTarget == Vector2.zero) return;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotation);    
    }

    public override void Hit()
    {
        _ball?.Hit(_rigidbody.velocity);
        AudioManager.getInstance().PlayAudio(_hitClip);
    }
}
