using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using ZP.Utility;

namespace PummelPartyHack.Minigames
{
    public class CannonCircle : MonoBehaviour
    {
        CannonCirclePlayer me = new CannonCirclePlayer();
        List<CannonCirclePlayer> players = new List<CannonCirclePlayer>();
        List<Transform> ccProjectiles = new List<Transform>();
        public void CannonCircleCheat()
        {
            GetPlayers();

            if (players.Count > 0 && me != null)
            {
                CharacterMoverInput input = GetProjectiles();
                Vector2 axis;
 
                //axis = (this.player.RewiredPlayer.GetAxis(InputActions.Horizontal), this.player.RewiredPlayer.GetAxis(InputActions.Vertical));
                //input = new CharacterMoverInput(axis, false, false);

                CharacterMover mover = (CharacterMover)typeof(CharacterMover).GetField("mover", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me); ;
                mover.CalculateVelocity(input, Time.deltaTime);
                mover.DoMovement(Time.deltaTime);
                if (mover.MovementAxis != Vector2.zero)
                {
                    base.transform.rotation = Quaternion.RotateTowards(base.transform.rotation, Quaternion.LookRotation(new Vector3(mover.MovementAxis.x, 0f, mover.MovementAxis.y), Vector3.up), 1500f * Time.deltaTime);
                }
                    //base.Do

                //me.transform = input;
            }
        }
        private CharacterMoverInput GetProjectiles()
        {
            CharacterMoverInput result = default(CharacterMoverInput);
            Vector2 curAvoidanceVec = (Vector2)typeof(Vector2).GetField("curAvoidanceVec", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me);
            float num = 60f;
            float num2 = 9f;
            float num3 = 3.40282347E+38f;
            Transform transform = null;
            Vector3 vector = Vector3.zero;

            ccProjectiles.Clear();
            int i = 0;


            foreach (var projectile in FindObjectsOfType(typeof(Transform)) as Transform[])
            {
                if (projectile.name == "cannonCircleProjectiles")
                {
                    ccProjectiles.Add(projectile);
                }

                
            }

            foreach (Transform transform2 in ccProjectiles)
            {
                Vector3 vector2 = ZPMath.ProjectPointOnLineSegment(transform2.position, transform2.position + transform2.forward * num, base.transform.position);
                if (!vector2.Equals(transform2.position))
                {
                    float sqrMagnitude = (vector2 - base.transform.position).sqrMagnitude;
                    if (sqrMagnitude < num2)
                    {
                        float sqrMagnitude2 = (transform2.position - base.transform.position).sqrMagnitude;
                        if (sqrMagnitude2 < num3)
                        {
                            transform = transform2;
                            vector = vector2;
                            num3 = sqrMagnitude2;
                        }
                    }
                }
            }

            if (transform != null)
            {
                Vector3 vector3 = vector - base.transform.position;
               

                curAvoidanceVec = new Vector2(vector3.x, vector3.z);
                curAvoidanceVec.Normalize();
                Vector3 vector4 = base.transform.position - Vector3.zero;
                Vector2 vector5;
                vector5 = new Vector2(vector4.x, vector4.z);
                Vector2 normalized = vector5.normalized;
                float num4 = Vector3.Dot(normalized, curAvoidanceVec);
                float num5 = Vector3.Dot(normalized, -curAvoidanceVec);
                if (num5 > num4)
                {
                    curAvoidanceVec = -curAvoidanceVec;
                }
            }
            else
            {
                //Vector2 curAvoidanceVec = (Vector2)typeof(Vector2).GetField("curAvoidanceVec", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me);
                curAvoidanceVec = Vector2.zero;
            }
            float num6 = 0.36f;
            Vector2 vector6;
            vector6 = new Vector2(base.transform.position.x, base.transform.position.z);
            Vector2 vector7;
            Vector3 targetPosition = (Vector3)typeof(Vector3).GetField("targetPosition", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(me);
            vector7 = new Vector2(targetPosition.x, targetPosition.z);
            if ((vector7 - vector6).sqrMagnitude <= num6)
            {
               targetPosition = ZPMath.RandomPointInUnitSphere(GameManager.rand) * 10f;
            }
            Vector3 vector8 = targetPosition - base.transform.position;
            Vector2 vector9;
            vector9 = new Vector2(vector8.x, vector8.z);
            Vector2 vector10 = vector9.normalized;
            if (curAvoidanceVec != Vector2.zero)
            {
                vector10 *= 0.5f;
                vector10 += curAvoidanceVec;
                vector10.Normalize();
            }
            result = new CharacterMoverInput(vector10, false, false);
            return result;
        }

        private void GetPlayers()
        {
            players.Clear();
            players.AddRange(FindObjectsOfType<CannonCirclePlayer>());

            foreach (CannonCirclePlayer ply in players)
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
