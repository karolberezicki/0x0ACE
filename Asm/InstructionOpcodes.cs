namespace Asm
{
    public enum InstructionOpcodes
    {
        Move, // 0x00
        BitwiseOr, // 0x01
        BitwiseXor, // 0x02
        BitwiseAnd, // 0x03
        BitwiseNegation, // 0x04
        Addition, // 0x05
        Subtraction, // 0x06
        Multiplication, // 0x07
        ShiftLeft, // 0x08
        ShiftRight, // 0x09
        Increment, // 0x0a
        Decrement, // 0x0b
        PushOnStack, // 0x0c
        PopFromStack, // 0x0d
        Compare, // 0x0e
        JumpNotZero, // 0x0f
        JumpWhenZero // 0x10
    }
}