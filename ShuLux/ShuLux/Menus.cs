using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Notifications;
using EloBuddy.SDK.Enumerations;
using EloBuddy.SDK.Spells;
using System;
using System.Linq;
using static ShuLux.SpellsManager;

namespace ShuLux
{
    internal class Menus
    {
        public static Menu Main,
            LCombo,
            LHarass,
            LlaneClear,
            LJungleSteal,
            LDraw,
            LLogic,
            LPrediction,
            LeLevel,
            MiscMenu,
            LKilSteal;
        private static AIHeroClient myHero
        {
            get { return Player.Instance; }
        }
        public static AIHeroClient _Player
        {
            get { return ObjectManager.Player; }
        }
        public static GameObject LuxEObject;

        internal static bool ManaManager(SpellSlot spellSlot)
        {
            if (MiscMenu["disableC"].Cast<CheckBox>().CurrentValue
                && Orbwalker.ActiveModesFlags.HasFlag(Orbwalker.ActiveModes.Combo))
            {
                return true;
            }

            var playerManaPercent = Player.Instance.ManaPercent;

            if (spellSlot == SpellSlot.Q)
            {
                return MiscMenu["manaQ"].Cast<Slider>().CurrentValue < playerManaPercent;
            }

            if (spellSlot == SpellSlot.W)
            {
                return MiscMenu["manaW"].Cast<Slider>().CurrentValue < playerManaPercent;
            }

            if (spellSlot == SpellSlot.E)
            {
                return MiscMenu["manaE"].Cast<Slider>().CurrentValue < playerManaPercent;
            }

            if (spellSlot == SpellSlot.R)
            {
                return MiscMenu["manaR"].Cast<Slider>().CurrentValue < playerManaPercent;
            }

            return false;
        }
      
        static string[] Monster_List = new string[] {
            "SRU_Dragon_Water", "SRU_Dragon_Fire", "SRU_Dragon_Earth", "SRU_Dragon_Air", "SRU_Dragon_Elder",
            "SRU_RiftHerald", "SRU_Baron", "SRU_Blue", "SRU_Red" };
        private static int StartTime;
        private static string Name;

        public static void GetName(AIHeroClient unit)
        {
            Name = unit.BaseSkinName;
        }

        public static void GetStartTime(int time)
        {
            StartTime = time;
        }

        public static AIHeroClient _player
        {
            get { return ObjectManager.Player; }

        }
        public static bool check(Menu submenu, string sig)
        {
            return submenu[sig].Cast<CheckBox>().CurrentValue;
        }
        public static AIHeroClient myhero { get { return ObjectManager.Player; } }

        public const string LComboID = "combomenuid";

