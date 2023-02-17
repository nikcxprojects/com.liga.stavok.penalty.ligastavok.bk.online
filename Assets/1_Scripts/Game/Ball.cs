using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float _dashForce;
    [SerializeField] private GameManager _gameManager;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }
    
    public void SetParent(Transform parent)
    {
        transform.parent = parent;
        _rigidbody.bodyType = RigidbodyType2D.Kinematic;
    }

    public void UnsetParent()
    {
        transform.parent = null;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void Hit(Vector2 velocity)
    {
        _rigidbody.AddForce(velocity * _dashForce);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("GatePlayer"))
        {
            _gameManager.Goal(GameManager.Gate.Player);
        }
        else if(collider.tag.Equals("GateBot"))
        {
            _gameManager.Goal(GameManager.Gate.Bot);
        }
    }
}
