using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class SpookySpikes : MonoBehaviour
    {
        SpookySpikesPlayer me = new SpookySpikesPlayer();
        List<SpookySpikesPlayer> players = new List<SpookySpikesPlayer>();
        List<GameObject> spikes = new List<GameObject>();
        public void SpookySpikesCheat()
        {

            GetPlayers();
            if (players.Count > 0 && me != null)
            {
                GetSpikes();

                GameObject spike = ShouldAct();

                if (spike == null)
                    return;

                int curState = (int)typeof(SpookySpikesPlayer).GetField("curState", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me);

                if (curState == 0)
                {

                    if (spike.transform.position.y > 0.75f)
                    {
                        me.StartCoroutine("Crouch");

                    }
                    else
                    {
                        me.StartCoroutine("Jump");
                    }
                }


            }

        }



        void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<SpookySpikesPlayer>());


            foreach (SpookySpikesPlayer ply in players)
            {
                //if (players[i].GamePlayer.Name == Main.PlayerName)
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply;
                }
            }

            return;

        }

        void GetSpikes()
        {
            spikes.Clear();

            foreach (var gameObj in FindObjectsOfType(typeof(GameObject)) as GameObject[])
            {
                if (gameObj.name == "HitCollider")
                {
                    spikes.Add(gameObj);
                }
            }

            return;
        }

        GameObject ShouldAct()
        {
            for (int i = 0; i < spikes.Count; i++)
            {
                float distance = (spikes[i].transform.position.x - me.transform.position.x);

                if (distance > -3 && distance < 0)
                {
                    return spikes[i];
                }
            }

            return null;
        }
    }
}
