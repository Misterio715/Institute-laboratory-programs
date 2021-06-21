using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab9
{
    class Calculator
    {
        private int curr = 0;

        public Calculator(int result) {
            curr = result;
        }

        public int Operation(char @operator, int operand)
        {
            switch (@operator)
            {
                case '+': curr += operand; break;
                case '-': curr -= operand; break;
                case '*': curr *= operand; break;
                case '/': curr /= operand; break;
            }
            // Напрямую меняет текст в main
            main.Instance.set_result(curr.ToString());

            return curr;
        }
    }

    abstract class Command {
        public abstract int Execute();
        public abstract int UnExecute();
    }

    class CalculatorCommand : Command {
        public char @operator;
        public int operand;
        Calculator calculator;

        // Constructor
        public CalculatorCommand(Calculator calculator, char @operator, int operand)
        {
            this.calculator = calculator;
            this.@operator = @operator;
            this.operand = operand;
        }
        public char Operator
        { set { @operator = value; } }

        public int Operand
        { set { operand = value; } }

        public override int Execute()
        { return calculator.Operation(@operator, operand); }

        public override int UnExecute()
        { return calculator.Operation(Undo(@operator), operand); }

        private char Undo(char @operator)
        {
            char undo;
            switch (@operator)
            {
                case '+': undo = '-'; break;
                case '-': undo = '+'; break;
                case '*': undo = '/'; break;
                case '/': undo = '*'; break;
                default: undo = ' '; break;   
            }
            return undo;
        }
    }
}
