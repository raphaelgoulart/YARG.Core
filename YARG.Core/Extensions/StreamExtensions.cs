﻿using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace YARG.Core.Extensions
{
    public enum Endianness
    {
        Little = 0,
        Big = 1,
    };

    public static class StreamExtensions
    {
        public static TType Read<TType>(this Stream stream, Endianness endianness)
            where TType : unmanaged, IComparable, IComparable<TType>, IConvertible, IEquatable<TType>, IFormattable
        {
            TType value = default;
            unsafe
            {
                byte* buffer = (byte*)&value;
                if (stream.Read(new Span<byte>(buffer, sizeof(TType))) != sizeof(TType))
                {
                    throw new EndOfStreamException($"Not enough data in the stream to read {typeof(TType)} ({sizeof(TType)} bytes)!");
                }
                CorrectByteOrder<TType>(buffer, endianness);
            }
            return value;
        }

        public static bool ReadBoolean(this Stream stream)
        {
            byte b = (byte)stream.ReadByte();
            return Unsafe.As<byte, bool>(ref b);
        }

        public static string ReadString(this Stream stream)
        {
            int length = Read7BitEncodedInt(stream);
            if (length == 0)
            {
                return string.Empty;
            }

            if (stream is UnmanagedMemoryStream unmanaged) unsafe
            {
                string str = Encoding.UTF8.GetString(unmanaged.PositionPointer, length);
                stream.Position += length;
                return str;
            }
            else if (stream is MemoryStream managed)
            {
                string str = Encoding.UTF8.GetString(managed.GetBuffer(), (int) managed.Position, length);
                stream.Position += length;
                return str;
            }

            var bytes = stream.ReadBytes(length);
            return Encoding.UTF8.GetString(bytes);
        }

        public static byte[] ReadBytes(this Stream stream, int length)
        {
            byte[] buffer = new byte[length];
            if (stream.Read(buffer, 0, length) != length)
            {
                throw new EndOfStreamException($"Not enough data in the stream to read {length} bytes!");
            }
            return buffer;
        }

        public static void Write<TType>(this Stream stream, TType value, Endianness endianness)
            where TType : unmanaged, IComparable, IComparable<TType>, IConvertible, IEquatable<TType>, IFormattable
        {
            unsafe
            {
                byte* buffer = (byte*) &value;
                CorrectByteOrder<TType>(buffer, endianness);
                stream.Write(new Span<byte>(buffer, sizeof(TType)));
            }
        }

        private static unsafe void CorrectByteOrder<TType>(byte* bytes, Endianness endianness)
            where TType : unmanaged, IComparable, IComparable<TType>, IConvertible, IEquatable<TType>, IFormattable
        {
            // Have to flip bits if the OS uses the opposite Endian
            if ((endianness == Endianness.Little) != BitConverter.IsLittleEndian)
            {
                int half = sizeof(TType) >> 1;
                for (int i = 0, j = sizeof(TType) - 1; i < half; ++i, --j)
                    (bytes[j], bytes[i]) = (bytes[i], bytes[j]);
            }
        }

        private static int Read7BitEncodedInt(this Stream stream)
        {
            uint result = 0;
            byte byteReadJustNow;

            const int MaxBytesWithoutOverflow = 4;
            for (int shift = 0; shift < MaxBytesWithoutOverflow * 7; shift += 7)
            {
                byteReadJustNow = (byte) stream.ReadByte();
                result |= (byteReadJustNow & 0x7Fu) << shift;
                if (byteReadJustNow <= 0x7Fu)
                {
                    return (int) result;
                }
            }

            byteReadJustNow = (byte) stream.ReadByte();
            if (byteReadJustNow > 0b_1111u)
            {
                throw new Exception("LEB value exceeds max allowed");
            }

            result |= (uint) byteReadJustNow << MaxBytesWithoutOverflow * 7;
            return (int) result;
        }
    }
}
