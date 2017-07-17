using System.Diagnostics;

namespace Asm
{
    [DebuggerDisplay("{Opcode}, Mod: {(int)Mod}, Src: {SrcReg}, Dest: {DestReg}, Imm: {ImmediateValue}")]
    public class Instruction
    {
        public InstructionOpcodes Opcode { get; set; }
        public InstructionMode Mod { get; set; }
        public int SrcReg { get; set; }
        public int DestReg { get; set; }
        public ushort ImmediateValue { get; set; }
    }
}