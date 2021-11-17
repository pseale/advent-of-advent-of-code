using System;

namespace Day22
{
    public class Recharge : ISpell
    {
        private const int ManaProvided = 101;

        public Recharge(int roundCast)
        {
            RoundCast = roundCast;
        }

        public string Name { get; } = "Recharge";
        public int ManaCost { get; } = 229;
        public bool Immediate { get; } = false;
        public int Duration { get; } = 5;
        public int RoundCast { get; }
        public void OnCast(Combat combat)
        {
            combat.PlayerManaPoints -= ManaCost;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += ManaCost;
            combat.Log.Add("Player casts Recharge.");
        }

        public void OnTick(Combat combat)
        {
            var remainingRounds = ISpell.GetSpellTimer(combat, "Recharge");
            combat.PlayerManaPoints += ManaProvided;
            combat.Log.Add($"Recharge provides {ManaProvided} mana; its timer is now {remainingRounds}.");
        }

        public void OnWearsOff(Combat combat)
        {
            combat.Log.Add("Recharge wears off.");
        }
    }
}