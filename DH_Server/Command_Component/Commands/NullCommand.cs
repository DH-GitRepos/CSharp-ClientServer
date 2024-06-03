﻿using System.Collections.Generic;

namespace Commands
{
    class NullCommand : Command
    {

        public NullCommand()
        {
        }

        public void Execute()
        {
            ConsoleWriter.WriteStrings(
                new List<string>()
                    {"Menu choice not recognised"});
        }
    }
}
