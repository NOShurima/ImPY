using System;
using System.Linq;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Enumerations;

namespace Activetion
{
    class Program
    {
        static Menu menu;

        static float lastaa, lastmove, aacastdelay, aadelay, lastminion;
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static bool isChecked(Menu obj, String value)
        {
            return obj[value].Cast<CheckBox>().CurrentValue;
        }

        public static int getSliderValue(Menu obj, String value)
        {
            return obj[value].Cast<Slider>().CurrentValue;
        }

        public static Spell.Ranged Q;
        public static Spell.Active R;

        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += VayLoad;
        }

        private static void VayLoad(EventArgs args)
        {
            Q = new Spell.Skillshot(SpellSlot.Q, 300, SkillShotType.Linear);
            R = new Spell.Active(SpellSlot.R);

            if (!Player.Instance.ChampionName.ToLower().Contains("vayne"))
                return;
            Chat.Print("Pronto");
            menu = MainMenu.AddMenu("Activetion", "Activetion");
            menu.Add("combo", new CheckBox("Combo Active", true));
            menu.Add("CR", new CheckBox("Active(R)"));
            menu.Add("UCR", new Slider("Use R When You Have More Enemy >= {0}", 3, 0, 5));
            Game.OnUpdate += Game_OnTick;
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
            Obj_AI_Base.OnBuffGain += Obj_AI_Base_OnBuffGain;
        }

        private static void Game_OnTick(EventArgs args)
        {
            if (menu["combo"].Cast<CheckBox>().CurrentValue)
            {
                Orbwalker.DisableMovement = true;
                Orbwalker.DisableAttacking = true;
                Combo();
            }
            if (Program.isChecked(menu,"CR") && R.IsReady())
            {
                if (_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) >= Program.getSliderValue(menu, "UCR"))
                {
                    R.Cast();
                }
            }
        }

          private static void Combo()
        {
            if (Player.CanUseSpell(SpellSlot.R) == SpellState.Ready && Game.Time > lastaa + aacastdelay + 0.025f && Game.Time < lastaa + (aadelay * 0.75f))
            {
                Player.CastSpell(SpellSlot.R, Game.CursorPos);
                return;
            }
            var target = GetAATarget(Player.Instance.AttackRange + Player.Instance.BoundingRadius);
            if (target == null)
            {
                if (Game.Time > lastaa + aacastdelay + 0.025f && Game.Time > lastmove + 0.150f)
                {
                    Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                    lastmove = Game.Time;
                }
                return;
            }
            if (Game.Time > lastaa + aadelay)
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, target);
                return;
            }
            if (Game.Time > lastaa + aacastdelay + 0.025f && Game.Time > lastmove + 0.150f)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                lastmove = Game.Time;
            }
        
        if (Program.isChecked(menu,"CR") && R.IsReady())
            {
                if (_Player.CountEnemiesInRange(_Player.GetAutoAttackRange()) >= Program.getSliderValue(menu, "UCR"))
                {
                    R.Cast();
                }
}
        }
        static AttackableUnit GetAATarget(float range)
        {
            AttackableUnit t = null;
            float num = 10000;
            foreach (var enemy in EntityManager.Heroes.Enemies)
            {
                float hp = enemy.Health;
                if (enemy.IsValidTarget(range + enemy.BoundingRadius) && hp < num)
                {
                    num = hp;
                    t = enemy;
                }
            }
            return t;
        }

        static void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                aacastdelay = sender.AttackCastDelay;
                aadelay = sender.AttackDelay;
                lastaa = Game.Time;
            }
        }

        static void Obj_AI_Base_OnBuffGain(Obj_AI_Base sender, Obj_AI_BaseBuffGainEventArgs args)
        {
            if (sender.IsMe && args.Buff.Name == "vaynetumblebonus")
            {
                lastaa = 0;
            }
        }
    }
}