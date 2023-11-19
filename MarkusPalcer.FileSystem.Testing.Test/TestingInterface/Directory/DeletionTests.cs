using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.TestingInterface.Directory;

[TestClass]
public class DeletionTests
{
    [TestMethod]
    public void DeleteFileSystemRootRecursively()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory("C:\\");

        sut.Invoking(x => x.Delete()).Should().NotThrow();

        fileSystem.EnumerateAllDirectories().Should().AllSatisfy(x => x.Exists.Should().BeFalse());
        fileSystem.EnumerateAllFiles().Should().AllSatisfy(x => x.Exists.Should().BeFalse());
    }

    [TestMethod]
    public void DeleteEmptyFileSystemRoot()
    {
        var fileSystem = new FileSystem();

        var sut = fileSystem.GetDirectory("C:\\");

        fileSystem.EnumerateAllDirectories().Should().NotBeEmpty();


        sut.Invoking(x => x.Delete(false)).Should().NotThrow();

        fileSystem.EnumerateAllDirectories().Should().AllSatisfy(x => x.Exists.Should().BeFalse());
        fileSystem.EnumerateAllFiles().Should().BeEmpty();
    }

    [TestMethod]
    [DataRow(true)]
    [DataRow(false)]
    public void DeleteNonExistingDirectory(bool recursive)
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.NonExistingDirectory);
        sut.Invoking(x => x.Delete(recursive)).Should().NotThrow();
    }

    [TestMethod]
    public void DeleteNonEmptyDirectory()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.ExistingDirectory);
        sut.Invoking(x => x.Delete(false)).Should().Throw<IOException>();

        sut.Files.Should().NotBeEmpty();
        sut.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void RecursiveDeletion()
    {
        var fileSystem = Common.CreateSampleData();
        var sut = fileSystem.GetDirectory(Common.ExistingDirectory);
        sut.Invoking(x => x.Delete()).Should().NotThrow();

        sut.Exists.Should().BeFalse();

        sut.Directories.Should().AllSatisfy(x => x.Exists.Should().BeFalse());
        sut.Files.Should().AllSatisfy(x => x.Exists.Should().BeFalse());

        sut.GetDirectory("C:\\").Exists.Should().BeTrue();
    }
}