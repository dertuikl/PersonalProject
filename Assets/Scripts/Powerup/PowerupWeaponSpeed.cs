using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWeaponSpeed : Powerup
{
    protected override void SpecificInfluence(PlayerController player)
    {
        player.UpdateWeaponSpeed(impactValue);
    }
}
