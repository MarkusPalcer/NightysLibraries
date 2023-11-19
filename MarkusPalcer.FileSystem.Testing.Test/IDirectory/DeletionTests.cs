using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.IDirectory;

[TestClass]
public class DeletionTests
{

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void DeleteFileSystemRoot(bool recursive)
    {
        var fileSystem = Common.CreateSampleData();
        var sut = (MarkusPalcer.FileSystem.IDirectory)fileSystem.GetDirectory("C:\\");

        sut.Invoking(x => x.Delete(recursive)).Should().Throw<IOException>();

        sut.Exists.Should().BeTrue();
        sut.Directories.Should().NotBeEmpty();
        sut.Directories.First().Files.Should().NotBeEmpty();
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void DeleteNonExistingDirectory(bool recursive)
    {
        var fileSystem = Common.CreateSampleData();
        var sut = (MarkusPalcer.FileSystem.IDirectory)fileSystem.GetDirectory(Common.NonExistingDirectory);
        sut.Invoking(x => x.Delete(recursive)).Should().Throw<DirectoryNotFoundException>();
    }

    [TestMethod]
    public void DeleteNonEmptyDirectory()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = (MarkusPalcer.FileSystem.IDirectory)fileSystem.GetDirectory(Common.ExistingDirectory);
        sut.Invoking(x => x.Delete()).Should().Throw<IOException>();

        sut.Files.Should().NotBeEmpty();
        sut.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void RecursiveDeletion()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = (MarkusPalcer.FileSystem.IDirectory)fileSystem.GetDirectory(Common.ExistingDirectory);
        sut.Invoking(x => x.Delete(true)).Should().NotThrow();

        sut.Exists.Should().BeFalse();

        sut.Directories.Should().BeEmpty();
        sut.Files.Should().BeEmpty();
    }

    [TestMethod]
    public void GetFileInSubdirectory()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = (MarkusPalcer.FileSystem.IDirectory)fileSystem.GetDirectory(Common.ExistingDirectory);

        var file = sut.GetFile("SubDir\\File.txt");

        file.Name.Should().Be("File.txt");
        file.FullPath.Should().Be(Path.Combine(Common.ExistingDirectory, "SubDir\\File.txt"));
        file.Parent.Parent.Should().BeSameAs(sut);
    }
}