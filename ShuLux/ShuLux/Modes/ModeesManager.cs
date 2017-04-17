using EloBuddy;
using EloBuddy.SDK;
using ShuLux.Modes;
using static ShuLux.Menus;
using System;
using EloBuddy.SDK.Menu.Values;


namespace ShuLux
{
    internal class ModeesManager
    {
        internal static void InitializeModes()
        {
            Game.OnTick += Game_OnTick;
        }

        private static void Game_OnTick(EventArgs args)
        {
            var orbMode = Orbwalker.ActiveModesFlags;
            var playerMana = Player.Instance.ManaPercent;

            KillSteal.Execute();
            if (orbMode.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                Combo.Execute();
            }
            if (orbMode.HasFlag(Orbwalker.ActiveModes.Harass))
            {
                Harass.Execute();
            }

            if (orbMode.HasFlag(Orbwalker.ActiveModes.LaneClear) && playerMana > LlaneClear["LMana"].Cast<Slider>().CurrentValue)
            {
                LaneClear.Execute();
            }

        }
    }
}