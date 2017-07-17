using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Utils;

namespace Asm
{
    public class Program
    {
        private const string AceKeyName = "X-0x0ACE-Key";
        private const string AceKeyValue = "";
        private const string BaseUrl = "http://80.233.134.207/";
        private const string UriString = BaseUrl + "0x00000ACE.html";

        public static void Main()
        {
            string result = MainAsync().Result;
            Console.WriteLine(result);
            Console.ReadKey();
        }

        private static async Task<string> MainAsync()
        {
            Dictionary<string, string> headers = new Dictionary<string, string> {{AceKeyName, AceKeyValue}};

            string htmlString = await HttpTools.HttpGetAsync(UriString, headers).ConfigureAwait(false);
            htmlString = Regex.Replace(htmlString, @"\s+", "");

            Regex regex = new Regex(@"""/challenge\?(.*?)""");
            string challengeId = regex.Matches(htmlString)[0].Groups[1].Value;

            string address = BaseUrl + "challenge?" + challengeId;
            byte[] bytes = HttpTools.HttpGetBytesAsync(address, headers).Result;

            List<Instruction> instructions = DecodeInstructions(bytes);

            Cpu cpu = new Cpu();
            cpu.ExecuteInstructions(instructions);

            Dictionary<string, string> data = new Dictionary<string, string>();
            for (int i = 0; i < cpu.Registers.Length; i++)
            {
                data.Add($"reg{i}", cpu.Registers[i].ToString("x").PadLeft(4, '0'));
            }

            return await HttpTools.HttpPostAsync(address, headers, data).ConfigureAwait(false);
        }

        private static List<Instruction> DecodeInstructions(IReadOnlyList<byte> bytes)
        {
            List<Instruction> instructions = new List<Instruction>();

            for (int i = 0; i < bytes.Count; i += 2)
            {
                InstructionOpcodes opcode = (InstructionOpcodes) bytes[i];

                string binary = Convert.ToString(bytes[i + 1], 2).PadLeft(8, '0');
                int mod = Convert.ToInt32(binary.Substring(4, 4), 2);
                int src = Convert.ToInt32(binary.Substring(0, 2), 2);
                int dest = Convert.ToInt32(binary.Substring(2, 2), 2);
                ushort imm = 0;

                if (mod % 2 == 0)
                {
                    imm = (ushort) (bytes[i + 3] * 256 + bytes[i + 2]);
                    i += 2;
                }

                instructions.Add(new Instruction
                {
                    Opcode = opcode,
                    Mod = (InstructionMode) mod,
                    SrcReg = src,
                    DestReg = dest,
                    ImmediateValue = imm
                });
            }

            return instructions;
        }
    }
}