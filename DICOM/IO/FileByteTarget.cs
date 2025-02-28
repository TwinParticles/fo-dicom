﻿// Copyright (c) 2012-2021 fo-dicom contributors.
// Licensed under the Microsoft Public License (MS-PL).

namespace Dicom.IO
{
    using System;
    using System.IO;

#if !NET35
    using System.Threading.Tasks;
#endif

    /// <summary>
    /// Representation of a file byte target.
    /// </summary>
    public sealed class FileByteTarget : IDisposable, IByteTarget
    {
        private IFileReference _file;

        private Stream _stream;

        private Endian _endian;

        private BinaryWriter _writer;

        private readonly object _lock;

        /// <summary>
        /// Initializes an instance of <see cref="FileByteTarget"/>.
        /// </summary>
        /// <param name="file"></param>
        public FileByteTarget(IFileReference file)
        {
            _file = file;
            _stream = _file.OpenWrite();
            _endian = Endian.LocalMachine;
            _writer = EndianBinaryWriter.Create(_stream, _endian);
            _lock = new object();
        }

        /// <summary>
        /// Gets or sets the endianness of the byte target.
        /// </summary>
        public Endian Endian
        {
            get
            {
                return _endian;
            }
            set
            {
                if (_endian != value)
                {
                    lock (_lock)
                    {
                        _endian = value;
                        _writer = EndianBinaryWriter.Create(_stream, _endian);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the current write position.
        /// </summary>
        public long Position
        {
            get
            {
                return _stream.Position;
            }
        }

        /// <summary>
        /// Write one <see cref="byte"/> to target.
        /// </summary>
        /// <param name="v"><see cref="byte"/> to write.</param>
        public void Write(byte v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="short"/> to target.
        /// </summary>
        /// <param name="v"><see cref="short"/> to write.</param>
        public void Write(short v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="ushort"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ushort"/> to write.</param>
        public void Write(ushort v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="int"/> to target.
        /// </summary>
        /// <param name="v"><see cref="int"/> to write.</param>
        public void Write(int v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="uint"/> to target.
        /// </summary>
        /// <param name="v"><see cref="uint"/> to write.</param>
        public void Write(uint v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="long"/> to target.
        /// </summary>
        /// <param name="v"><see cref="long"/> to write.</param>
        public void Write(long v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="ulong"/> to target.
        /// </summary>
        /// <param name="v"><see cref="ulong"/> to write.</param>
        public void Write(ulong v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="float"/> to target.
        /// </summary>
        /// <param name="v"><see cref="float"/> to write.</param>
        public void Write(float v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write one <see cref="double"/> to target.
        /// </summary>
        /// <param name="v"><see cref="double"/> to write.</param>
        public void Write(double v)
        {
            _writer.Write(v);
        }

        /// <summary>
        /// Write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        public void Write(byte[] buffer, uint offset, uint count)
        {
            _stream.Write(buffer, (int)offset, (int)count);
        }

#if !NET35
        /// <summary>
        /// Asynchronously write array of <see cref="byte"/>s to target.
        /// </summary>
        /// <param name="buffer">Array of <see cref="byte"/>s to write.</param>
        /// <param name="offset">Index of first position in <paramref name="buffer"/> to write to byte target.</param>
        /// <param name="count">Number of bytes to write to byte target.</param>
        /// <returns>Avaitable <see cref="Task"/>.</returns>
        public Task WriteAsync(byte[] buffer, uint offset, uint count)
        {
            return _stream.WriteAsync(buffer, (int)offset, (int)count);
        }
#endif

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            try
            {
                _stream.Dispose();
                _stream = null;
            }
            catch
            {
            }
        }
    }
}
