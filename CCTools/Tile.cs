using System;
using System.ComponentModel;

namespace CCTools
{
	[Serializable, TypeConverter(typeof(TileConverter))]
	public enum Tile : byte
	{
		Floor, Wall, Chip, Water, Fire, InvisibleWall, PanelN, PanelW, PanelS, PanelE, Block, Dirt, Ice, ForceFloorS, BlockN, BlockW,
		BlockS, BlockE, ForceFloorN, ForceFloorE, ForceFloorW, Exit, BlueLock, RedLock, GreenLock, YellowLock, IceNW, IceNE, IceSE, IceSW, BlueWallFake, BlueWallReal,
		Unused20, Thief, Socket, GreenButton, RedButton, ToggleDoorClosed, ToggleDoorOpen, BrownButton, BlueButton, Teleport, Bomb, Trap, HiddenWall, Gravel, RecessedWall, Hint,
		PanelSE, CloningMachine, ForceFloorRandom, Splash, BurningChip, BurntChip, Unused36, Unused37, Unused38, ChipInExit, FakeExit1, FakeExit2, ChipSwimN, ChipSwimW, ChipSwimS, ChipSwimE,
		BugN, BugW, BugS, BugE, FireballN, FireballW, FireballS, FireballE, PinkBallN, PinkBallW, PinkBallS, PinkBallE, TankN, TankW, TankS, TankE,
		GliderN, GliderW, GliderS, GliderE, TeethN, TeethW, TeethS, TeethE, WalkerN, WalkerW, WalkerS, WalkerE, BlobN, BlobW, BlobS, BlobE,
		ParameciumN, ParameciumW, ParameciumS, ParameciumE, BlueKey, RedKey, GreenKey, YellowKey, Flippers, FireBoots, IceSkates, SuctionBoots, ChipN, ChipW, ChipS, ChipE,
		OrBugN, OrBugW, OrBugS, OrBugE, OrFireballN, OrFireballW, OrFireballS, OrFireballE, OrPinkBallN, OrPinkBallW, OrPinkBallS, OrPinkBallE, OrTankN, OrTankW, OrTankS, OrTankE,
		OrGliderN, OrGliderW, OrGliderS, OrGliderE, OrTeethN, OrTeethW, OrTeethS, OrTeethE, OrWalkerN, OrWalkerW, OrWalkerS, OrWalkerE, OrBlobN, OrBlobW, OrBlobS, OrBlobE,
		OrParameciumN, OrParameciumW, OrParameciumS, OrParameciumE, OrBlueKey, OrRedKey, OrGreenKey, OrYellowKey, OrFlippers, OrFireBoots, OrIceSkates, OrSuctionShoes, OrChipN, OrChipW, OrChipS, OrChipE,
		AndBugN, AndBugW, AndBugS, AndBugE, AndFireballN, AndFireballW, AndFireballS, AndFireballE, AndPinkBallN, AndPinkBallW, AndPinkBallS, AndPinkBallE, AndTankN, AndTankW, AndTankS, AndTankE,
		AndGliderN, AndGliderW, AndGliderS, AndGliderE, AndTeethN, AndTeethW, AndTeethS, AndTeethE, AndWalkerN, AndWalkerW, AndWalkerS, AndWalkerE, AndBlobN, AndBlobW, AndBlobS, AndBlobE,
		AndParameciumN, AndParameciumW, AndParameciumS, AndParameciumE, AndBlueKey, AndRedKey, AndGreenKey, AndYellowKey, AndFlippers, AndFireBoots, AndIceSkates, AndSuctionShoes, AndChipN, AndChipW, AndChipS, AndChipE
	}
}
