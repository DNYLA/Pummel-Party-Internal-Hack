using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class ThunderTrench : MonoBehaviour 
    {
        LightningPlayer me = new LightningPlayer();
        List<LightningPlayer> players = new List<LightningPlayer>();
        bool holderSet = true;
        public void ThunderTrenchCheat()
        {
            GetPlayers();

            if (players.Count <= 0 || me == null) return;

            if (!holderSet)
            {
                me.LightningHolderRecieve(true);
                SetHolder();
            }


            //Get Private Variable Using Reflection
            //Type myClassType = me.GetType();
            //PropertyInfo myPropertyInfo = myClassType.GetProperty("lightningTimer");
            //ActionTimer x = (ActionTimer)myPropertyInfo.GetValue(me, null);
            //Debug.Log(x.last.ToString());
            //myPropertyInfo.SetValue(me, true, null);


        }

        private void SetHolder()
        {
            foreach (LightningPlayer ply in players)
            {
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    ply.LightningHolderRecieve(true);
                }
                else
                {
                    ply.LightningHolderRecieve(false); //Wont Do Anything Yet
                }
            }



            holderSet = true;
        }

        void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<LightningPlayer>());


            foreach (LightningPlayer ply in players)
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
