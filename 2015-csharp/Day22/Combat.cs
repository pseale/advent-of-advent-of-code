using System.Collections.Generic;

namespace Day22
{
    public class Combat
    {
        public int Round { get; set; } = 1;
        public int ManaSpent { get; set; }

        public int PlayerHitPoints { get; set; }
        public int PlayerManaPoints { get; set; }
        public int PlayerArmor { get; set; }
        public List<ISpell> ActiveSpells { get; set; } = new();
        public List<string> SpellsCast { get; set; } = new();
        public int BossHitPoints { get; set; }
        public int BossDamage { get; set; }
        public bool IsPlayerTurn { get; set; } = true; // starts with player turn

        public List<string> Log { get; set; } = new();

        // apology: it feels icky to have some (but not most) of the logic encapsulated here in this mostly-just-a-dumb-DTO object
        public string PlayerStatusLog()
        {
            return $"- Player has {Print(PlayerHitPoints)}, {PlayerArmor} armor, {PlayerManaPoints} mana";
        }

        public string BossStatusLog()
        {
            return $"- Boss has {Print(BossHitPoints)}";
        }

        private string Print(int hitPoints)
        {
            return hitPoints != 1 ? $"{hitPoints} hit points" : $"{hitPoints} hit point";
        }
    }
}