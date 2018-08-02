using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Shuffle.Logic;
using Shuffle.Model;
using static System.Console;

namespace Shuffle
{
    [ExcludeFromCodeCoverage] //Cannot Test Main Method
    internal static class Program
    {
        #region Fields

        //Add Logging
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();

        #endregion

        #region Methods

        private static void Main()
        {
            try
            {
                bool isConfigured = ConfigureConsole();
                if (!isConfigured)
                {
                    throw new ApplicationException("Error configuring console.");
                }

                GameFactory gameFactory = new GameFactory(new UserInterface(), new BoardFactory());
                gameFactory.StartGame();
                NLog.LogManager.Shutdown(); // Flush and close down internal threads and timers
            }
            catch (Exception exception)
            {
                Logger.Error(exception, $"An Error has occued: {0}", exception.Message);
                WriteLine($"An Error Occured: {exception}");
                WriteLine("Press Any Key to Exit");
                ReadKey();
            }
        }

        /// <summary>
        ///     Configure the console settings.
        /// </summary>
        private static bool ConfigureConsole()
        {
            InputEncoding = Encoding.UTF8;
            WindowWidth = 100;
            WindowHeight = 25;
            Title = "Shuffle! by Stuart Hopwood";
            Logger.Info("Console Configured");
            return true;
        }

        #endregion
    }
}