using NUnit.Framework;
using Pin.ProjectContainer;
using System.IO;
using System.Linq;

namespace Pin.Tests
{
    [TestFixture]
    public class ProjectDropBehaviorTest
    {
        [Test]
        public void CopyDirectoryTest()
        {
            // setup
            var SourcePath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            var DestinationPath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            Directory.CreateDirectory(SourcePath);

            // action
            ProjectDropBehavior.CopyDirectory(SourcePath, DestinationPath);

            // assert
            Assert.AreEqual(Directory.GetFiles(SourcePath).Replace(SourcePath), Directory.GetFiles(DestinationPath).Replace(DestinationPath));
            Assert.AreEqual(Directory.GetDirectories(SourcePath).Replace(SourcePath), Directory.GetDirectories(DestinationPath).Replace(DestinationPath));

            // cleanup
            Directory.Delete(SourcePath,true);
            Directory.Delete(DestinationPath, true);
        }

        [Test]
        public void CopyDirectoryWithNestedFileTest()
        {
            // setup
            var SourcePath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            var DestinationPath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            Directory.CreateDirectory(SourcePath);

            var FileName = File.AppendText(Path.Combine(SourcePath, "FileName.txt"));
            FileName.WriteLine("Content");
            FileName.Close();


            // action
            ProjectDropBehavior.CopyDirectory(SourcePath, DestinationPath);

            // assert
            Assert.AreEqual(Directory.GetFiles(SourcePath).Replace(SourcePath), Directory.GetFiles(DestinationPath).Replace(DestinationPath));
            Assert.AreEqual(Directory.GetDirectories(SourcePath).Replace(SourcePath), Directory.GetDirectories(DestinationPath).Replace(DestinationPath));

            // cleanup
            Directory.Delete(SourcePath, true);
            Directory.Delete(DestinationPath, true);
        }

        [Test]
        public void CopyDirectoryWithNestedFileAndDirectoryTest()
        {
            // setup
            var SourcePath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            var DestinationPath = ProjectDropBehavior.getCheckedRandomName(Path.GetTempPath());
            Directory.CreateDirectory(SourcePath);

            File.AppendAllText(Path.Combine(SourcePath, "FileName.txt"), "Content");
            Directory.CreateDirectory(Path.Combine(SourcePath, "DirectoryName"));

            // action
            ProjectDropBehavior.CopyDirectory(SourcePath, DestinationPath);

            // assert
            Assert.AreEqual(Directory.GetFiles(SourcePath).Replace(SourcePath), Directory.GetFiles(DestinationPath).Replace(DestinationPath));
            Assert.AreEqual(Directory.GetDirectories(SourcePath).Replace(SourcePath), Directory.GetDirectories(DestinationPath).Replace(DestinationPath));

            // cleanup
            Directory.Delete(SourcePath, true);
            Directory.Delete(DestinationPath, true);
        }
    }
}
