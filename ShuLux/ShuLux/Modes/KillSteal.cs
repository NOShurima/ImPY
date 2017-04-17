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
using System.Diagnostics;
using ShuLux;

namespace ShuLux
{
    internal class KillSteal
    {
        public static Obj_AI_Minion Minion;

        internal static void Execute()
        {
            if (SpellsManager.W.IsReady())
            {
                if (Player.Instance.HealthPercent <= 20 && EntityManager.Heroes.Enemies.Count(e => !e.IsDead && SpellsManager.W.IsInRange(e)) > 0 && LHarass["HW"].Cast<CheckBox>().CurrentValue)
                {
                    if (Player.Instance.HealthPercent <= 20)
                    {
                        return;
                    }
                    else
                    {
                        var firstOrDefault = EntityManager.Heroes.Enemies.FirstOrDefault(e => !e.IsDead && SpellsManager.W.IsInRange(e));
                        if (firstOrDefault != null)
                           SpellsManager.W.Cast(firstOrDefault.ServerPosition);
                    }
                }
            }

            var target = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Magical);
            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }

            //Thanks to Mario
             if (LKilSteal["KR"].Cast<CheckBox>().CurrentValue)
                {
                var rtarget = TargetSelector.GetTarget(R.Range, DamageType.Magical);

                if (rtarget == null) return;

                if (SpellsManager.R.IsReady())
                {
                    var passiveDamage = rtarget.HasPassive() ? rtarget.GetPassiveDamage() : 0f;
                    var rDamage = rtarget.GetDamage(SpellSlot.R) + passiveDamage;

                    var predictedHealth = Prediction.Health.GetPrediction(rtarget, R.CastDelay + Game.Ping);

                    if (predictedHealth <= rDamage)
                    {
                        var pred = SpellsManager.R.GetPrediction(rtarget);
                        if (pred.HitChancePercent >= 90)
                        {
                            SpellsManager.R.Cast(pred.CastPosition);
                        }
                    }
                }
            }
        }
    }
}