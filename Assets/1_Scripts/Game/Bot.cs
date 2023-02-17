using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        Bot.BallState ballState = BallState.BallMiddle;
        float distance = Vector2.Distance(transform.position, ball.position);

        if(distance > 0 && distance < 0.6f)
        {
            ballState = BallState.BallClose;
        }
        else if(distance > 0.6f && distance < 3.0f)
        {
            ballState = BallState.BallMiddle;
        }
        else if(distance > 3)
        {
            ballState = BallState.BallLongAway;
        }

        return ballState;
    }
    
    private GateState GetCurrentStateGate()
    {
        Bot.GateState gateState = GateState.GateMiddle;
        float distance = Vector2.Distance(transform.position, _gate.transform.position);

        if (distance > 0 && distance < 3.0f)
        {
            gateState = GateState.GateClose;
        }
        else if (distance > 3.0f)
        {
            gateState = GateState.GateMiddle;
        }
        else
        {
            gateState = GateState.GateClose;
        }

        return gateState;
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
