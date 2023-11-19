namespace MarkusPalcer.FileSystem;

/// <summary>
/// Replacement for <see cref="FileInfo"/> which can be replaced in tests
/// </summary>
public interface IFile
{
    /// <summary>
    /// <inheritdoc cref="FileInfo.Name"/>
    /// </summary>
    string Name { get; }

    /// <summary>
    /// <inheritdoc cref="FileInfo.FullName"/>
    /// </summary>
    string FullPath { get; }

    /// <summary>
    /// <inheritdoc cref="FileInfo.Directory"/>
    /// </summary>
    IDirectory Parent { get; }

    /// <summary>
    /// <inheritdoc cref="FileInfo.Exists"/>
    /// </summary>
    bool Exists { get; }

    /// <summary>
    /// <inheritdoc cref="System.IO.File.WriteAllText(string, string?)"/>
    /// </summary>
    /// <param name="content">The string to write to the file</param>
    void WriteAllText(string? content);

    /// <summary>
    /// <inheritdoc cref="System.IO.File.ReadAllText(string)"/>
    /// </summary>
    string ReadAllText();

    /// <summary>
    /// <inheritdoc cref="System.IO.FileInfo.Delete()"/>
    /// </summary>
    void Delete();
}