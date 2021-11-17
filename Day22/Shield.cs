using System;

namespace Day22
{
    public class Shield : ISpell
    {
        public Shield(int roundCast)
        {
            RoundCast = roundCast;
        }

        public string Name { get; } = "Shield";
        public int ManaCost { get; } = 113;
        public bool Immediate { get; } = false;
        public int Duration { get; } = 6;
        public int RoundCast { get; }
        public void OnCast(Combat combat)
        {
            combat.PlayerManaPoints -= 113;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += 113;
            combat.PlayerArmor += 7;
            combat.Log.Add("Player casts Shield, increasing armor by 7.");
        }

        public void OnTick(Combat combat)
        {
            var remainingRounds = ISpell.GetSpellTimer(combat, "Shield");
            combat.Log.Add($"Shield's timer is now {remainingRounds}.");
        }

        public void OnWearsOff(Combat combat)
        {
            combat.PlayerArmor -= 7;
            combat.Log.Add("Shield wears off, decreasing armor by 7.");
        }
    }
}