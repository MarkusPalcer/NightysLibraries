using FluentAssertions;

namespace MarkusPalcer.FileSystem.Testing.Test.TestingInterface;

[TestClass]
public class FileSystemTests
{
    [TestMethod]
    public void EmptyAtStartup()
    {
        var sut = new FileSystem();
        sut.EnumerateAllDirectories().Should().BeEmpty();
        sut.EnumerateAllFiles().Should().BeEmpty();
    }

    [TestMethod]
    public void GettingDirectoryCreatesEntries()
    {
        var sut = new FileSystem();

        sut.GetDirectory(@"C:\Test\Directory\Structure");

        sut.EnumerateAllDirectories().Select(x => x.FullPath).Should().BeEquivalentTo(
            @"C:",
            @"C:\Test",
            @"C:\Test\Directory",
            @"C:\Test\Directory\Structure"
        );
        sut.EnumerateAllDirectories().Should().AllSatisfy(x => x.Exists.Should().BeFalse());

        sut.EnumerateAllFiles().Should().BeEmpty();
    }

    [TestMethod]
    public void GettingFileCreatesEntries()
    {
        var sut = new FileSystem();

        sut.GetFile(@"C:\Test\Directory\Structure\MyFile.txt");

        sut.EnumerateAllDirectories().Select(x => x.FullPath).Should().BeEquivalentTo(
            @"C:",
            @"C:\Test",
            @"C:\Test\Directory",
            @"C:\Test\Directory\Structure"
        );
        sut.EnumerateAllDirectories().Should().AllSatisfy(x => x.Exists.Should().BeFalse());

        sut.EnumerateAllFiles().Select(x => x.FullPath).Should()
           .BeEquivalentTo(@"C:\Test\Directory\Structure\MyFile.txt");
        sut.EnumerateAllFiles().Should().AllSatisfy(x => x.Exists.Should().BeFalse());
    }

    [TestMethod]
    public void GettingNonExistingRoot()
    {
        var sut = Common.CreateSampleData();
        sut.GetDirectory(@"D:\").Exists.Should().BeFalse();
    }

    [TestMethod]
    public void GettingExistingRoot()
    {
        var sut = Common.CreateSampleData();
        sut.GetDirectory(@"C:\").Exists.Should().BeTrue();
    }

    [TestMethod]
    public void CreatingDirectoryCreatesWholePath()
    {
        var sut = new FileSystem();
        sut.CreateDirectory(@"C:\Test\Directory\Structure");
    }

}