using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Controllable : MonoBehaviour
{

    protected Rigidbody2D _rigidbody;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _dashForce;
    [SerializeField] protected float _rotation;
    [SerializeField] protected GameObject _gate;

    [Header("Audio")]
    [SerializeField] protected AudioClip _hitClip;
    [SerializeField] private AudioClip _dashClip;
    [SerializeField] private AudioClip _leadClip;

    protected PlayerState _state;

    protected Ball _ball;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _state = PlayerState.None;
    }
    
    private void FixedUpdate()
    {
        if(_state == PlayerState.Move)
            Movement();
        else if(_state == PlayerState.Dash)
        {
            if(new Vector2((float) Math.Round(_rigidbody.velocity.x),
                (float) Math.Round(_rigidbody.velocity.y)) == Vector2.zero) _state = PlayerState.Move;
        }
        Rotation();
    }

    public void SetState(PlayerState state)
    {
        _state = state;
    }

    protected abstract void Movement();

    protected abstract void Rotation();

    public abstract void Hit();
    
    public void Lead(bool val)
    {
        if (val)
            _ball?.SetParent(transform);
        else
            _ball?.UnsetParent();
    }

    public void Dash()
    {
        if (_state == PlayerState.Dash) return;
        _state = PlayerState.Dash;
        _rigidbody.AddForce(_rigidbody.velocity * _dashForce);
        AudioManager.getInstance().PlayAudio(_dashClip);
    }

    private void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (!collider2D.gameObject.GetComponent<Ball>()) return;
        AudioManager.getInstance().PlayAudio(_leadClip);
        _ball = collider2D.gameObject.GetComponent<Ball>();
    }

    private void OnTriggerExit2D(Collider2D collider2D)
    {
        if(collider2D.gameObject.GetComponent<Ball>())
            _ball = null;
    }

    public enum PlayerState
    {
        Dash,
        Move,
        None
    }
}