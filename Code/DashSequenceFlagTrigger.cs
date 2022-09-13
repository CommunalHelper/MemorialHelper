using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System.Collections.Generic;

namespace Celeste.Mod.MemorialHelper {
    [Tracked(false)]
    [CustomEntity("MemorialHelper/DashSequenceFlagTrigger")]
    public class DashSequenceFlagTrigger : Trigger {
        //<summary>
        //Attributes:

        //repeatable: When true, you can toggle the flag more than once by repeating sequence
        //persistent: When false, The flag linked to the trigger will reset to 'false' upon exiting the screen
        //</summary>

        private Session ThisSession => SceneAs<Level>().Session;

        private List<int> dashList = new List<int> { };
        private int currentPoint = 0;

        private bool triggered = false;

        private readonly bool repeatable;
        private readonly bool persistent;

        private readonly string flag;



        public DashSequenceFlagTrigger(EntityData data, Vector2 offset) : base(data, offset) {

            SetList(data.Attr("sequence"));
            flag = data.Attr("flag");

            repeatable = data.Bool("repeatable");
            persistent = data.Bool("persistent");

            Add(new DashListener { OnDash = OnDash });
        }

        public override void Removed(Scene scene) {
            if (!persistent) {
                ThisSession.SetFlag(flag, false);
            }
            base.Removed(scene);
        }

        public override void Added(Scene scene) {
            base.Added(scene);

            if (persistent && ThisSession.GetFlag(flag + "_dashFlag")) {
                triggered = true;
            }
        }

        public void OnDash(Vector2 vect) {

            if (!triggered && PlayerIsInside) {
                //convert dash direction to 0-7 value
                //0 is straight left, increases clockwise
                // 123
                // 0 4
                // 765
                int dir = ((int)(vect.Angle().ToDeg() + 202.5)) / 45 % 8;

                if (currentPoint >= dashList.Count || dashList[currentPoint] == dir) {
                    currentPoint++;

                    if (currentPoint >= dashList.Count) {
                        currentPoint = 0;
                        Trigger();
                    }
                } else {
                    //If the the dash is incorrect, shift the counter back until things align
                    //eg. if the solution is 1231234, and you dash 1231231, the counter should
                    //		go from 123123 to 1231 instead of to the first 1.
                    int shift = 0;
                    int matchAt = currentPoint - 1;
                    while (matchAt >= 0) {
                        shift++;
                        if (dir == dashList[matchAt]) {
                            matchAt--;
                            while (matchAt >= 0) {
                                if (dashList[matchAt] != dashList[matchAt + shift]) {
                                    matchAt = --currentPoint - 1;
                                    break;
                                }
                                matchAt--;
                            }
                        } else {
                            matchAt = --currentPoint - 1;
                        }
                    }
                }
            }
        }

        private void Trigger() {
            if (repeatable) {
                ThisSession.SetFlag(flag, !ThisSession.GetFlag(flag));
            } else if (!triggered) {
                triggered = true;
                ThisSession.SetFlag(flag);

                if (persistent) {
                    ThisSession.SetFlag(flag + "_dashFlag");
                }

            }
        }

        private void SetList(string input) {
            foreach (char symbol in input.ToCharArray()) {
                //only add numbers 0-7.
                if (symbol >= '0' && symbol <= '7') {
                    dashList.Add(symbol - '0');
                }
            }
        }
    }
}