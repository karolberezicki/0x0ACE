using System;
using System.Collections.Generic;

namespace Asm
{
    public class Cpu
    {
        public Cpu()
        {
            Registers = new ushort[4];
            InstructionPointer = 0;
            ZeroFlag = false;
            Stack = new Stack<ushort>();
        }

        public ushort[] Registers { get; set; }
        public int InstructionPointer { get; set; }
        public bool ZeroFlag { get; set; }

        public Stack<ushort> Stack { get; set; }

        public void ExecuteInstructions(List<Instruction> instructions)
        {
            while (instructions.Count > InstructionPointer)
            {
                Instruction instruction = instructions[InstructionPointer];

                switch (instruction.Opcode)
                {
                    case InstructionOpcodes.Move:
                        Move(instruction);
                        break;
                    case InstructionOpcodes.BitwiseOr:
                        BitwiseOr(instruction);
                        break;
                    case InstructionOpcodes.BitwiseXor:
                        BitwiseXor(instruction);
                        break;
                    case InstructionOpcodes.BitwiseAnd:
                        BitwiseAnd(instruction);
                        break;
                    case InstructionOpcodes.BitwiseNegation:
                        BitwiseNegation(instruction);
                        break;
                    case InstructionOpcodes.Addition:
                        Addition(instruction);
                        break;
                    case InstructionOpcodes.Subtraction:
                        Subtraction(instruction);
                        break;
                    case InstructionOpcodes.Multiplication:
                        Multiplication(instruction);
                        break;
                    case InstructionOpcodes.ShiftLeft:
                        ShiftLeft(instruction);
                        break;
                    case InstructionOpcodes.ShiftRight:
                        ShiftRight(instruction);
                        break;
                    case InstructionOpcodes.Increment:
                        Increment(instruction);
                        break;
                    case InstructionOpcodes.Decrement:
                        Decrement(instruction);
                        break;
                    case InstructionOpcodes.PushOnStack:
                        PushOnStack(instruction);
                        break;
                    case InstructionOpcodes.PopFromStack:
                        PopFromStack(instruction);
                        break;
                    case InstructionOpcodes.Compare:
                        Compare(instruction);
                        break;
                    case InstructionOpcodes.JumpNotZero:
                        JumpNotZero(instruction);
                        break;
                    case InstructionOpcodes.JumpWhenZero:
                        JumpWhenZero(instruction);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                InstructionPointer++;
            }
        }

        private void Move(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = instruction.ImmediateValue;
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = Registers[instruction.SrcReg];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void BitwiseOr(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] | instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] | Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void BitwiseXor(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] ^ instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] ^ Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void BitwiseAnd(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] & instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] & Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void BitwiseNegation(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = (ushort) ~Registers[instruction.DestReg];
                    break;
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)~instruction.ImmediateValue;
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)~Registers[instruction.SrcReg];
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void Addition(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] + instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] + Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void Subtraction(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] - instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] - Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void Multiplication(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] * instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] * Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void ShiftLeft(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] << 1);
                    break;
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] << instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] << Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void ShiftRight(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] >> 1);
                    break;
                case InstructionMode.RegisterImmediate:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] >> instruction.ImmediateValue);
                    break;
                case InstructionMode.RegisterRegister:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] >> Registers[instruction.SrcReg]);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void Increment(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = (ushort) (Registers[instruction.DestReg] + 1);
                    break;
                case InstructionMode.RegisterImmediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterRegister:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);

        }

        private void Decrement(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = (ushort)(Registers[instruction.DestReg] - 1);
                    break;
                case InstructionMode.RegisterImmediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterRegister:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
            ZeroFlagSet(Registers[instruction.DestReg]);
        }

        private void PushOnStack(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    Stack.Push(instruction.ImmediateValue);
                    break;
                case InstructionMode.Register:
                    Stack.Push(Registers[instruction.SrcReg]);
                    break;
                case InstructionMode.RegisterImmediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterRegister:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void PopFromStack(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    Registers[instruction.DestReg] = Stack.Pop();
                    break;
                case InstructionMode.RegisterImmediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterRegister:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

        }

        private void Compare(Instruction instruction)
        {
            ushort test;
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.Register:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterImmediate:
                    test = (ushort)(Registers[instruction.DestReg] - instruction.ImmediateValue);
                    ZeroFlagSet(test);
                    break;
                case InstructionMode.RegisterRegister:
                    test = (ushort)(Registers[instruction.DestReg] - Registers[instruction.SrcReg]);
                    ZeroFlagSet(test);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void JumpNotZero(Instruction instruction)
        {
            if (!ZeroFlag)
            {
                Jump(instruction);
            }
        }

        private void JumpWhenZero(Instruction instruction)
        {
            if (ZeroFlag)
            {
                Jump(instruction);
            }
        }

        private void Jump(Instruction instruction)
        {
            switch (instruction.Mod)
            {
                case InstructionMode.Immediate:
                    InstructionPointer = instruction.ImmediateValue - 1;
                    break;
                case InstructionMode.Register:
                    InstructionPointer = Registers[instruction.SrcReg] - 1;
                    break;
                case InstructionMode.RegisterImmediate:
                    throw new ArgumentOutOfRangeException();
                case InstructionMode.RegisterRegister:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void ZeroFlagSet(ushort lastResult)
        {
            ZeroFlag = lastResult == 0;
        }

    }
}