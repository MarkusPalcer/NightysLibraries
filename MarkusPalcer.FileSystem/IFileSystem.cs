namespace MarkusPalcer.FileSystem;

/// <summary>
/// The root object for the file system abstraction
/// </summary>
public interface IFileSystem
{
    IFile GetFile(string path);
    IDirectory GetDirectory(string path);
}