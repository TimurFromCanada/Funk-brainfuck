using System.Collections.Generic;

namespace func.brainfuck
{
    public class BrainfuckLoopCommands
    {
        class Instruction
        {
            public readonly Dictionary<int, int> End;
            public readonly Dictionary<int, int> Begin;

            public Instruction()
            {
                End = new Dictionary<int, int>();
                Begin = new Dictionary<int, int>();
            }

            public void AddToDictionary(string instructions)
            {
                var stack = new Stack<int>();

                for (var i = 0; i < instructions.Length; i++)
                {
                    if (instructions[i] == '[')
                        stack.Push(i);

                    if (instructions[i] == ']')
                    {
                        Begin[stack.Peek()] = i;
                        End[i] = stack.Pop();
                    }
                }
            }
        }

        public static void RegisterTo(IVirtualMachine vm)
        {
            var instruction = new Instruction();
            instruction.AddToDictionary(vm.Instructions);

            vm.RegisterCommand('[', b =>
            {
                if (b.Memory[b.MemoryPointer] == 0)
                    b.InstructionPointer = instruction.Begin[b.InstructionPointer];
            });
            vm.RegisterCommand(']', b => b.InstructionPointer = instruction.End[b.InstructionPointer] - 1);
        }
    }
}