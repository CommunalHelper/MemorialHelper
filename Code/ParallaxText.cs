using Celeste.Mod.Entities;
using Microsoft.Xna.Framework;
using Monocle;
using System;

namespace Celeste.Mod.MemorialHelper {
    [Tracked(false)]
    [CustomEntity("MemorialHelper/ParallaxText")]
    public class ParallaxText : Entity {
        //I spent hours looking for non-existent mistakes in my parallax equation
        //		only to find out the position system for text has an arbitrary scalar of 6.
        private static readonly Vector2 camCenterOffset = new Vector2(160f, 90f);
        private static readonly float camScalar = 6f;

        private readonly Vector2 centerPos;

        private readonly string[] text;
        private readonly Vector2 textCenterPos;
        private readonly Vector2 textCenterOffset;
        private readonly bool outline;

        private readonly bool noFlag;
        private readonly string flag;

        private readonly Vector2 halfSize;
        private readonly Vector2 zeroAlphaDistance;

        private readonly Vector2 parallaxScalar;

        private float alpha;

        private readonly Vector2 textScalar;
        private readonly Color textColor;
        private readonly Color borderColor;


        public ParallaxText(EntityData data, Vector2 offset) : base(data.Position + offset) {
            Tag = Tags.HUD;

            Vector2 textSize = Vector2.Zero;
            textScalar = new Vector2(data.Float("textScalarX", 1.25f), data.Float("textScalarY", 1.25f));
            textColor = Calc.HexToColor(data.Attr("color", "ffffff"));
            borderColor = Calc.HexToColor(data.Attr("borderColor", "000000"));

            //The 'error text' is part of the SC2020 hunt. If you get it unexpectedly, it means
            //		that the text you placed has no 'dialog' attribute, likely an ahorn/lonn integration error
            text = data.Has("dialog") ? Dialog.Clean(data.Attr("dialog", "app_ending")).Split(new char[] { '\n', '\r' }, StringSplitOptions.None) : new string[] { "Extract the zip", "Find 'gymBG'", "Then open it as", "a .txt" };
            float textWidth = 0f;
            foreach (string line in text) {
                Vector2 lineSize = ActiveFont.Measure(line);
                float width = Math.Max(lineSize.X, textWidth);
                textSize.Y += lineSize.Y * textScalar.Y;
            }

            textSize.X = 0;//textWidth * textScalar.X
            textCenterOffset = textSize / 2 - new Vector2(data.Float("offsetX"), data.Float("offsetY")) * camScalar;

            outline = data.Bool("border");

            halfSize = new Vector2(data.Width / 2, data.Height / 2);

            centerPos = halfSize + data.Position + offset;
            textCenterPos = data.Nodes.Length == 0 ? centerPos : data.Nodes[0] + offset;
            parallaxScalar = new Vector2(data.Float("parallaxX"), data.Float("parallaxY"));

            zeroAlphaDistance = new Vector2(Math.Max(0f, data.Float("visibleDistanceX")), Math.Max(0f, data.Float("visibleDistanceY", 1f))) + halfSize;

            flag = data.Attr("flag");
            noFlag = flag.Length == 0;
        }

        public override void Awake(Scene scene) {
            base.Awake(scene);
            Update();
        }

        public override void Update() {
            Player entity = Scene.Tracker.GetEntity<Player>();
            if (entity != null) {
                alpha = Ease.CubeInOut(Calc.ClampedMap(Math.Abs(entity.Center.X - centerPos.X), halfSize.X, zeroAlphaDistance.X, 1f, 0f) * Calc.ClampedMap(Math.Abs(entity.Center.Y - centerPos.Y), halfSize.Y, zeroAlphaDistance.Y, 1f, 0f));
            }
            base.Update();
        }

        public override void Render() {
            if (noFlag || SceneAs<Level>().Session.GetFlag(flag)) {
                Camera cam = SceneAs<Level>().Camera;

                //parallax equation
                Vector2 pos = camScalar * (Vector2.Multiply(parallaxScalar, textCenterPos - cam.Position - camCenterOffset) + camCenterOffset) - textCenterOffset;

                if (SaveData.Instance != null && SaveData.Instance.Assists.MirrorMode)
                    pos.X = 1920f - pos.X;


                foreach (string line in text) {
                    if (outline)
                        ActiveFont.DrawOutline(line, pos, new Vector2(0.5f, 0f), textScalar, textColor * alpha, 2f, borderColor * (alpha * alpha));
                    else
                        ActiveFont.Draw(line, pos, new Vector2(0.5f, 0f), textScalar, textColor * alpha);
                    pos += ActiveFont.Measure(line).Y * textScalar.YComp();
                }
            }
        }

    }
}