        internal static void MyMenu()
        {
            Main = MainMenu.AddMenu("ShuLux", "ShuLux");
            LLogic = Main.AddSubMenu("LogicCombo");
            LLogic.AddSeparator();
            LLogic.AddLabel("Combo Stun - Q - W -E - R");
            LLogic.AddLabel("Combo Spells Q - E - R - W");
            LLogic.AddSeparator();
            LLogic.AddLabel("Logic R Spell");
            LLogic.Add("LR", new CheckBox("Use R logic", false));
            LLogic.AddSeparator();
            LLogic.AddLabel("Interrupter");
            LLogic.Add("InterrupterSpell", new CheckBox("Use Q Interrupter"));
            //Predction
            LPrediction = Main.AddSubMenu("Prediction");
            LPrediction.Add("qSlider", new Slider("Cast Q if % HitChance", 75));
            LPrediction.AddSeparator();
            LPrediction.AddLabel("Hit E");
            LPrediction.Add("eSlider", new Slider("Cast E if % HitChance", 75));
            LPrediction.AddSeparator();
            LPrediction.AddLabel("Hit R");
            LPrediction.Add("rSlider", new Slider("Cast R if % HitChance", 75));
            //Combo
            LCombo = Main.AddSubMenu("Combo Lux");
            LCombo.Add("CQ", new CheckBox("Use Q in Combo"));
            LCombo.Add("CW", new CheckBox("Use W in Combo", false));
            LCombo.Add("CE", new CheckBox("Use E in Combo"));
            LCombo.Add("CR", new CheckBox("Use R in Combo"));
            LCombo.Add("SliderR", new Slider("Amount of Enemies before casting R", 3, 1, 5));
            LLogic.AddSeparator();
            LCombo.AddLabel("Config Spell W");
            LCombo.Add("LW", new CheckBox("Use W in Ally"));
            LCombo.Add("LLW", new CheckBox(" Use W"));
            LCombo.Add("LMin", new Slider("Min de Hp", 45));
            //Harass
            LHarass = Main.AddSubMenu("Harass Lux");
            LHarass.Add("HQ", new CheckBox("Use Q in Harass"));
            LHarass.Add("HW", new CheckBox("Use W in Harass"));
            LHarass.Add("HE", new CheckBox("Use E in Harass"));
            LHarass.Add("HR", new CheckBox("Use R In Harass"));
            LHarass.Add("HM", new Slider("Mana%", 30));
            //LaneClear
            LlaneClear = Main.AddSubMenu("Lane Lux");
            LlaneClear.Add("Lq", new CheckBox("Use Q Lane Clear", false));
            LlaneClear.Add("Le", new CheckBox("Use E Lane Claer"));
            LlaneClear.Add("Lr", new CheckBox("Use R Lane CLear", false));
            LLogic.AddSeparator();
            LlaneClear.Add("LMana", new Slider("Mana%", 30));
            LlaneClear.Add("Le", new CheckBox("Use Lane Clear"));
            LlaneClear.Add("Manager", new CheckBox("Mana Lane Clear"));
            LlaneClear.Add("MinionsE", new Slider("Min. Minions for E ", 3, 0, 10));
            LlaneClear.Add("MinionsQ", new Slider("Min. Minions for Q ", 2, 0, 10));
            //JungleSteal
            LJungleSteal = Main.AddSubMenu("JungleSteal");
            LJungleSteal.Add("JSR", new CheckBox("Use R Jungle Steal"));
            LJungleSteal.Add("BuffBlue", new CheckBox("Use R Steal Buff Blue", false));
            LJungleSteal.Add("BuffRed", new CheckBox("Use R Steal Buff Red", false));
            LJungleSteal.Add("BufDragon", new CheckBox("Use R Steal Dragon", true));
            //Draw Spell
            LDraw = Main.AddSubMenu("Draw Spells");
            LDraw.Add("draw", new CheckBox("Enable Drawings"));
            LDraw.AddSeparator();
            LDraw.Add("DQ", new CheckBox("Draw Q Spell"));
            LDraw.Add("DW", new CheckBox("Draw W Spell"));
            LDraw.Add("DE", new CheckBox("Draw E Spell"));
            LDraw.Add("DR", new CheckBox("Draw R Spell"));
            LDraw.Add("Ignite", new CheckBox("Draw Ignite"));
            LLogic.AddSeparator();
            LDraw.AddLabel("Config Damage");
            LDraw.Add("Dameger", new CheckBox("Damager Indicador", true));
            //Spell R KillSteal
            LKilSteal = Main.AddSubMenu("KilSteal");
            LKilSteal.Add("KR", new CheckBox("Use R KilSteal"));
            LKilSteal.Add("ignite", new CheckBox("Use Iginite"));
            //Auto Level
            LeLevel = Main.AddSubMenu("Auto Level");
            LeLevel.Add("Auto", new CheckBox("Use Auto Level", false));
            LeLevel.AddLabel("Bugs Spell Not Recomended");
            //Misc
            MiscMenu = Main.AddSubMenu("ManaManger");
            MiscMenu.Add("manaQ", new Slider("Mana Manager Q", 25));
            MiscMenu.Add("manaW", new Slider("Mana Manager W", 25));
            MiscMenu.Add("manaE", new Slider("Mana Manager E", 25));
            MiscMenu.Add("manaR", new Slider("Mana Manager R", 25));
            MiscMenu.Add("disableC", new CheckBox("Disable Mana Manager in Combo"));
            MiscMenu.AddGroupLabel("Misc Settings");
            MiscMenu.Add("useW", new CheckBox("Automatically Cast W"));
            MiscMenu.Add("useM", new CheckBox("Use W only on Modes"));
            MiscMenu.Add("hpW", new Slider("HP % before W", 25));
            MiscMenu.AddLabel("Who to use W on?");

            //Config Auto

            Game.OnUpdate += Game_OnUpdate;
            Interrupter.OnInterruptableSpell += Interrupter_OnInterruptableSpell;
            GameObject.OnCreate += OberAtack.GameObject_OnCreate;
            GameObject.OnDelete += OberAtack.GameObject_OnDelete;
        }

