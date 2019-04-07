using System.Windows.Controls;
using Mmu.Mlh.WpfExtensions.Areas.MvvmShell.Views.Models;
using Mmu.Rb.WpfUI.Areas.Testing.ViewModels;

namespace Mmu.Rb.WpfUI.Areas.Testing.Views
{
    public partial class ModelTestingView : UserControl, IViewMap<ModelTestingViewModel>
    {
        public ModelTestingView()
        {
            InitializeComponent();
        }
    }
}