namespace MarkusPalcer.FileSystem;

/// <summary>
/// Replacement <see cref="DirectoryInfo"/> which can be virtualized for tests
/// </summary>
public interface IDirectory
{
    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.FullName"/>
    /// </summary>
    string FullPath { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.Name"/>
    /// </summary>
    string Name { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.Exists"/>
    /// </summary>
    bool Exists { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.Parent"/>
    /// </summary>
    IDirectory? Parent { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.EnumerateDirectories()"/>
    /// </summary>
    IEnumerable<IDirectory> Directories { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.EnumerateFiles()"/>
    /// </summary>
    IEnumerable<IFile> Files { get; }

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.Create"/>
    /// </summary>
    void Create();

    /// <summary>
    /// Returns a file abstraction for the given file relative to this directory
    /// </summary>
    IFile GetFile(string path);

    /// <summary>
    /// Returns a directory abstraction for the given directory relative to this directory
    /// </summary>
    IDirectory GetDirectory(string path);

    /// <summary>
    /// <inheritdoc cref="DirectoryInfo.Delete(bool)"/>
    /// </summary>
    void Delete(bool recursive = false);
}