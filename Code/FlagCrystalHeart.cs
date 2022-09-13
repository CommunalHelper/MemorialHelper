using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;

namespace Celeste.Mod.MemorialHelper {
    [TrackedAs(typeof(HeartGem))]
    [CustomEntity("MemorialHelper/FlagCrystalHeart")]
    public class FlagCrystalHeart : Entity {
        private Session ThisSession => SceneAs<Level>().Session;
        private HeartGem HeartSummon;

        private readonly string flag;

        private bool summoned = false;
        private bool active = false;

        public FlagCrystalHeart(EntityData data, Vector2 offset) : base(data.Position + offset) {

            HeartSummon = new HeartGem(data, offset);
            flag = data.Attr("flag");

        }

        public override void Added(Scene scene) {
            base.Added(scene);
            UpdateSummon();
        }

        public override void Update() {
            base.Update();
            UpdateSummon();
        }

        private void UpdateSummon() {
            if (!ThisSession.HeartGem && ThisSession.GetFlag(flag) != active) {
                if (!(summoned || active)) {
                    Scene.Add(HeartSummon);
                    summoned = true;
                }
                active = HeartSummon.Collidable = HeartSummon.Visible = !active;
            }
        }
    }
}