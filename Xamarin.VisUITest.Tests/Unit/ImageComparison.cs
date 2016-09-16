using System.Drawing;
using NUnit.Framework;
using Imaging = AForge.Imaging;

namespace Xamarin.VisUITest.Tests
{
    [TestFixture]
    public class ImageComparison
    {
        [Test]
        public void SameImageComparison()
        {
            Bitmap imageA = Imaging.Image.FromFile("../../../img/A.jpg");
            Bitmap imageB = Imaging.Image.FromFile("../../../img/B.png");
            Bitmap imageC = Imaging.Image.FromFile("../../../img/C.png");
            Bitmap imageD = Imaging.Image.FromFile("../../../img/D.jpg");
            Bitmap imageE = Imaging.Image.FromFile("../../../img/E.png");
            Bitmap imageF = Imaging.Image.FromFile("../../../img/F.tif");
            Bitmap imageG = Imaging.Image.FromFile("../../../img/G.jpg");

            Assert.IsTrue(imageA.IsIdenticalTo(imageA, 1));
            Assert.IsTrue(imageA.IsIdenticalTo(imageC, 1));
            Assert.IsFalse(imageA.IsIdenticalTo(imageB, 1));
            Assert.IsFalse(imageA.IsIdenticalTo(imageF, 1));

            Assert.IsTrue(imageD.IsIdenticalTo(imageE, 1));
            Assert.IsTrue(imageD.IsIdenticalTo(imageE, 1));
            Assert.IsFalse(imageD.IsIdenticalTo(imageA, 1));
            Assert.IsFalse(imageD.IsIdenticalTo(imageG, 1));

            Assert.IsTrue(imageF.IsIdenticalTo(imageG, 1));
            Assert.IsFalse(imageF.IsIdenticalTo(imageB, 1));
        }
    }
}

