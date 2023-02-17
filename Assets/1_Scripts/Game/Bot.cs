using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : Controllable
{

    [SerializeField] private Transform ball;
    
    protected override void Movement()
    {
        switch (GetCurrentState())
        {
            case BallState.BallClose:
                Lead(true);
                MovementToGate();
                break;
            case BallState.BallMiddle:
                MoveToBall();
                break;
            case BallState.BallLongAway:
                MoveToBall();
                Dashing();
                break;
        }
    }

    private void MovementToGate()
    {
        switch (GetCurrentStateGate())
        {
            case GateState.GateClose:
                Lead(false);
                Hit();
                break;
            case GateState.GateMiddle:
                MoveToGate();
                Dashing();
                break;
        }
    }
    
    
    private void MoveToGate()
    {
        var direction = _gate.transform.position - transform.position;
        _rigidbody.velocity = direction.normalized * _speed;
    }

    private void MoveToBall()
    {
        var direction = ball.position - transform.position;
        _rigidbody.velocity = direction.normalized * _speed;
    }

    private void Dashing()
    {
        if(_state == PlayerState.Move) Dash();
    }

    private BallState GetCurrentState()
    {
        var state = Vector2.Distance(transform.position, ball.position) switch
        {
            > 0 and < 0.6f => BallState.BallClose,
            > 0.6f and < 3 => BallState.BallMiddle,
            > 3 => BallState.BallLongAway,
            _ => BallState.BallLongAway
        };
        return state;
    }
    
    private GateState GetCurrentStateGate()
    {
        var state = Vector2.Distance(transform.position, _gate.transform.position) switch
        {
            > 0 and < 3f => GateState.GateClose,
            > 3f => GateState.GateMiddle,
            _ => GateState.GateClose
        };
        return state;
    }

    protected override void Rotation()
    {
        var vectorToTarget = ball.position;
        var angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
        var q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * _rotation);   
        //transform.LookAt(ball);
    }

    public override void Hit()
    {
        var direction = _gate.transform.position - ball.position;
        _ball?.Hit(direction);
        AudioManager.getInstance().PlayAudio(_hitClip);
    }

    private enum BallState
    {
        BallLongAway,
        BallMiddle,
        BallClose
    }
    
    private enum GateState
    {
        GateMiddle,
        GateClose
    }
}
