using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.IDirectory;

[TestClass]
public class CreationTests
{
    [TestMethod]
    public void CreatingDirectoryInExistingParent()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingDirectory)
                                                            .GetDirectory("NewDirectory");
        sut.Exists.Should().BeFalse();

        sut.Create();
        sut.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void CreatingDirectoryInNonExistingParent()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.NonExistingDirectory)
                                                            .GetDirectory("NewDirectory");
        sut.Exists.Should().BeFalse();

        sut.Invoking(x => x.Create())
           .Should().Throw<DirectoryNotFoundException>()
           .Where(x => x.Message.Contains($" {Common.NonExistingDirectory} "), "the exception message should contain the lowest non-existing directory");
        sut.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void RootsCannotBeCreated()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory("Z:\\");
        sut.Exists.Should().BeFalse();

        sut.Invoking(x => x.Create())
           .Should().Throw<DirectoryNotFoundException>()
           .Where(x => x.Message.Contains("this is not allowed for a file system root"), "the exception message should say that a root was trying to be created");
        sut.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void CreateExistingDirectory()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingDirectory);
        sut.Exists.Should().BeTrue();

        sut.Invoking(x => x.Create()).Should().NotThrow();
        sut.Exists.Should().BeTrue();
    }

    [TestMethod]
    public void CreateDirectoryInNonExistingRoot()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(@"Z:\\Test");
        sut.Exists.Should().BeFalse();

        sut.Invoking(x => x.Create())
           .Should().Throw<DirectoryNotFoundException>()
           .Where(x => x.Message.Contains("its file system root does not exist"), "the exception message should say that the file system root does not exist");
        sut.Exists.Should().BeFalse();
    }

    [TestMethod]
    public void CreateNonExistingStructureInExistingDirectory()
    {
        var sut = (MarkusPalcer.FileSystem.IDirectory)Common.CreateSampleData().GetDirectory(Common.ExistingDirectory).GetDirectory("Some").GetDirectory("Test").GetDirectory("Path");
        sut.Exists.Should().BeFalse();

        sut.Invoking(x => x.Create())
           .Should().Throw<DirectoryNotFoundException>()
           .Where(x => x.Message.Contains($" {Common.ExistingDirectory}\\Some "), "the exception message should contain the path to the lowest non-existing directory");
        sut.Exists.Should().BeFalse();
    }
}