using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : MonoBehaviour
{
    [field: SerializeField] public Inventory inventory { get; private set; }
    [SerializeField] private FloatingJoystick _joystick;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _rotateSpeed;

    private Vector3 _moveVector;

    private void Update()
    {
        Move();
    }
}
