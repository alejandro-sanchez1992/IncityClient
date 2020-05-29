using System;
using System.IO;
using IncityClient.Common.interfaces;

namespace IncityClient.Common.Helpers
{
    public class FilesHelper : IFilesHelper
    {
        public byte[] ReadFully(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
