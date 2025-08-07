# walkamileinmyshoes

A RimWorld 1.6 mod that adds a damage-over-time mechanic for pawns who walk barefoot.
Pawns lacking footwear accumulate the **Barefoot Damage** hediff which slows them down and hurts.

### Features
- Adds several foot-related diseases: gout, athlete's foot, and trench foot.
- Barefoot damage applies hediffs to each leg when pawns have no shoes.
- Automatically converts apparel that targets feet to use legs for broader mod compatibility.
- Damage severity scales with the terrain – harder floors hurt bare feet more.
- Includes a Biotech gene **Tough Feet** that grants immunity to barefoot damage.

Place this folder in your RimWorld `Mods` directory to play.

### Development
Game libraries such as `Assembly-CSharp.dll` are only required for building the mod
and should not be included in the published `Mods` folder. Keeping them out of the
mod directory prevents type loading conflicts when the game starts.
