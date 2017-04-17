using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Constants;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;
using static ShuLux.SpellsManager;
using static ShuLux.Menus;

namespace ShuLux.Modes
{
    class Harass
    {
        internal static void Execute()
        {
            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            if (target == null) return;
            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Mixed);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Mixed);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Mixed);
            var rtarget = TargetSelector.GetTarget(3400, DamageType.Mixed);

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            if (Q.IsReady() && LHarass["HQ"].Cast<CheckBox>().CurrentValue)
            {
                var predq = Q.GetPrediction(qtarget);
                if (predq.HitChance >= HitChance.High)
                {
                    Q.Cast(predq.CastPosition);
                }
            }

            if (E.IsReady() && LHarass["HE"].Cast<CheckBox>().CurrentValue)
            {
                var prede = E.GetPrediction(etarget);
                if (prede.HitChance >= HitChance.High)
                {
                    E.Cast(prede.CastPosition);
                }
            }

            if (R.IsReady() && LHarass["HR"].Cast<CheckBox>().CurrentValue)
            {
                var prede = R.GetPrediction(etarget);
                if (prede.HitChance >= HitChance.High)
                {
                    R.Cast(prede.CastPosition);
                }
            }
        }
    }
}