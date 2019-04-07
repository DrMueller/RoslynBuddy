using System.Threading.Tasks;
using Mmu.Mlh.WpfExtensions.Areas.MvvmShell.Commands;
using Mmu.Mlh.WpfExtensions.Areas.MvvmShell.ViewModels.Behaviors;
using Mmu.Mlh.WpfExtensions.Areas.MvvmShell.ViewModels.Models;
using Mmu.Mlh.WpfExtensions.Areas.ViewExtensions.Components.CommandBars.ViewData;

namespace Mmu.Rb.WpfUI.Areas.Testing.ViewModels
{
    public class ModelTestingViewModel : ViewModelBase, IMainNavigationViewModel, IViewModelWithHeading, IInitializableViewModel
    {
        private readonly ModelTestingViewModelCommands _commandsContainer;
        public CommandsViewData Commands => _commandsContainer.Commands;
        public string HeadingText { get; } = "Model Testing";
        public string NavigationDescription { get; } = "Model Testing";
        public int NavigationSequence { get; } = 1;
        public string FilePath { get; set; }
        public ViewModelCommand SelectFilePath => _commandsContainer.SelectFilePath;

        public ModelTestingViewModel(ModelTestingViewModelCommands commandsContainer)
        {
            _commandsContainer = commandsContainer;
            FilePath = @"C:\Users\matthias.mueller\Desktop\Test";
        }

        public async Task InitializeAsync()
        {
            await _commandsContainer.InitializeAsync(this);
        }
    }
}