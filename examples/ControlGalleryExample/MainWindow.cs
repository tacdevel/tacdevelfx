using TCD.Drawing;
using TCD.UI;
using TCD.UI.Controls.Containers;

namespace ControlGalleryExample
{
    public class MainWindow : Window
    {
        private TabContainer tabContainer = new TabContainer();
        private readonly BasicControlsTab basicControlsTab = new BasicControlsTab();
        //private readonly NumbersTab numbersTab = new NumbersTab();
        //private readonly DataChoosersTab dataChoosersTab = new DataChoosersTab();

        public MainWindow() : base("TCD.UI Control Gallery", new Size(640, 480), true) => InitializeComponent();

        protected override void InitializeComponent()
        {
            IsMargined = true;
            Child = tabContainer;

            tabContainer.Children.Add(basicControlsTab);
            //tabContainer.Children.Add(numbersTab);
            //tabContainer.Children.Add(dataChoosersTab);
        }
    }
}