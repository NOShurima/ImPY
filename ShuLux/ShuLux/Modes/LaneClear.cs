using static ShuLux.SpellsManager;
using static ShuLux.Menus;
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

namespace ShuLux.Modes
{
    class LaneClear
    {
        internal static void Execute()
        {
            var min = EntityManager.MinionsAndMonsters.EnemyMinions.Where(w => w.IsValidTarget(1200)).OrderBy(o => o.Health);
            if (min != null)
            {
                if (LlaneClear["Lq"].Cast<CheckBox>().CurrentValue)
                {
                    if (LlaneClear["Manager"].Cast<CheckBox>().CurrentValue)
                    {
                        if (Q.IsReady() && Player.Instance.ManaPercent >= LlaneClear["LMana"].Cast<Slider>().CurrentValue)
                        {
                            var p = Q.GetBestLinearCastPosition(min);
                            if (p.HitNumber >= LlaneClear["MinionsQ"].Cast<Slider>().CurrentValue)
                            {
                                Q.Cast(p.CastPosition);
                            }
                        }
                    }
                    else
                    {
                        if (Q.IsReady())
                        {
                            var p = Q.GetBestLinearCastPosition(min);
                            if (p.HitNumber >= LlaneClear["MinionsQ"].Cast<Slider>().CurrentValue)
                            {
                                Q.Cast(p.CastPosition);
                            }
                        }
                    }
                }
                if (LlaneClear["Le"].Cast<CheckBox>().CurrentValue)
                {
                    if (LlaneClear["Manager"].Cast<CheckBox>().CurrentValue)
                    {
                        if (E.IsReady() && Player.Instance.ManaPercent >= LlaneClear["LMana"].Cast<Slider>().CurrentValue)
                        {
                            var p = E.GetBestCircularCastPosition(min);
                            if (p.HitNumber >= LlaneClear["MinionsE"].Cast<Slider>().CurrentValue)
                            {
                                E.Cast(p.CastPosition);
                            }
                        }
                    }
                    else
                    {
                        if (E.IsReady())
                        {
                            var p = E.GetBestCircularCastPosition(min);
                            if (p.HitNumber >= LlaneClear["MinionsE"].Cast<Slider>().CurrentValue)
                            {
                                E.Cast(p.CastPosition);
                            }
                        }
                    }
                }
            }
        }
    }
}

       