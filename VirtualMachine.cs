using System;
using System.Collections.Generic;

namespace func.brainfuck
{
	public class VirtualMachine : IVirtualMachine
	{
		public Dictionary<char, Action<IVirtualMachine>> Dictionary = new Dictionary<char, Action<IVirtualMachine>>();

		public VirtualMachine(string program, int memorySize)
		{
			Instructions = program;
			MemoryPointer = 0;
			Memory = new byte[memorySize];
			InstructionPointer = 0;
		}

		public void RegisterCommand(char symbol, Action<IVirtualMachine> execute)
		{
			Dictionary.Add(symbol, execute);
		}

		public string Instructions { get; }
		public int InstructionPointer { get; set; }
		public byte[] Memory { get; }
		public int MemoryPointer { get; set; }

		public void Run()
		{
			for (; InstructionPointer < Instructions.Length; InstructionPointer++)
			{
				var command = Instructions[InstructionPointer];

				if (Dictionary.ContainsKey(command))
					Dictionary[command](this);
			}
		}
	}
}