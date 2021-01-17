using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class Template : MonoBehaviour
    {
        BomberPlayer me = new BomberPlayer();
        List<BomberPlayer> players = new List<BomberPlayer>();
        public void StrategicShockwaveCheat()
        {
            GetPlayers();

            if (players.Count <= 0 || me == null) return;

            /* Get Private Variable Using Reflection 
            Type myClassType = me.GetType();
            PropertyInfo myPropertyInfo = myClassType.GetProperty("maxPlaceRange");
            int x = (int)myPropertyInfo.GetValue(me, null);
            Debug.Log(x);
            myPropertyInfo.SetValue(me, 123, null);
            */

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
