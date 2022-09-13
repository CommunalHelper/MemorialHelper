namespace Celeste.Mod.MemorialHelper {
    public class MemorialModule : EverestModule {
        public static MemorialModule Instance;

        public MemorialModule() {
            Instance = this;
        }
        public override void Load() { }
        public override void Unload() { }
    }
}
