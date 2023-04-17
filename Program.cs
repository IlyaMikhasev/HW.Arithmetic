using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static HW.Arithmetic.ParseINput;

namespace HW.Arithmetic
{
    internal class ParseINput {
       public ParseINput() {  }
       public void ParseInput(string input,int index)
        {
            _input = input;
            var regexleft = new Regex(@"\d+\s*(\+|\-|\*|\/)");
            var regexrigth = new Regex(@"(\+|\-|\*|\/)\s*\d+");
            var regNum = new Regex(@"\d+");
            var regexOperator = new Regex(@"(\+|\-|\*|\/)");
            MatchCollection matchesleft = regexleft.Matches(_input);
            MatchCollection numLeft = regNum.Matches(matchesleft[0].ToString());
            MatchCollection matchesrigth = regexrigth.Matches(_input);
            MatchCollection numrigth = regNum.Matches(matchesrigth[0].ToString());                    
            MatchCollection operat = regexOperator.Matches(matchesleft[0].ToString());
            try
            {               
                _OperandLeft.Add(int.Parse(numLeft[0].ToString()));
                _OperandRight.Add(int.Parse(numrigth[0].ToString()));
                _operat.Add(char.Parse(operat[0].ToString()));
            }
            catch {
                Console.WriteLine("Некоректный ввод данных");
                _OperandLeft.Add(0);
                _OperandRight.Add(1);
                _operat.Add('+');
            }
        }
        string _input ;
        public List<int> _OperandLeft = new List<int>();
        public List<int> _OperandRight = new List<int>();
        public List<char> _operat = new List<char>();
    public class MathFile {
        public MathFile(string filename, int index) { _filePath = filename;_index = index;_parseI = new ParseINput(); }
            public void FileReader() {
                var streamReader = new StreamReader(_filePath);
                
                for (int i=0; !streamReader.EndOfStream;i++)
                {
                    string strArithmetic = streamReader.ReadLine();
                    _parseI.ParseInput(strArithmetic,i);
                }
                streamReader.Close();
            }
            public void FileWriter()
            {
                var streamWriter = new StreamWriter("solution" + _index + ".txt");
                int resault;
                for (int i = 0; i < _parseI._operat.Count; i++)
                {
                    switch (_parseI._operat[i])
                    {
                        case '+': resault = _parseI._OperandLeft[i] + _parseI._OperandRight[i]; break;
                        case '-': resault = _parseI._OperandLeft[i] - _parseI._OperandRight[i]; break;
                        case '*': resault = _parseI._OperandLeft[i] * _parseI._OperandRight[i]; break;
                        case '/':
                            if (_parseI._OperandRight[i] != 0)
                            {
                                resault = _parseI._OperandLeft[i] / _parseI._OperandRight[i];
                            }
                            else
                            {
                                Console.WriteLine("Ошибка:на ноль нельзя делить");
                                resault = -1;
                            }; break;
                        default: resault = -1; break;
                    }
                    streamWriter.WriteLine($"{_parseI._OperandLeft[i]} {_parseI._operat[i]} {_parseI._OperandRight[i]} = {resault}");
                }
                    streamWriter.Close();
            }
            int _index;
            string _filePath;
            ParseINput _parseI ;


    }
    }
    internal class Program
    {
		
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    for(int i=0;i<args.Length;i++){
                        MathFile mathFile = new MathFile(args[i],i);
                        mathFile.FileReader();
                        mathFile.FileWriter();
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("неверный формат файла или файла не существует");
                }
            }
            else {
                Console.WriteLine( "аргументы не переданны" );
            }
        }
    }
}
