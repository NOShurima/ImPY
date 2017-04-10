using EloBuddy.SDK.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Perfect_Smite
{
    class Program
    {
        static void Main(string[] args)
        {
            Loading.OnLoadingComplete += SmiteComplete;
        }

        private static void SmiteComplete(EventArgs args)
        {
            new SmiteConfig.Load();
            new SpellSmite();
            new MonstersList();
        }
    }
}
