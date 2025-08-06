# walkamileinmyshoes

A RimWorld 1.6 mod that adds a damage-over-time mechanic for pawns who walk barefoot.
Pawns lacking footwear accumulate the **Barefoot Damage** hediff which slows them down and hurts.

### Features
- Adds several foot-related diseases: gout, athlete's foot, and trench foot.
- Barefoot damage applies hediffs to each foot when pawns have no shoes.
- Includes a Biotech gene **Tough Feet** that grants immunity to barefoot damage.

Place this folder in your RimWorld `Mods` directory to play.

### Development
Game libraries such as `Assembly-CSharp.dll` are only required for building the mod
and should not be included in the published `Mods` folder. Keeping them out of the
mod directory prevents type loading conflicts like missing `GameComponentDef` errors.
