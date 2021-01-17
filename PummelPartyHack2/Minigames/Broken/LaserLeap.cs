using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class LaserLeap : MonoBehaviour
    {
        LaserLeapPlayer me = new LaserLeapPlayer();
        List<LaserLeapPlayer> players = new List<LaserLeapPlayer>();
       // List<GameObject> spikes = new List<GameObject>();
        public void LaserLeapCheat()
        {
            

            GetPlayers();
            if (players.Count > 0 && me != null)
            {
                me.Score += 1;
                me.IsInvulnerable = true;
            }

        }



        void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<LaserLeapPlayer>());

            foreach (LaserLeapPlayer ply in players)
            {
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply;
                }
            }

            return;

        }
    }
}
