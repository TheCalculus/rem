using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Light
{
    NONE,
    REM_READER,
    MILKOTOV,
}

enum Medium
{
    NONE,
    BK47,
}

enum Heavy
{
    NONE,
    RPG,
}

public class WeaponController : MonoBehaviour
{
    (Light, Light, Medium) equippedWeapons = (Light.NONE, Light.NONE, Medium.NONE);
}
