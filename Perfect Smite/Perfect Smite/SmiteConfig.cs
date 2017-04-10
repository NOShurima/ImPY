using System;
using EloBuddy;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using EloBuddy.SDK;
using System.Linq;
using EloBuddy.SDK.Rendering;
using SharpDX;
using SharpDX.Direct3D9;

namespace Perfect_Smite
{
    internal class SmiteConfig
    {
        private static SpellSlot Smite = SpellSlot.Unknown;

        static string[] Monster_List = new string[] {
            "SRU_Dragon_Water",
            "SRU_Dragon_Fire",
            "SRU_Dragon_Earth",
            "SRU_Dragon_Air",
            "SRU_Dragon_Elder",
            "SRU_RiftHerald",
            "SRU_Baron", "Sru_Crab",
            "SRU_Krug",
            "SRU_Red",
            "SRU_Razorbeak",
            "SRU_Murkwolf",
            "SRU_Blue",
            "SRU_Gromp" };

        private const float SmiteRange = 600f;
        static Font DrawFont = new Font(Drawing.Direct3DDevice, new System.Drawing.Font("Obrigado", 15, System.Drawing.FontStyle.Bold));
        private static Menu Menu, ConfigSmite, SmiteDraw, StealSmite, MonsSmite;
        public SmiteConfig()
        {
            new MonstersList();
    
        }

        internal class Load
        {
            public Load()
            {
                if (SpellSmite.Smite == null) { return; }
                //
                Chat.Print("Addon is in Beta");
                //Menu  
                Menu = MainMenu.AddMenu("Perfect Smite", "Smite");
                Menu.AddLabel("Config Combo Chapion");
                Menu.Add("Combo", new CheckBox("Enbaled in Combo Mode", false));
                Menu.Add("Mode Smite", new CheckBox("Use Smite Enemys"));
                Menu.Add("Mode Smite2", new CheckBox("Use Smite MobsJungle", true));
                Menu.AddLabel("Config Lag or Daley");
                Menu.Add("min", new Slider("Min. Delay (ms)", 1, 0, 1000));
                Menu.Add("max", new Slider("Max. Delay (ms)", 249, 0, 1000));
                Menu.Add("ConfigKeyV", new KeyBind("KeySmite", false, KeyBind.BindTypes.HoldActive, 'V'));
                Menu.Add("ConfigKeyv2", new KeyBind("KeySmite", false, KeyBind.BindTypes.HoldActive, 32));
                Menu.AddLabel("Config Draw Smite");
                Menu.Add("Range", new Slider("CheckStrut", 1000, 0, 1700));
                //
                ConfigSmite = Menu.AddSubMenu("Smite");
                ConfigSmite.AddLabel("Config Barão");
                ConfigSmite.Add("SmiteMons", new CheckBox("Smite Baron"));
                ConfigSmite.AddLabel("Smite Dragon Config");
                ConfigSmite.Add("SmiteMons1", new CheckBox("Smite Dragon Whater"));
                ConfigSmite.Add("SmiteMons2", new CheckBox("Smite Dragon Fire"));
                ConfigSmite.Add("SmiteMons3", new CheckBox("Smite Dragon Air"));
                ConfigSmite.Add("SmiteMons4", new CheckBox("Smite Dragon Earth"));
                ConfigSmite.Add("SmiteMons5", new CheckBox("Smite Dragon Elder"));
                ConfigSmite.Add("SmiteMons6", new CheckBox("Smite Dragon Purce"));
                ConfigSmite.AddLabel("Config Jungle Enemy");
                ConfigSmite.Add("SmiteMons9", new CheckBox("Steal Blue Enemy"));
                ConfigSmite.Add("SmiteMons10", new CheckBox("Steal Red Enemy"));
                ConfigSmite.AddLabel("ConfigHP");
                ConfigSmite.Add("Hp.Champ", new Slider("ConfigHP", 50, 0, 100));
                ConfigSmite.AddLabel("Smite Enemy Hp");
                ConfigSmite.Add("KillSteal", new CheckBox("Steal Smite Enemy"));
                //
                MonsSmite = Menu.AddSubMenu("Smite Monster");
                MonsSmite.Add("MonsBlue", new CheckBox("Smite Blue", true));
                MonsSmite.Add("MonsRed", new CheckBox("Smite Red", true));
                MonsSmite.Add("MonsWolf", new CheckBox("Smite Worlf", false));
                MonsSmite.Add("MonsGromp", new CheckBox("Smite Gromp",false));
                MonsSmite.Add("MonsCrap", new CheckBox("Smite Crap",false));
                MonsSmite.Add("MonsKrug", new CheckBox("Smite Krug", false));
                MonsSmite.Add("MonsRazorbeak", new CheckBox("Smite Razorbeak", false));
                MonsSmite.AddLabel("This Addon came with a small Bug No Red And No Blue"); 
                MonsSmite.AddLabel("so I recommend getting off near the red or the blue if you offer the blue or red to some Ally");
                //
                SmiteDraw = Menu.AddSubMenu("Smite Draw");
                SmiteDraw.Add("DrawRage", new CheckBox("Draw Rage Smite"));
                SmiteDraw.AddLabel("Credtis Designer Nebula Smite");
                //StealSmite
                StealSmite = Menu.AddSubMenu("KillSteal");
                StealSmite.Add("KE", new CheckBox("Use Smite KillSteal Enemys"));



                Drawing.OnDraw += Game_OnDraw;
                Game.OnUpdate += Game_OnUpdate;


            }

