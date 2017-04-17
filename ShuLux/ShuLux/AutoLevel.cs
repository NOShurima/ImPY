using EloBuddy;
using EloBuddy.SDK;
using System;
using static ShuLux.Menus;

namespace ShuLux
{
    internal class AutoLevel
    {
        internal static void AutoMenu()
        {
            Obj_AI_Base.OnLevelUp += OnLvlUp;
        }

   
       private static void OnLvlUp(Obj_AI_Base sender, Obj_AI_BaseLevelUpEventArgs args)
        {
            if (!sender.IsMe || !check(LeLevel, "Auto")) return;

            Core.DelayAction(delegate
            {
                if (myhero.Level > 1 && myhero.Level < 4)
                {
                    switch (myhero.Level)
                    {
                       
                        case 2:
                            Player.LevelSpell(SpellSlot.E);
                            break;
                        case 3:
                            Player.LevelSpell(SpellSlot.W);
                            break;
                    }
                }
                else if (myhero.Level >= 4)
                {
                    if (myhero.Spellbook.CanSpellBeUpgraded(SpellSlot.R) && Player.LevelSpell(SpellSlot.R))
                    {
                        return;
                    }
                    else if (myhero.Spellbook.CanSpellBeUpgraded(SpellSlot.Q) && Player.LevelSpell(SpellSlot.Q))
                    {
                        return;
                    }
                    else if (myhero.Spellbook.CanSpellBeUpgraded(SpellSlot.E) && Player.LevelSpell(SpellSlot.E))
                    {
                        return;
                    }
                    else if (myhero.Spellbook.CanSpellBeUpgraded(SpellSlot.W) && Player.LevelSpell(SpellSlot.W))
                    {
                        return;
                    }
                }
            }, 1000);
        }
    }
}