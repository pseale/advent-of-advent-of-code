using System;

namespace Day22
{
    public class Poison : ISpell
    {
        private const int Damage = 3;

        public Poison(int roundCast)
        {
            RoundCast = roundCast;
        }

        public string Name { get; } = "Poison";
        public int ManaCost { get; } = 173;
        public bool Immediate { get; } = false;
        public int Duration { get; } = 6;
        public int RoundCast { get; }
        public void OnCast(Combat combat)
        {
            combat.PlayerManaPoints -= ManaCost;
            if (combat.PlayerManaPoints < 0)
                throw new Exception(
                    $"Impossible: player should not have been able to cast this spell. Mana points: {combat.PlayerManaPoints}");
            combat.ManaSpent += ManaCost;
            combat.Log.Add("Player casts Poison.");
        }

        public void OnTick(Combat combat)
        {
            combat.BossHitPoints -= Damage;
            var remainingRounds = ISpell.GetSpellTimer(combat, "Poison");
            combat.Log.Add($"Poison deals {Damage} damage; its timer is now {remainingRounds}.");
        }

        public void OnWearsOff(Combat combat)
        {
            combat.Log.Add("Poison wears off.");
        }
    }
}