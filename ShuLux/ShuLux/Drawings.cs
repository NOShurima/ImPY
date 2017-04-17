using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Rendering;
using SharpDX;


namespace ShuLux
{
    class Drawings
    {
        public static void OnDraw(EventArgs args)
        {
            if (!Menus.LDraw["draw"].Cast<CheckBox>().CurrentValue) return;

            if (Menus.LDraw["DQ"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady())
                Circle.Draw(Color.LightBlue, SpellsManager.Q.Range, Player.Instance.Position);
            if (Menus.LDraw["DW"].Cast<CheckBox>().CurrentValue && SpellsManager.W.IsReady())
                Circle.Draw(Color.BlueViolet, SpellsManager.W.Range, Player.Instance.Position);
            if (Menus.LDraw["DE"].Cast<CheckBox>().CurrentValue && SpellsManager.E.IsReady())
                Circle.Draw(Color.DeepSkyBlue, SpellsManager.E.Range, Player.Instance.Position);
            if (Menus.LDraw["DR"].Cast<CheckBox>().CurrentValue && SpellsManager.R.IsReady())
                Circle.Draw(Color.LightSkyBlue, SpellsManager.R.Range, Player.Instance.Position);
            if (Menus.LDraw["ignite"].Cast<CheckBox>().CurrentValue && SpellsManager.Ignite != null && SpellsManager.Ignite.IsReady())
                Circle.Draw(Color.OrangeRed, SpellsManager.Ignite.Range, Player.Instance.Position);

            if (Menus.LDraw["Dameger"].Cast<CheckBox>().CurrentValue)
            {
                Calculations.Execute();

                foreach (var enemy in EntityManager.Heroes.Enemies.Where(e => e.IsHPBarRendered))
                {
                    var hp = (enemy.TotalShieldHealth() - Calculations.Damage > 0f ?
                              enemy.TotalShieldHealth() - Calculations.Damage : 0f) /
                             (enemy.MaxHealth + enemy.AllShield + enemy.AttackShield + enemy.MagicShield);
                    var start = new Vector2(enemy.HPBarPosition.X + hp * 107, enemy.HPBarPosition.Y - 10f);
                    var end = new Vector2(enemy.HPBarPosition.X + hp * 107, enemy.HPBarPosition.Y);
                    var color = enemy.TotalShieldHealth() - Calculations.Damage > 0f ?
                                System.Drawing.Color.Lime : System.Drawing.Color.Red;

                    Drawing.DrawLine(start, end, 1, color);
                }
            }
        }
    }
}