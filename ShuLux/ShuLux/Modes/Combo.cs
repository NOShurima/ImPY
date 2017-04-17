using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Menu.Values;
using static ShuLux.SpellsManager;
using static ShuLux.Menus;
using System.Linq;

namespace ShuLux
{
    internal class Combo
    {
        internal static void Execute()
        {

            var target = TargetSelector.GetTarget(E.Range + 200, DamageType.Mixed);

            var qtarget = TargetSelector.GetTarget(Q.Range, DamageType.Magical);
            var wtarget = TargetSelector.GetTarget(W.Range, DamageType.Magical);
            var etarget = TargetSelector.GetTarget(E.Range, DamageType.Magical);
            var rtarget = TargetSelector.GetTarget(3400, DamageType.Mixed);
            var Etarget = TargetSelector.GetTarget(E.Range + 200, DamageType.Magical);
            var enemies = EntityManager.Heroes.Enemies.OrderByDescending(a => a.HealthPercent).Where(a => !a.IsMe && a.IsValidTarget() && a.Distance(_Player) <= R.Range);
            var predq = Q.GetPrediction(qtarget);

            if (target == null || target.IsInvulnerable || target.MagicImmune)
            {
                return;
            }


            if (Q.IsReady() && LCombo["CQ"].Cast<CheckBox>().CurrentValue)
            {
                var t = TargetSelector.GetTarget(Q.Range, DamageType.Magical);

                if (t != null)
                {
                    var pred = Q.GetPrediction(t);

                    if (pred != null && t.IsValidTarget() && pred.HitChancePercent >= LPrediction["qSlider"].Cast<Slider>().CurrentValue)
                    {
                        Q.Cast(pred.CastPosition);
                    }
                }

                if (E.IsReady() && LCombo["CE"].Cast<CheckBox>().CurrentValue)
                {
                    var prede = E.GetPrediction(etarget);
                    if (prede.HitChance >= HitChance.High)
                    {
                        E.Cast(prede.CastPosition);
                    }
                }
                E.Cast();
                {

                    if (R.IsReady() && LCombo["CR"].Cast<CheckBox>().CurrentValue)
                    {
                        foreach (var ultenemies in enemies)
                        {
                            var useR = LCombo["r.ult" + ultenemies.ChampionName].Cast<CheckBox>().CurrentValue;
                            var predictedHealth = Prediction.Health.GetPrediction(target, R.CastDelay + Game.Ping);
                            var passiveDamage = target.HasPassive() ? target.GetPassiveDamage() : 0f;
                            var rDamage = target.GetDamage(SpellSlot.R) + passiveDamage;
                            {
                                if ((useR) && (predictedHealth <= rDamage))
                                {
                                    R.Cast(ultenemies);
                                }
                                else if (R.IsReady())
                                {

                                    var totalDamage = target.GetDamage(SpellSlot.E) + target.GetDamage(SpellSlot.R) + passiveDamage;


                                    if (predictedHealth <= totalDamage)
                                    {
                                        R.Cast(ultenemies);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

    