namespace Celeste.Mod.MemorialHelper
{
    public class MemorialModule : EverestModule
    {

        // Only one alive module instance can exist at any given time.
        public static MemorialModule Instance;

        public MemorialModule()
        {
            Instance = this;
        }
        public override void Load()
        {
        }
        public override void Unload()
        {
        }
    }
}
