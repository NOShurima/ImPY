using System;
using EloBuddy;
using static ShuLux.Menus;

using EloBuddy.SDK.Constants;

namespace ShuLux
{
    class OberAtack
    {
        internal static void GameObject_OnCreate(GameObject sender, EventArgs args)
        {
            if(!(sender is MissileClient))
                return;

            var missile = sender as MissileClient;

            if (!missile.SpellCaster.IsMe || missile.SData.IsAutoAttack())
                return;

            if (missile.SData.Name.Contains("LuxLightStrikeKugel") || missile.SData.Name.Contains("Lux_Base_E_mis.troy") || missile.SData.Name.Contains("LuxLightstrike_tar"))
            {
                LuxEObject = sender;
            }
        }

        internal static void GameObject_OnDelete(GameObject sender, EventArgs args)
        {
            throw new NotImplementedException();
        }
    }
}
