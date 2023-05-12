using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField] private Animator _animator;
    private void Move()
    {
        _moveVector = Vector3.zero;
        _moveVector.x = _joystick.Horizontal * _moveSpeed * Time.deltaTime;
        _moveVector.z = _joystick.Vertical * _moveSpeed * Time.deltaTime;

        if(_joystick.Horizontal != 0 || _joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, _moveVector, _rotateSpeed * Time.deltaTime, 0f);
            transform.rotation = Quaternion.LookRotation(direction);

            _animator.SetBool("isRunning", true);
            _animator.SetBool("boxWalking", Inventory.Instance.IsItemInInventory);
            _animator.SetBool("boxIdle", !Inventory.Instance.IsItemInInventory);
            

        }
        else
        {
            _animator.SetBool("isRunning", false);
            if(Inventory.Instance.IsItemInInventory == true)
            {
                _animator.SetBool("boxIdle", true);
                _animator.SetBool("boxWalking", false);
            }
            else
                _animator.SetBool("boxIdle", false);
                
        }

        _rigidbody.MovePosition(_rigidbody.position + _moveVector);
    }
}
