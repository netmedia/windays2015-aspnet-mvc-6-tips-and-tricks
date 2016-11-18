namespace Windays2016.Views.Components
{
    public class ItemViewModel
    {
        public string Name { get; }
        public string TargetUrl { get; }

        public ItemViewModel(string name, string targetUrl)
        {
            Name = name;
            TargetUrl = targetUrl;
        }
    }
}