using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class StrategicShockwave : MonoBehaviour
    {
        BomberPlayer me = new BomberPlayer();
        List<BomberPlayer> players = new List<BomberPlayer>();
        public void StrategicShockwaveCheat()
        {
            GetPlayers();

            if (players.Count <= 0 || me == null) return;

            me.bombRange = 15;
            me.BombsRemaining = 25;

        }



        void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<BomberPlayer>());

            foreach (BomberPlayer ply in players)
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
