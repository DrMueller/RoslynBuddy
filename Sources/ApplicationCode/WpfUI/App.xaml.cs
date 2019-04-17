using System;
using System.Windows;
using Mmu.Mlh.LanguageExtensions.Areas.Types.Maybes;
using Mmu.Mlh.ServiceProvisioning.Areas.Initialization.Models;
using Mmu.Mlh.WpfExtensions.Areas.Initialization;

namespace Mmu.Rb.WpfUI
{
    /// <summary>
    ///     Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override async void OnStartup(StartupEventArgs e)
        {
            var appIcon = WpfUI.Properties.Resources.M;
            var assemblyParameters = ContainerConfiguration.CreateFromAssembly(typeof(App).Assembly);

            var appConfig = ApplicationConfiguration.CreateFromIcon("Hello Test", appIcon);
            await BootstrapService.StartUpAsync(assemblyParameters, appConfig, Maybe.CreateNone<Action>());
        }
    }
}