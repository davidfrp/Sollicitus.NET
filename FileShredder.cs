using System;
using System.IO;

namespace Sollicitus
{
    public static class FileShredder
    {
        /// <summary>
        /// Gets or sets a value indicating whether the file should be shredded using zeroization.
        /// </summary>
        public static bool IsUsingZeroization { get; set; }

        private static int _overwrites;
        private static double _totalSectors;

        /// <summary>
        /// Gets or sets the amount of overwrites for the file.
        /// </summary>
        public static void SetOverwrites(int overwrites)
        {
            _overwrites = overwrites;
        }

        /// <summary>
        /// Shreds a directory with all of its subdirectories and files.
        /// </summary>
        public static void Shred(this DirectoryInfo directoryInfo)
        {
            foreach (var subDirectoryInfo in directoryInfo.GetDirectories())
            {
                subDirectoryInfo.Shred();
            }

            foreach (var fileInfo in directoryInfo.GetFiles())
            {
                fileInfo.Shred();
            }
        }

        /// <summary>
        /// Shreds a file and then deletes it permanently.
        /// </summary>
        public static void Shred(this FileInfo fileInfo)
        {
            fileInfo.Attributes = FileAttributes.Normal;

            _totalSectors = Math.Ceiling(fileInfo.Length / 512.0);

            var dummyBuffer = new byte[512];
            var fileStream = fileInfo.Open(FileMode.Open);
            for (int currentOverwrite = 1; currentOverwrite <= _overwrites; currentOverwrite++)
            {
                fileStream.Position = 0;

                for (int currentSector = 1; currentSector <= _totalSectors; currentSector++)
                {
                    if (!IsUsingZeroization)
                        new Random().NextBytes(dummyBuffer);

                    fileStream.Write(dummyBuffer, 0, dummyBuffer.Length);
                }
            }

            fileStream.SetLength(0);
            fileStream.Close();
            fileInfo.Delete();
        }
    }
}
