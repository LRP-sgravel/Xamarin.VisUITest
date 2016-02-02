using System.Drawing;
using Imaging = AForge.Imaging;
using NUnit.Framework;

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
            Bitmap imageE = Imaging.Image.FromFile("../../../img/E.gif");
            Bitmap imageF = Imaging.Image.FromFile("../../../img/F.tif");
            Bitmap imageG = Imaging.Image.FromFile("../../../img/G.jpg");

            Assert.IsTrue(imageA.IsIdenticalTo(imageA));
            Assert.IsTrue(imageA.IsIdenticalTo(imageC));
            Assert.IsFalse(imageA.IsIdenticalTo(imageB));
            Assert.IsFalse(imageA.IsIdenticalTo(imageF));

            Assert.IsTrue(imageD.IsIdenticalTo(imageE));
            Assert.IsFalse(imageD.IsIdenticalTo(imageA));
            Assert.IsFalse(imageD.IsIdenticalTo(imageG));

            Assert.IsTrue(imageF.IsIdenticalTo(imageG));
            Assert.IsFalse(imageF.IsIdenticalTo(imageB));
        }
    }
}

