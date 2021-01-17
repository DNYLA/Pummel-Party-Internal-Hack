using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class BarnBrawl : MonoBehaviour
    {
        BarnBrawlPlayer me = new BarnBrawlPlayer();
        public List<BarnBrawlPlayer> players = new List<BarnBrawlPlayer>();
        public List<BarnBrawlPlayer> enemies = new List<BarnBrawlPlayer>();
        // List<GameObject> spikes = new List<GameObject>();
        public void BarnBrawlCheat()
        {
            Debug.Log("Test3");
            GetPlayers();

            Debug.Log("Test2");

            if (players.Count > 0 && me != null)
            {
                //This will Teleport my Player to the Enemy which means that .transform.position is the corect position for enemy
                //me.transform.position = enemies[0].transform.position;

                

                if (BurstGun)
                {
                    me.minProjectiles = 99;
                    me.maxProjectiles = 100;
                }
                else
                {
                    me.minProjectiles = 7;
                    me.maxProjectiles = 10;
                }

                if (GodMode)
                    me.ApplyDamage(me, -10, new Vector3(0, 0, 0), 0, 0, 0);

                //me.Score += 1;
                if (InfiniteShotgun)
                    me.HoldingShotgun = true;
            }

        }



        void GetPlayers()
        {
            players.Clear();
            enemies.Clear();
            players.AddRange(FindObjectsOfType<BarnBrawlPlayer>());
            enemies.AddRange(FindObjectsOfType<BarnBrawlPlayer>());

            Debug.Log("Test");

            foreach (BarnBrawlPlayer ply in players)
            {

                //if (players[i].GamePlayer.Name == Main.PlayerName)
                //Debug.Log($"IsLocalPlayer: {ply.GamePlayer.IsLocalPlayer}; IsAi: {ply.GamePlayer.IsAI}; Name: {ply.GamePlayer.Name};");
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply;
                    enemies.Remove(me);
                }
            }

            return;
        }

        public bool GodMode = false;
        public bool BurstGun = false;
        public bool InfiniteShotgun = false;
        public void DrawGUI()
        {
            if (enemies.Count > 0)
            {
                GodMode = GUI.Toggle(new Rect(25, 500, 100, 30), GodMode, "God Mode");
                BurstGun = GUI.Toggle(new Rect(25, 525, 100, 30), BurstGun, "Burst Mode");
                InfiniteShotgun = GUI.Toggle(new Rect(25, 550, 100, 30), InfiniteShotgun, "Infinite Shotgun");

                
                foreach (BarnBrawlPlayer enemy in enemies)
                {
                    Vector3 w2s = Camera.current.WorldToScreenPoint(enemy.transform.position);

                    Main.DrawLine(new Vector2(Screen.width / 2, Screen.height), new Vector2(w2s.x, Screen.height - w2s.y), Color.green, 2f, false);
                }
            }
        }
    }
}
