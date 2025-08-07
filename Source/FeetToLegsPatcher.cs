using System.Linq;
using RimWorld;
using Verse;

namespace WalkAMileInMyShoes
{
    [StaticConstructorOnStartup]
    public static class FeetToLegsPatcher
    {
        static FeetToLegsPatcher()
        {
            foreach (ThingDef def in DefDatabase<ThingDef>.AllDefs)
            {
                var groups = def.apparel?.bodyPartGroups;
                if (groups == null) continue;
                for (int i = 0; i < groups.Count; i++)
                {
                    BodyPartGroupDef group = groups[i];
                    string name = group?.defName ?? string.Empty;
                    if (name.IndexOf("foot", System.StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        groups[i] = BodyPartGroupDefOf.Legs;
                    }
                }
            }
        }
    }
}
