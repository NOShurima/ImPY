using System.Linq;
using EloBuddy;
using EloBuddy.SDK;

namespace Perfect_Smite
{
    public class SpellSmite
    {
        public static Spell.Targeted Smite { get; private set; }

        static SpellSmite()
        {
            Smite = new Spell.Targeted(Player.Instance.Spellbook.Spells.FirstOrDefault(s => s.SData.Name.ToLower().Contains("smite")).Slot, 570);
        }
    }
}