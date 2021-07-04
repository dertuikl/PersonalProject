using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupWeaponDamage : Powerup
{
    protected override void SpecificInfluence(PlayerController player)
    {
        player.UpdateWeaponDamage(impactValue);
    }
}
