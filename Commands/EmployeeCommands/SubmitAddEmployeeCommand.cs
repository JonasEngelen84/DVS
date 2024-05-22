﻿using DVS.Stores;
using DVS.ViewModels.View_ViewModels;

namespace DVS.Commands.EmployeeCommands
{
    public class SubmitAddEmployeeCommand : CommandBase
    {
        private readonly ModalNavigationStore _modalNavigationStore;

        public SubmitAddEmployeeCommand(AddEmployeeViewModel addEmployeeViewModel, ModalNavigationStore modalNavigationStore)
        {
            _modalNavigationStore = modalNavigationStore;
        }

        public override void Execute(object parameter) => _modalNavigationStore.Close();
    }
}
