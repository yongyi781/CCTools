using CCTools;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace CCMemory
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
    public class ChipsState
    {
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public Tile[] Upper { get; set; }           // (0,0)
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 1024)]
        public Tile[] Lower { get; set; }           // (0,32)
        public short LevelNumber { get; set; }      // (0,64)
        public short TotalLevelCount { get; set; }  // (2,64)
        public short InitialTimeLimit { get; set; } // (4,64)
        public short InitialChipsLeft { get; set; } // (6,64)
        public Point ChipLocation { get; set; }     // (8,64)
        public short IsSliding { get; set; }        // (12,64)
        public short IsBuffered { get; set; }       // (14,64)
        public short ShowStartScreen { get; set; }  // (16,64)
        public Point BufferDirection { get; set; }   // (18,64)
        public short Autopsy { get; set; }          // (22,64)
        public Point SlidingDirection { get; set; } // (24,64)
        public short InitialMonsterListSize { get; set; }   // (28,64)
        [field: MarshalAs(UnmanagedType.ByValArray, SizeConst = 128)]
        public BytePoint[] InitialMonsterList { get; set; } // (30,64)
        public ListEntry SlipList { get; set; }         // (30,72)
        public ListEntry MonsterList { get; set; }      // (8,73)
        public ListEntry ToggleList { get; set; }       // (18,73)
        public ListEntry TrapList { get; set; }         // (28,73)
        public ListEntry CloneList { get; set; }        // (6,74)
        public ListEntry TeleportList { get; set; }     // (16,74)
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
        public string Title { get; set; }               // (26,74)
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
        public string Hint { get; set; }                // (26,76)
        [field: MarshalAs(UnmanagedType.ByValTStr, SizeConst = 10)]
        public string Password { get; set; }            // (26,80)
        public Point ViewportLocation { get; set; }     // (4,81)
        public Point ViewportSize { get; set; }         // (8,81)
        public Point UnknownPoint { get; set; }         // (12,81)
        public short RestartCount { get; set; }         // (16,81)
        public short HavingTroubleCounter { get; set; } // (18,81)
        public short MoveCount { get; set; }            // (20,81)
        public short EndingTick { get; set; }           // (22,81)
        public short HasMouseTarget { get; set; }       // (24,81)
        public Point MouseTarget { get; set; }          // (26,81)
        public short IdleTickCount { get; set; }        // (30,81)
        public short ChipHasMoved { get; set; }         // (0,82)
        public short GameStateSize { get; set; }        // (2,82)
    }

    [TypeConverter(typeof(BytePointConverter))]
    public struct BytePoint
    {
        public BytePoint(byte x, byte y)
        {
            X = x;
            Y = y;
        }

        public byte X { get; set; }
        public byte Y { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    [TypeConverter(typeof(PointConverter))]
    public struct Point
    {
        public Point(short x, short y)
        {
            X = x;
            Y = y;
        }

        public short X { get; set; }
        public short Y { get; set; }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    [TypeConverter(typeof(ListEntryConverter))]
    public struct ListEntry
    {
        public short Length { get; set; }
        public short Cap { get; set; }
        public short Handle { get; set; }
        public short Pointer { get; set; }
        public short Segment { get; set; }

        public override string ToString()
        {
            return $"{Length}, {Cap}, {Handle}, {Pointer}, {Segment}";
        }
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1), TypeConverter(typeof(MonsterConverter))]
    public struct Monster
    {
        public Tile Tile { get; set; }
        public Point Location { get; set; }
        public Point Direction { get; set; }
        public short IsSlipping { get; set; }

        public override string ToString()
        {
            return $"{Tile}, ({Location}), ({Direction}), {IsSlipping}";
        }
    }
}
