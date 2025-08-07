using HarmonyLib;
using Verse;

namespace WalkAMileInMyShoes
{
    [StaticConstructorOnStartup]
    public static class Bootstrap
    {
        static Bootstrap()
        {
            var harmony = new Harmony("openai.walkamileinmyshoes");
            harmony.Patch(
                original: AccessTools.Method(typeof(Game), nameof(Game.FinalizeInit)),
                postfix: new HarmonyMethod(typeof(Bootstrap), nameof(AddComponent))
            );
        }

        private static void AddComponent(Game __instance)
        {
            if (__instance.GetComponent<GameComponent_BarefootDamage>() == null)
            {
                __instance.components.Add(new GameComponent_BarefootDamage(__instance));
            }
        }
    }
}
