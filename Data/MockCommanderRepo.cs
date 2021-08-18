using System.Collections.Generic;
using Commander.Models;

namespace Commander.Data
{
    public class MockCommanderRepo : ICommanderRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil an Egg", Line="Boil water", Platform="Kettle & Pan"},
                new Command{Id=1, HowTo="Cut Bread", Line="Get a knife", Platform="knife & chopping board"},
                new Command{Id=2, HowTo="Make a cup of tea", Line="place teabage in cup", Platform="Kettle & cup"}
            }; 
            
            return commands;
        }

        public Command GetCommandyById(int id)
        {
            return new Command{Id=0, HowTo="Boil an Egg", Line="Boil water", Platform="Kettle & Pan"};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }
    }
}