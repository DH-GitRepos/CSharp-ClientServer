﻿using Controllers;
using Presenters;

namespace Commands
{
    class InitialiseDatabaseCommand : Command
    {

        public InitialiseDatabaseCommand()
        {
        }

        public void Execute()
        {
            InitialiseDatabaseController controller =
                new InitialiseDatabaseController(
                        new MessagePresenter());

            CommandLineViewData data =
                (CommandLineViewData)controller.Execute();

            ConsoleWriter.WriteStrings(data.ViewData);
        }
    }
}
