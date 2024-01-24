using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LightType
{
    NONE,
    REM_READER,
    MILKOTOV,
}

public enum MediumType
{
    NONE,
    BK47,
}

public enum HeavyType
{
    NONE,
    RPG,
}

public class WeaponController : MonoBehaviour
{
    private (LightType, LightType, MediumType) equippedWeapons = (LightType.NONE, LightType.NONE, MediumType.NONE);
}
