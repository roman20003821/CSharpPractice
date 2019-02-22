using RomanProject.ViewModel;
using System.Windows.Controls;

namespace RomanProject.View
{
    /// <summary>
    /// Interaction logic for PIckBirthdayControl.xaml
    /// </summary>
    public partial class PickBirthdayControl : UserControl
    {
        public PickBirthdayControl()
        {
            InitializeComponent();
            DataContext = new PickDateViewModel();
        }
    }
}
