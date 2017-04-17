using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu.Values;

namespace ShuLux
{

    class Program
    {
        

    static void Main(string[] args)
        {
            Loading.OnLoadingComplete += Loading_OnLoadingComplete;
        }

        private static void Loading_OnLoadingComplete(EventArgs args)
        {
            
            Chat.Print("<font color='#04B404'>By </font><font color='#3737E6'>Shurima</font><font color='#D61EBE'>7</font><font color='#FF0000'> <3 </font>");
            if (Player.Instance.ChampionName != "Lux") return;
            SpellsManager.InitializeSpells();
            Menus.MyMenu();
            ModeesManager.InitializeModes();
            AutoLevel.AutoMenu();
            Drawing.OnDraw += Drawings.OnDraw;

        }
    }
}
