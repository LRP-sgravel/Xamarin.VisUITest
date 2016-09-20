namespace Xamarin.VisUITest.App.Models
{
    public class ScreenSizes
    {
        public string WithoutStatus { get; set; }
        public string WithStatus { get; set; }

        public ScreenSizes(string withoutStatus, string withStatus)
        {
            WithoutStatus = withoutStatus;
            WithStatus = withStatus;
        }
    }
}