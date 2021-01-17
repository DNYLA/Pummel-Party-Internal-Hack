using PummelPartyHack.Board;
using PummelPartyHack.Minigames;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PummelPartyHack.Data
{
    class MinigameHolder
    {
        SpookySpikes ss = new SpookySpikes();
        BarnBrawl bb = new BarnBrawl();
        DaringDogfight dd = new DaringDogfight();
        MysteryMaze mm = new MysteryMaze();
        SandySearch sandySearch = new SandySearch();
        LaserLeap ll = new LaserLeap();
        ItemExploit IE = new ItemExploit();
        StrategicShockwave sShock = new StrategicShockwave();
        ThunderTrench tt = new ThunderTrench();

        public bool ssToggle = false;
        public bool llToggle = false;
        public bool bbToggle = false;
        public bool sandySToggle = false;
        public bool IEToggle = false;
        public bool mmToggle = false;
        public bool ddToggle = false;
        public bool sShockToggle = false;
        public bool ttToggle = false;


        public void StartCheats()
        {

            if (llToggle) ll.LaserLeapCheat();
            if (bbToggle) bb.BarnBrawlCheat();
            if (sandySToggle) sandySearch.SandySearchCheat();
            if (IEToggle) IE.ItemExploiter();
            if (mmToggle) mm.MysterMazeCheat();
            if (ddToggle) dd.DaringDogFightCheat();
            if (ssToggle) ss.SpookySpikesCheat();
            if (sShockToggle) sShock.StrategicShockwaveCheat();
            if (ttToggle) tt.ThunderTrenchCheat();
        }

    }
}
