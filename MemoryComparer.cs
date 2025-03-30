using System;
using System.Linq;
using System.Runtime.InteropServices;


public static class MemoryComparer
{
    [DllImport("msvcrt.dll", CallingConvention = CallingConvention.Cdecl)]
    static extern int memcmp(byte[] b1, byte[] b2, long count);

    public static bool EqualsMemCmp(byte[] b1, byte[] b2)
    {
        return b1.Length == b2.Length && memcmp(b1, b2, b1.Length) == 0;
    }

    public static bool EqualsLoop(byte[] b1, byte[] b2)
    {

        if (b1.Length != b2.Length) return false;
        for (int i = 0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i]) return false;
        }

        return true;
    }

    public static bool EqualsSequenceEqual(byte[] b1, byte[] b2)
    {
        return b1.SequenceEqual(b2);
    }

    public static bool EqualsSpan(ReadOnlySpan<byte> b1, ReadOnlySpan<byte> b2)
    {
        return b1.SequenceEqual(b2);
    }
}