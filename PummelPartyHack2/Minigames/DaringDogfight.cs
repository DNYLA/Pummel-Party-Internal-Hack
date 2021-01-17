using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class DaringDogfight : MonoBehaviour
    {
        List<PlanesPlayer> players = new List<PlanesPlayer>();
        PlanesPlayer me;
        public void DaringDogFightCheat()
        {
            GetPlayers();

            if (players.Count <= 0)
                return;

            if (me.IsDead || me == null)
                return;



        }

        public void DrawGUI()
        {
            //Vector3 pW2s = WorldToScreen 

            float distance = 1000f;
            float tempDistance;


            foreach (PlanesPlayer ply in players)
            {
                if (ply.name == "Dan")
                    continue;

                Vector3 w2s = Camera.current.WorldToScreenPoint(ply.transform.position);

                Main.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(w2s.x, Screen.height - w2s.y), Color.red, 1.5f, false);
                //tempDistance = Vector3.Distance(me.transform.position, ply.transform.position);
            }

        }

        private void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<PlanesPlayer>());

            foreach (PlanesPlayer ply in players)
            {
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply; 
                    break;
                }
            }
        }
    }
}
