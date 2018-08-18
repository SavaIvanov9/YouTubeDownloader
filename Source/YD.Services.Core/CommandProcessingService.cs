using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Services.Abstraction.CommandProcessing;
using YD.Services.Abstraction.UI;

namespace YD.Services.Core
{
    public class CommandProcessingService : ICommandProcessingService
    {
        private readonly IUIService uiService;
        private readonly ICommandFactoryService commandFactory;
        private readonly ConcurrentBag<string> commandsBeingProcessed;

        public CommandProcessingService(ICommandFactoryService commandFactory, IUIService uiService)
        {
            this.uiService = uiService;
            this.commandFactory = commandFactory;
            this.commandsBeingProcessed = new ConcurrentBag<string>();
        }

        public bool IsProcessing
        {
            get
            {
                return this.commandsBeingProcessed.Count > 0 ? true : false;
            }
        }

        public IEnumerable<string> ProcessCommand(string commandSelector, string commandParams, bool displayCommandName)
        {
            if (commandSelector == "help" || commandSelector == "h")
            {
                var helpText = new List<string>();
                helpText.Add($"{this.commandFactory.Register.Commands.Count} available commands:");

                foreach (var command in commandFactory.Register.Commands)
                {
                    helpText.Add($"-Name: {command.Value.Name}");
                    helpText.Add($"-Selector: {command.Key}");

                    if (!string.IsNullOrWhiteSpace(command.Value.CommandFormat))
                    {
                        helpText.Add($"-Format: {command.Value.CommandFormat}");
                    }

                    helpText.Add($"-Description: {command.Value.CommandDescription}" + Environment.NewLine);
                }

                return helpText;
            }

            using (var command = this.commandFactory.CreateCommand(commandSelector))
            {
                this.TrackCommand(commandSelector);

                return this.HandleCommand(() =>
                    command.Execute(commandParams),
                    commandSelector,
                    string.IsNullOrWhiteSpace(command.Name) ? command.GetType().Name : command.Name,
                    displayCommandName);
            }
        }

        public Task<IEnumerable<string>> ProcessCommandAsync(string commandSelector, string commandParams, bool displayCommandName)
        {
            if (commandSelector == "help" || commandSelector == "h")
            {
                return Task<IEnumerable<string>>.Factory.StartNew(() =>
                {
                    var helpText = new List<string>();
                    helpText.Add($"{this.commandFactory.Register.Commands.Count} available commands:");

                    foreach (var command in this.commandFactory.Register.Commands)
                    {
                        helpText.Add($"-Name: {command.Value.Name}");
                        helpText.Add($"-Selector: {command.Key}");

                        if (!string.IsNullOrWhiteSpace(command.Value.CommandFormat))
                        {
                            helpText.Add($"-Format: {command.Value.CommandFormat}");
                        }

                        helpText.Add($"-Description: {command.Value.CommandDescription}" + Environment.NewLine);
                    }

                    return helpText;
                });
            }

            return Task<IEnumerable<string>>.Factory.StartNew(() =>
            {
                using (var command = this.commandFactory.CreateCommand(commandSelector))
                {
                    this.TrackCommand(commandSelector);

                    return this.HandleCommand(() =>
                        command.Execute(commandParams),
                        commandSelector,
                        string.IsNullOrWhiteSpace(command.Name) ? command.GetType().Name : command.Name,
                        displayCommandName);
                }
            });
        }

        private void TrackCommand(string commandSelector)
        {
            this.commandsBeingProcessed.Add(commandSelector);
        }

        private IEnumerable<string> HandleCommand(Func<IEnumerable<string>> execute, string commandSelector, string commandName, bool displayCommandName)
        {
            try
            {
                if (displayCommandName)
                {
                    this.uiService.WriteOutput($@"""{commandName}"" command started...", false);
                }

                return execute();
            }
            finally
            {
                this.commandsBeingProcessed.TryTake(out commandSelector);
            }
        }

        public void LoadCommands(ICommandRegister commandRegister)
        {
            this.commandFactory.Register = commandRegister;
        }
    }
}
