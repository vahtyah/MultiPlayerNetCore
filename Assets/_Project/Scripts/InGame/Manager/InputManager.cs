﻿using UnityEngine;

public static class InputManager
{
    public static float HorizontalMovement() { return Input.GetAxisRaw("Horizontal"); }

    public static float VerticalMovement() { return Input.GetAxisRaw("Vertical"); }
}