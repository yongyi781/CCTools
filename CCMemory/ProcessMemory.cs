using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using static CCMemory.NativeMethods;

namespace CCMemory
{
    public class ProcessMemory
    {
        public static bool ReadChipsState(Process process, IntPtr address, ChipsState state)
        {
            return ReadProcessMemory(process.Handle, address, state, Marshal.SizeOf<ChipsState>(), out _);
        }

        public static bool WriteChipsState(Process process, IntPtr address, ChipsState state)
        {
            return WriteProcessMemory(process.Handle, address, state, Marshal.SizeOf<ChipsState>(), out _);
        }

        public static bool ReadMonsterList(Process process, IntPtr address, Monster[] monsterList, int count)
        {
            var asBytes = MemoryMarshal.AsBytes<Monster>(monsterList);
            return ReadProcessMemory(process.Handle, address, out asBytes[0], count * Marshal.SizeOf<Monster>(), out _);
        }

        public static bool WriteMonsterList(Process process, IntPtr address, Monster[] monsterList, int count)
        {
            var asBytes = MemoryMarshal.AsBytes<Monster>(monsterList);
            return WriteProcessMemory(process.Handle, address, in asBytes[0], count * Marshal.SizeOf<Monster>(), out _);
        }

        public static IntPtr Find(Process process, byte[] sequence)
        {
            var address = IntPtr.Zero;
            while (VirtualQueryEx(process.Handle, address, out var mbi, Marshal.SizeOf<MEMORY_BASIC_INFORMATION>()) != 0)
            {
                var regionSize = (int)mbi.RegionSize;
                if (regionSize < 0)
                    break;  // Would wrap back to 0 otherwise.

                if (mbi.Protect == PageProtect.PAGE_EXECUTE_READWRITE)
                {
                    var span = new Span<byte>(new byte[regionSize]);
                    ReadProcessMemory(process.Handle, mbi.BaseAddress, out span[0], span.Length, out _);
                    var index = span.IndexOf(sequence);
                    if (index != -1)
                        return mbi.BaseAddress + index;
                }
                address += regionSize;
            }
            return new IntPtr(-1);
        }
    }
}
