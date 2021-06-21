using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    class User
    {
        // Initializers
        private Calculator _calculator = null;
        public List<Command> _commands = new List<Command>();
        public int _current = 0;
        public int result;

        public User(int result) {
            this._calculator = new Calculator(result);
            this.result = result;
        }

        public int Redo(int levels)
        {
            int answer = -1;
            if (_current < _commands.Count)
                answer = _commands[_current++].Execute();
            return answer;
        }

        public int Undo(int levels)
        {
            if (_current > 0)
                return _commands[--_current].UnExecute();
            return result;
        }

        public int Compute(char @operator, int operand)
        {
            Command command = new CalculatorCommand(_calculator, @operator, operand);
            if (_current != _commands.Count)
            {
                for (int i = _current; i < _commands.Count; i++)
                {
                    _commands.RemoveAt(i);
                }
            }
            _commands.Add(command);
            _current++;

            return command.Execute();
        }
    }
}

