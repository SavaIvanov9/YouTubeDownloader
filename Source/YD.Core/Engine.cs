using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YD.Common.Contracts;
using YD.Common.Exceptions;
using YD.Common.Models;
using YD.Services.Abstraction;
using YD.Services.Abstraction.CommandProcessing;
using YD.Services.Abstraction.UI;

namespace YD.Core
{
    public class Engine
    {
        private readonly IUIService uiService;
        private readonly ICommandProcessingService commandProcessor;
        private readonly IErrorLoggingService errorLogger;

        private Action executeOnStartup;
        private Action executeOnExit;
        private string defaultCommand;
        private Func<string, string> defaultCommandFormatInput;
        private Func<IEnumerable<string>, IEnumerable<string>> defaultCommandFormatOutput;
        private bool isStarted;
        private bool isWorkingAsync;
        private string exitCommand;

        public Engine(
            IUIService uiService,
            ICommandProcessingService commandProcessor,
            IErrorLoggingService errorLogger)
        {
            this.uiService = uiService;
            this.commandProcessor = commandProcessor;
            this.errorLogger = errorLogger;
            
            isStarted = false;
            isWorkingAsync = false;
            exitCommand = "exit";
        }

        public Engine ExecuteOnStartup(Action onStartup)
        {
            executeOnStartup = onStartup;
            return this;
        }

        public Engine ExecuteOnExit(Action onExit)
        {
            executeOnExit = onExit;
            return this;
        }

        public Engine UseExitCommand(string exitCommand)
        {
            this.exitCommand = exitCommand;
            return this;
        }

        public Engine WorkInAsyncMode()
        {
            this.isWorkingAsync = true;
            return this;
        }

        public Engine UseDefaultCommand(string command, Func<string, string> formatInput, Func<IEnumerable<string>, IEnumerable<string>> formatOutput)
        {
            defaultCommand = command;
            defaultCommandFormatInput = formatInput;
            defaultCommandFormatOutput = formatOutput;
            return this;
        }

        public void Start(ICommandRegister commandRegister, bool isErrorLoggingOn = false)
        {
            errorLogger.IsEnabled = isErrorLoggingOn;

            if (!isStarted)
            {
                isStarted = true;
                this.commandProcessor.LoadCommands(commandRegister);
                executeOnStartup?.Invoke();
            }
        }

        public async void ProcessInput(string commandLine)
        {
            if (commandLine == exitCommand)
            {
                if (commandProcessor.IsProcessing)
                {
                    uiService.WriteOutput($@"Some commands are being processed. Try again later or enter ""force {exitCommand}"" if you know what you are doing.", false);
                    return;
                }

                ShutDown();
                return;
            }

            if (commandLine == $"force {exitCommand}")
            {
                ShutDown();
                return;
            }

            if (string.IsNullOrWhiteSpace(commandLine))
            {
                uiService.WriteOutput(@"Unknown command. Type ""help"" or ""h"" for a list of commands.", false);
                return;
            }

            string commandParams;
            var index = commandLine.IndexOf("-", StringComparison.Ordinal);
            string command;

            if (index < 0)
            {
                commandParams = null;
                command = commandLine;
            }
            else
            {
                commandParams = commandLine.Substring(index + 1);
                command = commandLine.Substring(0, commandLine.Length - commandParams.Length - 1);
            }

            try
            {
                await ProcessCommand(command.Trim().ToLower(), commandParams, true);
            }
            catch (CommandNotImplementedException ex)
            {
                if (defaultCommand != null)
                {
                    var formatedInput = defaultCommandFormatInput.Invoke(commandLine);
                    await ProcessCommand(defaultCommand, formatedInput, false, defaultCommandFormatOutput);
                }
                else
                {
                    uiService.WriteOutput(ex.Message);
                }
            }
            catch (CustomException ex)
            {
                uiService.WriteOutput(ex.Message);
            }
            catch (Exception ex)
            {
                uiService.WriteOutput($"Error occured: {ex.Message}", false);

                if (errorLogger.IsEnabled)
                {
                    var logName = errorLogger.LogError(new Error { Exception = ex });
                    uiService.WriteOutput($"Error log created: {logName}");
                }
            }
        }

        private async Task ProcessCommand(string selector, string input, bool displayCommandName, Func<IEnumerable<string>, IEnumerable<string>> formatOutput = null)
        {
            IEnumerable<string> result;

            if (isWorkingAsync)
            {
                result = await commandProcessor.ProcessCommandAsync(selector, input, displayCommandName);
            }
            else
            {
                result = commandProcessor.ProcessCommand(selector, input, displayCommandName);
            }

            if (result != null && result.Count() > 0)
            {
                if (formatOutput != null)
                {
                    uiService.WriteOutput(formatOutput.Invoke(result));
                }
                else
                {
                    uiService.WriteOutput(result);
                }
            }
        }

        private void ShutDown()
        {
            executeOnExit?.Invoke();
            isStarted = false;
        }
    }
}
