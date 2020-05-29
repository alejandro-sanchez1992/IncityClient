using System;
using System.IO;

namespace IncityClient.Common.interfaces
{
    public interface IFilesHelper
    {
        byte[] ReadFully(Stream input);
    }
}
