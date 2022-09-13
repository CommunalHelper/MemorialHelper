using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.MemorialHelper {
    [TrackedAs(typeof(HeartGem))]
    [CustomEntity("MemorialHelper/FlagCrystalHeart")]
    public class FlagCrystalHeart : Entity {
        private readonly HeartGem HeartSummon;
        private readonly string flag;

        private bool active = false;

        public FlagCrystalHeart(EntityData data, Vector2 offset) 
            : base(data.Position + offset) {
            HeartSummon = new HeartGem(data, offset);
            flag = data.Attr("flag");
        }

        private Session ThisSession => SceneAs<Level>().Session;

        public override void Added(Scene scene) {
            base.Added(scene);
            UpdateSummon();
        }

        public override void Update() {
            base.Update();
            UpdateSummon();
        }

        private void UpdateSummon() {
            if (ThisSession.HeartGem) {
                HeartSummon.RemoveSelf();
                RemoveSelf();
            }
            else if (ThisSession.GetFlag(flag) != active) {
                if (!active && HeartSummon.Scene == null) {
                    Scene.Add(HeartSummon);
                }

                active = HeartSummon.Active = HeartSummon.Visible = HeartSummon.Collidable = !active;
            }
        }
    }
}