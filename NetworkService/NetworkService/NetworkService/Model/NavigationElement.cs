using NetworkService.Helpers;

namespace NetworkService.Model
{
    public class NavigationElement//klasa koja se koristi u dictionary-u za navigiranje kroz prozore
    {
        public BindableBase ViewModel { get; set; }
        public string Title { get; set; }
        public string Color0 { get; set; }
        public string Color1 { get; set; }
        public string Color2 { get; set; }
        public string Color3 { get; set; }
        public string Help { get; set; }

        public NavigationElement(BindableBase viewModel, string title, string color0, string color1, string color2, string color3, string help)
        {
            ViewModel = viewModel;
            Title = title;
            Color0 = color0;
            Color1 = color1;
            Color2 = color2;
            Color3 = color3;
            Help = help;
        }
    }
}
