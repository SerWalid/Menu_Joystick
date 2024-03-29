using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof (BoxCollider))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private FixedJoystick _joystick;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject[] _heartBars;

    private int _collisionCount = 0;

    private void Start()
    {
        // Initialize heart bars

        foreach (GameObject heartBar in _heartBars)
        {
            heartBar.SetActive(true);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector3(_joystick.Horizontal * _moveSpeed, _rigidbody.velocity.y, _joystick.Vertical * _moveSpeed);

        if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            _collisionCount++;

            
            if (_collisionCount >= 3)
            {
               
                foreach (GameObject heartBar in _heartBars)
                {
                    Destroy(heartBar);
                }
                
                Debug.Log("Game Over!");               
            }
            else
            {
               
                Destroy(_heartBars[_collisionCount - 1]);
            }
        }
    }


}
