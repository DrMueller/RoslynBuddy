﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Mmu.Mlh.WpfExtensions.Areas.MvvmShell.Commands;
using Mmu.Mlh.WpfExtensions.Areas.ViewExtensions.Components.CommandBars.ViewData;
using Mmu.Mlh.WpfExtensions.Areas.ViewExtensions.Dialogs.FolderDialogs.Services;
using Mmu.Rb.Application.Areas.Testing.Services;

namespace Mmu.Rb.WpfUI.Areas.Testing.ViewModels
{
    public class ModelTestingViewModelCommands : IViewModelCommandContainer<ModelTestingViewModel>
    {
        private readonly IFolderDialogService _folderDialogService;
        private readonly IModelTestInitializationService _modelTestInitializationService;
        private ModelTestingViewModel _context;
        public CommandsViewData Commands { get; private set; }

        public ViewModelCommand SelectFilePath
        {
            get
            {
                return new ViewModelCommand(
                    "..",
                    new RelayCommand(
                        () =>
                        {
                            var selectedPathResult = _folderDialogService.SelectFolder();
                            if (selectedPathResult.OkClicked)
                            {
                                _context.FilePath = selectedPathResult.FolderPath;
                            }
                        }));
            }
        }

        public ModelTestingViewModelCommands(
            IFolderDialogService folderDialogService,
            IModelTestInitializationService modelTestInitializationService)
        {
            _folderDialogService = folderDialogService;
            _modelTestInitializationService = modelTestInitializationService;
        }

        public async Task InitializeAsync(ModelTestingViewModel context)
        {
            _context = context;

            await Task.Run(
                () =>
                {
                    Commands = new CommandsViewData(
                        new List<ViewModelCommand>
                        {
                            new ViewModelCommand(
                                "Create Model Tests",
                                new RelayCommand(
                                    async () =>
                                    {
                                        await _modelTestInitializationService.InitializeAllAsync(context.FilePath);
                                    },
                                    () => !string.IsNullOrEmpty(context.FilePath)))
                        });
                });
        }
    }
}