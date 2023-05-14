using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField] private Animator _animator;
    [SerializeField] private CharacterController chController;
    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        if (_joystick.Direction != Vector2.zero)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(direction);
            _animator.speed = (Mathf.Abs(_joystick.Horizontal) + Mathf.Abs(_joystick.Vertical));
            _animator.SetBool("isRunning", true);
            _animator.SetBool("boxWalking", !storage.IsEmpty);
            _animator.SetBool("boxIdle", storage.IsEmpty);
        }
        else
        {
            _animator.SetBool("isRunning", false);
            if (!storage.IsEmpty)
            {
                _animator.SetBool("boxIdle", true);
                _animator.SetBool("boxWalking", false);
            }
            else
                _animator.SetBool("boxIdle", false);

        }
        chController.Move(_moveVector);
        // _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    }
}
