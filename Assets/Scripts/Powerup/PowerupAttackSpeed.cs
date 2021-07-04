public class PowerupAttackSpeed : Powerup
{
    protected override void SpecificInfluence(PlayerController player)
    {
        player.UpdateAttackSpeed(impactValue);
    }
}
