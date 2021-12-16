using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Small : Enemy // INHERITANCE
{
    public override void SetEnemySpeedAfterLastWave() // POLYMORPHISM
    {
        speed = 6;
    }

}
