using PummelPartyHack.Board;
using PummelPartyHack.Minigames;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace PummelPartyHack
{
    public class Main : MonoBehaviour
    {

        private static Texture2D aaLineTex = null;
        private static Texture2D lineTex = null;
        public static Texture2D texture;
        public static Texture2D inactivetexture;
        public static Texture2D Activetexture;
        public static Texture2D texture2;
        public static Color HoverColor = new Color32(149, 0, 1, 255);
        private static Material blitMaterial = null;
        private static Material blendMaterial = null;
        private static Rect lineRect = new Rect(0f, 0f, 1f, 1f);
        public static Rect rc = new Rect(Screen.width - 450, 100, 350, 220);
        public static Rect itemRC = new Rect(50, 250, 350, 220);
        public static Color Color3 = new Color32(0, 149, 1, 255);
        public void Start()
        {
            texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture.SetPixel(0, 0, Color.red);
            texture.SetPixel(1, 0, Color.red);
            texture.SetPixel(0, 1, Color.red);
            texture.SetPixel(1, 1, Color.red);
            texture.Apply();


            inactivetexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            inactivetexture.SetPixel(0, 0, HoverColor);
            inactivetexture.SetPixel(1, 0, HoverColor);
            inactivetexture.SetPixel(0, 1, HoverColor);
            inactivetexture.SetPixel(1, 1, HoverColor);
            inactivetexture.Apply();

            texture2 = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            texture2.SetPixel(0, 0, Color.green);
            texture2.SetPixel(1, 0, Color.green);
            texture2.SetPixel(0, 1, Color.green);
            texture2.SetPixel(1, 1, Color.green);
            texture2.Apply();

            Activetexture = new Texture2D(2, 2, TextureFormat.ARGB32, false);
            Activetexture.SetPixel(0, 0, Color3);
            Activetexture.SetPixel(1, 0, Color3);
            Activetexture.SetPixel(0, 1, Color3);
            Activetexture.SetPixel(1, 1, Color3);
            Activetexture.Apply();

            if (lineTex == null)
            {
                lineTex = new Texture2D(1, 1, TextureFormat.ARGB32, false);
                lineTex.SetPixel(0, 1, UnityEngine.Color.white);
                lineTex.Apply();
            }
            if (aaLineTex == null)
            {
                aaLineTex = new Texture2D(1, 3, TextureFormat.ARGB32, false);
                aaLineTex.SetPixel(0, 0, new UnityEngine.Color(1, 1, 1, 0));
                aaLineTex.SetPixel(0, 1, UnityEngine.Color.white);
                aaLineTex.SetPixel(0, 2, new UnityEngine.Color(1, 1, 1, 0));
                aaLineTex.Apply();
            }

            blitMaterial = (Material)typeof(GUI).GetMethod("get_blitMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
            blendMaterial = (Material)typeof(GUI).GetMethod("get_blendMaterial", BindingFlags.NonPublic | BindingFlags.Static).Invoke(null, null);
        }

        public static void DrawLine(Vector2 pointA, Vector2 pointB, Color color, float width, bool antiAlias)
        {
            //
            float num = pointB.x - pointA.x; //-460
            float num2 = pointB.y - pointA.y; //-720

            float num3 = Mathf.Sqrt(num * num + num2 * num2); //532900000000

            if (num3 < 0.001f)
            {
                return;
            }

            Texture2D texture2D;
            if (antiAlias)
            {
                width *= 3f;
                texture2D = aaLineTex;
                Material material = blendMaterial;
            }
            else
            {
                texture2D = lineTex;
                Material material2 = blitMaterial;
            }
            float num4 = width * num2 / num3;
            float num5 = width * num / num3;
            Matrix4x4 identity = Matrix4x4.identity;
            identity.m00 = num;
            identity.m01 = -num4;
            identity.m03 = pointA.x + 0.5f * num4;
            identity.m10 = num2;
            identity.m11 = num5;
            identity.m13 = pointA.y - 0.5f * num5;
            GL.PushMatrix();
            GL.MultMatrix(identity);
            GUI.color = color;
            GUI.DrawTexture(lineRect, texture2D);
            GL.PopMatrix();
            GUI.color = Color.white;
        }
        public bool ssToggle = false;
        public bool llToggle = false;
        public bool bbToggle = false;
        public bool sandySToggle = false;
        public bool IEToggle = false;
        public bool mmToggle = false;
        public bool ddToggle = false;
        public bool sShockToggle = false;
        public bool ttToggle = false;

        bool debugSet = false;
        SpookySpikes ss = new SpookySpikes();
        CannonCircle cc = new CannonCircle();
        LaserLeap ll = new LaserLeap();
        BarnBrawl bb = new BarnBrawl();
        SandySearch sandySearch = new SandySearch();
        MysteryMaze mm = new MysteryMaze();
        DaringDogfight dd = new DaringDogfight();
        ItemExploit IE = new ItemExploit();
        StrategicShockwave sShock = new StrategicShockwave();
        ThunderTrench tt = new ThunderTrench();

        public void Update()
        {
            if (Input.GetKeyUp(KeyCode.Insert))
            {
                ShowMenu = !ShowMenu;
            }

            //Eject Whilst In-Game
            if (Input.GetKeyUp(KeyCode.End))
            {
                Loader.Unload();
            }

            if (!debugSet)
                GameManager.DEBUGGING = true;

            if (llToggle) ll.LaserLeapCheat();
            if (bbToggle) bb.BarnBrawlCheat();
            if (sandySToggle) sandySearch.SandySearchCheat();
            if (IEToggle) IE.ItemExploiter();
            if (mmToggle) mm.MysterMazeCheat();
            if (ddToggle) dd.DaringDogFightCheat();
            if (ssToggle) ss.SpookySpikesCheat();
            if (sShockToggle) sShock.StrategicShockwaveCheat();
            if (ttToggle) tt.ThunderTrenchCheat();

            //cc.CannonCircleCheat(); - BROKEN

            //allHidden = all.ToArray(typeof(GameObject)) as GameObject[];

        }

        private bool ShowMenu = true;
        GUIStyle ItemStyle = new GUIStyle();
        GUIStyle ActiveStyle = new GUIStyle();
        private void GI(int id)
        {
            ItemStyle = new GUIStyle(GUI.skin.toggle);
            //ItemStyle.font = new Font("Georgia");
            ItemStyle.fontStyle = FontStyle.Bold;
            ItemStyle.normal.textColor = Color.white;
            //ItemStyle.normal.background = texture2;
            ItemStyle.hover.textColor = Color.gray;
            //ItemStyle.hover.background = inactivetexture;
            //ItemStyle.active.background = inactivetexture;

            ActiveStyle = new GUIStyle(GUI.skin.toggle);
            ActiveStyle.fontStyle = FontStyle.Bold;
            ActiveStyle.normal.textColor = Color.red;
            //ActiveStyle.normal.background = texture2;
            ActiveStyle.hover.textColor = Color.gray;
            //ActiveStyle.hover.background = Activetexture;
            //ActiveStyle.active.background = Activetexture;

            int yPos = 25; //Auto Increment rather than manually do it

            int width = 150; //Set As Variable as it may change later

            bbToggle = GUI.Toggle(new Rect(15, yPos, width, 30), bbToggle, "Barn Brawl", ActiveStyle); yPos += 25;
            ddToggle = GUI.Toggle(new Rect(15, yPos, width, 30), ddToggle, "Daring Dogfight", ActiveStyle); yPos += 25;
            mmToggle = GUI.Toggle(new Rect(15, yPos, width, 30), mmToggle, "Mystery Maze", ActiveStyle); yPos += 25;
            sandySToggle = GUI.Toggle(new Rect(15, yPos, width, 30), sandySToggle, "Sandy Search", ActiveStyle); yPos += 25;
            ssToggle = GUI.Toggle(new Rect(15, yPos, width, 30), ssToggle, "Spooky Spikes", ActiveStyle); yPos += 25;
            sShockToggle = GUI.Toggle(new Rect(15, yPos, width, 30), sShockToggle, "Strat Shock", ActiveStyle); yPos += 25;
            IEToggle = GUI.Toggle(new Rect(15, yPos, width, 30), IEToggle, "Item Exploiter", ActiveStyle); yPos += 25;

            //Not Working
            /*
            llToggle = GUI.Toggle(new Rect(15, 125, 100, 30), llToggle, "Laser Leap", ActiveStyle);
            ttToggle = GUI.Toggle(new Rect(15, 200, 100, 30), IEToggle, "Thunder Trench", ActiveStyle);
            */
            //


        }

        string itemID = "1";
        string KeyAmm = "1";
        string healthAmm = "1";
        string node2tp = "1";
        string moveSpaces = "1";

        private void ItemGUI(int id)
        {

            ItemStyle = new GUIStyle(GUI.skin.toggle);
            //ItemStyle.font = new Font("Georgia");
            ItemStyle.fontStyle = FontStyle.Bold;
            ItemStyle.normal.textColor = Color.white;
            //ItemStyle.normal.background = texture2;
            ItemStyle.hover.textColor = Color.gray;
            //ItemStyle.hover.background = inactivetexture;
            //ItemStyle.active.background = inactivetexture;

            ActiveStyle = new GUIStyle(GUI.skin.toggle);
            ActiveStyle.fontStyle = FontStyle.Bold;
            ActiveStyle.normal.textColor = Color.red;
            //ActiveStyle.normal.background = texture2;
            ActiveStyle.hover.textColor = Color.gray;
            //ActiveStyle.hover.background = Activetexture;
            //ActiveStyle.active.background = Activetexture;

            int yPos = 25; //Auto Increment rather than manually do it

            int width = 150; //Set As Variable as it may change later



            itemID = GUI.TextField(new Rect(10, yPos, 75, 20), itemID, 2);
            if (GUI.Button(new Rect(100, yPos, 85, 25), "Give Item"))
                IE.GiveItem(itemID);
            yPos += 30;

            KeyAmm = GUI.TextField(new Rect(10, yPos, 75, 20), KeyAmm, 4);
            if (GUI.Button(new Rect(100, yPos, 85, 25), "Give Keys"))
                IE.GiveKey(KeyAmm);
            yPos += 30;

            healthAmm = GUI.TextField(new Rect(10, yPos, 75, 20), healthAmm, 4);
            if (GUI.Button(new Rect(100, yPos, 85, 25), "Give Health"))
                IE.GiveHealth(healthAmm);
            yPos += 30;

            node2tp = GUI.TextField(new Rect(10, yPos, 75, 20), node2tp, 2);
            if (GUI.Button(new Rect(100, yPos, 85, 25), "Teleport"))
                IE.TeleportOnMap(node2tp);
            yPos += 30;

            moveSpaces = GUI.TextField(new Rect(10, yPos, 75, 20), moveSpaces, 4);
            if (GUI.Button(new Rect(100, yPos, 85, 25), "Move"))
                IE.MoveForward(moveSpaces);

            //Not Working
            /*
            llToggle = GUI.Toggle(new Rect(15, 125, 100, 30), llToggle, "Laser Leap", ActiveStyle);
            ttToggle = GUI.Toggle(new Rect(15, 200, 100, 30), IEToggle, "Thunder Trench", ActiveStyle);
            */
            //


        }

        public void OnGUI()
        {
            //Initialize Cheat GUIS
            if (sandySToggle) sandySearch.DrawGUI();
            if (ddToggle) dd.DrawGUI();
            if (bbToggle) bb.DrawGUI();
            
            //Return if Menu is turned off
            if (!ShowMenu) return;

            rc = GUI.Window(0, rc, new GUI.WindowFunction(GI), "Pummel Party Hack v0.05 - Yerr");

            if (IEToggle)
                itemRC = GUI.Window(1, itemRC, new GUI.WindowFunction(ItemGUI), "Item Exploiter");

        }
    }
}
