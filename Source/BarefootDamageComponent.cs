using System.Linq;
using RimWorld;
using Verse;

namespace WalkAMileInMyShoes
{
    public class GameComponent_BarefootDamage : GameComponent
    {
        public GameComponent_BarefootDamage(Game game) { }

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

                    bool hasFootwear = pawn.apparel?.WornApparel?.Any(a => a.def.apparel.bodyPartGroups.Contains(BodyPartGroupDefOf.Feet)) ?? false;
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
            foreach (BodyPartRecord part in pawn.RaceProps.body.AllParts.Where(p => p.def.defName.Contains("Foot")))
            {
                if (!pawn.health.hediffSet.HasHediff(def, part))
                {
                    pawn.health.AddHediff(def, part);
                }
            }
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
