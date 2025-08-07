using System.Linq;
using RimWorld;
using Verse;

namespace WalkAMileInMyShoes
{
    public class GameComponent_BarefootDamage : GameComponent
    {
        public GameComponent_BarefootDamage(Game game) : base(game) { }

        public override void GameComponentTick()
        {
            if (Find.TickManager.TicksGame % 250 != 0) return;

            HediffDef barefootDef = DefDatabase<HediffDef>.GetNamedSilentFail("BarefootDamage");
            if (barefootDef == null) return;

            GeneDef toughFeet = DefDatabase<GeneDef>.GetNamedSilentFail("ToughFeet");

            foreach (Map map in Find.Maps)
            {
                foreach (Pawn pawn in map.mapPawns.FreeColonistsAndPrisonersSpawned)
                {
                    if (!pawn.RaceProps.Humanlike || pawn.Dead) continue;

                    bool hasFootwear = pawn.apparel?.WornApparel?.Any(a => a.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.Legs)) ?? false;
                    bool immuneByGene = pawn.genes?.HasGene(toughFeet) ?? false;

                    if (!hasFootwear && !immuneByGene)
                    {
                        ApplyBarefootDamage(pawn, barefootDef);
                    }
                    else
                    {
                        RemoveBarefootDamage(pawn, barefootDef);
                    }
                }
            }
        }

        private void ApplyBarefootDamage(Pawn pawn, HediffDef def)
        {
            float multiplier = TerrainDamageMultiplier(pawn);
            foreach (BodyPartRecord part in pawn.RaceProps.body.AllParts.Where(p => p.def.defName.Contains("Leg")))
            {
                Hediff hediff = pawn.health.hediffSet.GetFirstHediffOfDef(def, part);
                if (hediff == null)
                {
                    hediff = pawn.health.AddHediff(def, part);
                }

                var comp = def.CompProps<HediffCompProperties_SeverityPerDay>();
                float perDay = comp?.severityPerDay ?? 0f;
                float perInterval = perDay / 240f; // 60k ticks per day / 250 interval
                hediff.Severity += perInterval * (multiplier - 1f);
            }
        }

        private float TerrainDamageMultiplier(Pawn pawn)
        {
            TerrainDef terrain = pawn.Position.GetTerrain(pawn.Map);
            if (terrain == null)
            {
                return 1f;
            }

            string name = terrain.defName.ToLowerInvariant();
            if (name.Contains("carpet") || name.Contains("mat") || name.Contains("moss"))
            {
                return 0.5f;
            }
            if (name.Contains("soil") || name.Contains("sand") || name.Contains("dirt") || name.Contains("ice"))
            {
                return 0.75f;
            }
            if (name.Contains("metal") || name.Contains("concrete") || name.Contains("tile") || name.Contains("stone"))
            {
                return 1.5f;
            }
            return 1f;
        }

        private void RemoveBarefootDamage(Pawn pawn, HediffDef def)
        {
            foreach (Hediff hediff in pawn.health.hediffSet.hediffs.Where(h => h.def == def).ToList())
            {
                pawn.health.RemoveHediff(hediff);
            }
        }
    }
}
