using DH_GUIPresenters;
using System.Collections.Generic;

namespace DH_GUICommands
{
    public class NullCommand : Command
    {

        public NullCommand()
        {
        }

        public CommandLineViewData Execute()
        {
            /*
            ConsoleWriter.WriteStrings(
                new List<string>()
                    {"Menu choice not recognised"});
            */

            List<string> nulldata = new List<string>() {"Menu choice not recognised"};
            CommandLineViewData data = new CommandLineViewData(nulldata);
            return data;
        }
    }
}
