using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Shuffle.Logic;
using Shuffle.Model;

namespace Shuffle
{
    [ExcludeFromCodeCoverage]
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
                Console.WriteLine($"An Error Occured: {exception}");
                Console.WriteLine("Press Any Key to Exit");
                Console.ReadKey();
            }
        }

        /// <summary>
        ///     Configure the console settings.
        /// </summary>
        [ExcludeFromCodeCoverage]
        private static bool ConfigureConsole()
        {
            Console.InputEncoding = Encoding.UTF8;
            Console.Title = "Shuffle! by Stuart Hopwood";
            Logger.Info("Console Configured");
            return true;
        }

        #endregion
    }
}