        private static void Interrupter_OnInterruptableSpell(Obj_AI_Base sender, Interrupter.InterruptableSpellEventArgs e)
        {
            var qtarget = TargetSelector.GetTarget(SpellsManager.Q.Range, DamageType.Mixed);
            if (LLogic["InterrupterSpell"].Cast<CheckBox>().CurrentValue && SpellsManager.Q.IsReady() && SpellsManager.Q.GetPrediction(qtarget).HitChance >= HitChance.High)
            {
                if (sender.Distance(_player.ServerPosition, true) <= SpellsManager.Q.Range && SpellsManager.Q.GetPrediction(qtarget).HitChance >= HitChance.High)
                {
                    SpellsManager.Q.Cast(qtarget);
                }
            }
        }

        private static void Game_OnUpdate(EventArgs args)
        {
            if (LJungleSteal["JSR"].Cast<CheckBox>().CurrentValue)
            {
                var target_monster = EntityManager.MinionsAndMonsters.Monsters.Where(x => Player.Instance.Distance(x) < 3340 && !x.BaseSkinName.ToLower().Contains("mini")).LastOrDefault();
                if (target_monster != null)


                    if (Player.Instance.IsDead) { return; }

                var target_champion = TargetSelector.GetTarget(SpellsManager.R.Range, DamageType.Physical);


                if (SpellsManager.R.IsReady())
                {
                    if (LKilSteal["KR"].Cast<CheckBox>().CurrentValue)
                    {
                        if (target_champion != null &&
                        SpellsManager.R.IsInRange(target_champion) &&
                        target_champion.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Ignite))
                        {
                            SpellsManager.R.Cast(target_champion);
                        }

                        if (LJungleSteal["JSR"].Cast<CheckBox>().CurrentValue)
                        {
                            if (target_monster != null &&
                            SpellsManager.R.IsInRange(target_monster) &&
                            (target_monster.BaseSkinName.Contains("Dragon") || target_monster.BaseSkinName.Contains("Herald") || target_monster.BaseSkinName.Contains("Baron")) &&
                            target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                            {
                                SpellsManager.R.Cast(target_monster);
                            }
                            if (LJungleSteal["BuffBlue"].Cast<CheckBox>().CurrentValue)
                                if (target_monster != null &&
                            SpellsManager.R.IsInRange(target_monster) &&
                            (target_monster.BaseSkinName.Contains("Blue")) &&
                            target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                                {
                                    SpellsManager.R.Cast(target_monster);
                                }
                            if (LJungleSteal["BuffRed"].Cast<CheckBox>().CurrentValue)
                                if (target_monster != null &&
                            SpellsManager.R.IsInRange(target_monster) &&
                            (target_monster.BaseSkinName.Contains("Red")) &&
                            target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                                {
                                    SpellsManager.R.Cast(target_monster);
                                }

                            if (target_champion != null && target_monster != null &&
                                SpellsManager.R.IsInRange(target_monster) &&
                                target_monster.Distance(target_champion) <= LJungleSteal["QR"].Cast<Slider>().CurrentValue &&
                                Player.Instance.Distance(target_champion) <= LJungleSteal["QR"].Cast<Slider>().CurrentValue &&
                                target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                            {
                                SpellsManager.R.Cast(target_monster);
                            }
                        }
                    }
                }
            }
        }
    }
}