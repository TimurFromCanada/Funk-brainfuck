using System;
using System.Collections.Generic;
using System.Linq;

namespace func.brainfuck
{
	public class BrainfuckBasicCommands
	{
		static readonly char[] arrayChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToArray();

		public static void RegisterTo(IVirtualMachine vm, Func<int> read, Action<char> write)
		{
			vm.RegisterCommand('.', b => write((char)b.Memory[b.MemoryPointer]));
			vm.RegisterCommand(',', b => b.Memory[b.MemoryPointer] = (byte)read());
			vm.RegisterCommand('+', b => Increment(b));
			vm.RegisterCommand('-', b => Decrement(b));
			vm.RegisterCommand('>', b => MoveNextMemoryPointer(b));
			vm.RegisterCommand('<', b => MovePreviousMemoryPointer(b));

			foreach (var e in arrayChar)
			{
				char temporary = e;
				vm.RegisterCommand(e, b => { b.Memory[b.MemoryPointer] = (byte)temporary; });
			}
		}

		private static void Increment(IVirtualMachine virtualMachine)
		{
			if (virtualMachine.Memory[virtualMachine.MemoryPointer] == 255)
				virtualMachine.Memory[virtualMachine.MemoryPointer] = 0;
			else
				virtualMachine.Memory[virtualMachine.MemoryPointer]++;
		}

		private static void Decrement(IVirtualMachine virtualMachine)
		{
			if (virtualMachine.Memory[virtualMachine.MemoryPointer] == 0)
				virtualMachine.Memory[virtualMachine.MemoryPointer] = 255;
			else
				virtualMachine.Memory[virtualMachine.MemoryPointer]--;
		}

		private static void MoveNextMemoryPointer(IVirtualMachine virtualMachine)
		{
			if (virtualMachine.MemoryPointer == virtualMachine.Memory.Length - 1)
				virtualMachine.MemoryPointer = 0;
			else
				virtualMachine.MemoryPointer++;
		}

		private static void MovePreviousMemoryPointer(IVirtualMachine virtualMachine)
		{
			if (virtualMachine.MemoryPointer == 0)
				virtualMachine.MemoryPointer = virtualMachine.Memory.Length - 1;
			else
				virtualMachine.MemoryPointer--;
		}
	}
}