            private void Game_OnUpdate(EventArgs args)
            {
                if (Player.Instance.IsDead) { return; }

                var target_champion = TargetSelector.GetTarget(SpellSmite.Smite.Range, DamageType.Physical);
                var target_monster = EntityManager.MinionsAndMonsters.Monsters.Where(x => Player.Instance.Distance(x) < 1000 && !x.BaseSkinName.ToLower().Contains("mini")).LastOrDefault();

                if (SpellSmite.Smite.IsReady())
                {
                    if (target_champion != null &&
                        SpellSmite.Smite.IsInRange(target_champion) &&
                        target_champion.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                    {
                        SpellSmite.Smite.Cast(target_champion);
                    }

                    if (target_monster != null &&
                        SpellSmite.Smite.IsInRange(target_monster) &&
                        (target_monster.BaseSkinName.Contains("Dragon") || target_monster.BaseSkinName.Contains("Herald") || target_monster.BaseSkinName.Contains("Baron") || target_monster.BaseSkinName.Contains("Blue") || target_monster.BaseSkinName.Contains("Red")) &&
                        target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                    {
                        SpellSmite.Smite.Cast(target_monster);
                    }

                    if (target_champion != null && target_monster != null &&
                        SpellSmite.Smite.IsInRange(target_monster) &&
                        target_monster.Distance(target_champion) <= Menu["Range"].Cast<Slider>().CurrentValue &&
                        Player.Instance.Distance(target_champion) <= Menu["Range"].Cast<Slider>().CurrentValue &&
                        target_monster.Health <= Player.Instance.GetSummonerSpellDamage(target_champion, DamageLibrary.SummonerSpells.Smite))
                    {
                        SpellSmite.Smite.Cast(target_monster);
                    }
                }

   

                    if (Menu["ConfigKeyv2"].Cast<KeyBind>().CurrentValue)
                    {
                        if (SpellSmite.Smite.IsReady())
                        {
                            if (target_champion != null && Menu["Mode Smite"].Cast<CheckBox>().CurrentValue && SpellSmite.Smite.IsInRange(target_champion))
                            {
                                if (target_champion.HealthPercent <= ConfigSmite["Hp.Champ"].Cast<Slider>().CurrentValue)
                                {
                                    SpellSmite.Smite.Cast(target_champion);
                                }
                            }
                        }
                    }

                    if (Menu["ConfigKeyV"].Cast<KeyBind>().CurrentValue)
                    {
                        if (SpellSmite.Smite.IsReady())
                        {
                            if (target_monster != null && Menu["Mode Smite2"].Cast<CheckBox>().CurrentValue && SpellSmite.Smite.IsInRange(target_monster))
                            {
                                SpellSmite.Smite.Cast(target_monster);
                            }
                        }
                    }
                }
            


            private void Game_OnDraw(EventArgs args)
            {
                if (MainMenu.IsVisible)
                {
                    var target_monster = EntityManager.MinionsAndMonsters.Monsters.Where(x => Player.Instance.Distance(x) < 1000 && !x.BaseSkinName.ToLower().Contains("mini")).LastOrDefault();

                    if (target_monster != null)
                    {
                        Circle.Draw(Color.BlueViolet, Menu["Draw.CheckRange"].Cast<Slider>().CurrentValue, target_monster.Position);
                        Circle.Draw(Color.BlueViolet, Menu["Draw.CheckRange"].Cast<Slider>().CurrentValue, Player.Instance.Position);
                    }
                }

                if (SmiteDraw["DrawRage"].Cast<CheckBox>().CurrentValue)
                {
                    Drawing.DrawCircle(Player.Instance.Position, SpellSmite.Smite.Range, System.Drawing.Color.BlueViolet);
                }

                }
            }
               private static bool Killable(AIHeroClient target)
            {
                if (target == null || target.IsDead || target.Health <= 0)
                {
                    return true;
                }

                if (target.HasBuff("KindredRNoDeathBuff"))
                {
                    return true;
                }

                if (target.HasBuff("UndyingRage") && target.GetBuff("UndyingRage").EndTime - Game.Time > 0.3 && target.Health <= target.MaxHealth * 0.10f)
                {
                    return true;
                }

                if (target.HasBuff("JudicatorIntervention"))
                {
                    return true;
                }

                if (target.HasBuff("ChronoShift") && target.GetBuff("ChronoShift").EndTime - Game.Time > 0.3 && target.Health <= target.MaxHealth * 0.10f)
                {
                    return true;
                }

                if (target.HasBuff("VladimirSanguinePool"))
                {
                    return true;
                }

                if (target.HasBuff("ShroudofDarkness"))
                {
                    return true;
                }

                if (target.HasBuff("SivirShield"))
                {
                    return true;
                }

                if (target.HasBuff("itemmagekillerveil"))
                {
                    return true;
                }

                return target.HasBuff("FioraW");
            }
        }
    }

