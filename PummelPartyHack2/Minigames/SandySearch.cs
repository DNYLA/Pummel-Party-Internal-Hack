using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack.Minigames
{
    public class SandySearch : MonoBehaviour // SandySearch = TreasureHunt
    {
        TreasureHuntPlayer me = new TreasureHuntPlayer();
        List<TreasureHuntPlayer> players = new List<TreasureHuntPlayer>();
        public void SandySearchCheat()
        {
            
            GetPlayer();
            GetGems();
            //me.transform.position = players[2].transform.position;
            //gems[0].transform.position = me.transform.position;
        }

        private void GetPlayer()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<TreasureHuntPlayer>());

            foreach (TreasureHuntPlayer ply in players)
            {
                if (ply.GamePlayer.IsLocalPlayer && !ply.GamePlayer.IsAI)
                {
                    me = ply;
                }
            }
        }

        private void GetGems()
        {
            //TreasureChest tChest = FindObjectOfType<TreasureChest>();

            gems.Clear();
            gems.AddRange(FindObjectsOfType<TreasureHuntCollectible>());
            
            

            //curState = (int)typeof(TreasureHuntCollectible).GetField("active", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me);

            //foreach(TreasureHuntCollectible go in FindObjectsOfType<TreasureHuntCollectible>())
            //{
            //        if (go == null) continue;

            //        gems.Add(go);
            //}

            //for (int i = 0; i < gems.Count; i++)
            //{
            //    if (gems[i].name != "Collectible")
            //    {
            //        gems.Remove(gems[i]);
            //    }
            //   // posR[i] = Camera.main.WorldToScreenPoint(gems[i].transform.position);

            //    //var rend2 = gems[i].GetComponentsInChildren<Renderer>();
           
            //}
            

        }

        List<TreasureHuntCollectible> gems = new List<TreasureHuntCollectible>();
        List<float> pos = new List<float>();
        List<float> posY = new List<float>();
        List<Vector3> posR = new List<Vector3>();


        public void DrawGUI()
        {
            if (gems.Count <= 0) return;

            TreasureHuntController tt  = FindObjectOfType<TreasureHuntController>();

            if (!tt.introStarted && !tt.introFinished) return;
            //if (tt.introStarted && !tt.introFinished) return;

            GUI.Label(new Rect(450, 200, 100, 30), gems.Count.ToString());

            float distance = 55f;
            float tempDistance;
            TreasureHuntCollectible closest = new TreasureHuntCollectible();

            foreach (TreasureHuntCollectible gem in gems)
            {
                if (gem.transform != null && gem.transform.position != null)
                {
                    tempDistance = Vector3.Distance(me.transform.position, gem.transform.position);

                    if (distance > tempDistance)
                    {
                        distance = tempDistance;
                        closest = gem;
                    }

                    tempDistance = Vector3.Distance(me.transform.position, gem.transform.position);
                    Vector3 w2s = Camera.current.WorldToScreenPoint(gem.transform.position);
                    
                    if (w2s == null) continue;
                    if (w2s.z > -1)
                    {
                        GUI.Label(new Rect(w2s.x, Screen.height - w2s.y, 100, 30), gem.gameObject.name);
                        
                    }
                }
            }

            Vector3 w3s = Camera.current.WorldToScreenPoint(closest.transform.position);
            Vector3 pw2s = Camera.current.WorldToScreenPoint(me.transform.position);
            if (w3s.z > -1)
            {
                GUI.color = Color.blue;
                GUI.Label(new Rect(w3s.x, Screen.height - w3s.y, 100, 30), closest.gameObject.name);
                //Main.DrawLine(new Vector2(Screen.width / 2 - 50, Screen.height - 50), new Vector2(w3s.x, Screen.height - w3s.y), Color.blue, 1f, false);
                Main.DrawLine(new Vector2(pw2s.x, pw2s.y + 300), new Vector2(w3s.x, Screen.height - w3s.y), Color.blue, 1f, false);
            }

            TreasureHuntTreasure treasure = FindObjectOfType<TreasureHuntTreasure>();
            w3s = Camera.current.WorldToScreenPoint(treasure.transform.position);

            if (w3s.z > -1)
            {
                GUI.color = Color.red;
                GUI.Label(new Rect(w3s.x, Screen.height - w3s.y, 100, 30), "[Treasure]");
                GUI.color = Color.white;
            }
        }

    }
}
