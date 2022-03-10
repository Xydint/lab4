using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive;
using ReactiveUI;

namespace AvaloniaApplication1.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        string?[] Romans;
        string result = "Enter Number";
        int mode;
        int op;
        string[] operators = { "+", "-", "*", "/" };
        bool restart = true;
        void Zero()
        {
            Romans[0] = null;
            Romans[1] = null;
            result = "";
            mode = 0;
            op = -1;
            restart = false;
        }
        string AddString(string str)
        {
            if (restart) Zero();
            if (Romans[mode] != null) Romans[mode] += str;
            else Romans[mode] = str;
            return result + str;
        }
        string Operation(string str)
        {
            int symb = Int32.Parse(str);
            if (mode == 0)
            {
                if (Romans[0] != null)
                {
                    op = symb;
                    mode = 1;
                    return result + operators[symb];
                }
                return result;
            }
            if (Romans[1] == null)
            {
                return result.Substring(0, result.Length - 2) + operators[symb];
            }
            return result;
        }

        string Equation()
        {
            restart = true;
            if (Romans[0] == null) return "Введ. знач.";
            if (mode == 0) return result;
            if (Romans[1] == null) Romans[1] = Romans[0];
            RomanNumberExtend r0, r1;
            try
            {
                r0 = new RomanNumberExtend(Romans[0]);
                r1 = new RomanNumberExtend(Romans[1]);
                if (op == 0) return (r0 + r1).ToString();
                if (op == 1) return (r0 - r1).ToString();
                if (op == 2) return (r0 * r1).ToString();
                if (op == 3) return (r0 / r1).ToString();
                return "Ошибка";
            }
            catch
            {
                return "Некорр. в-ие";
            }
        }

        public MainWindowViewModel()
        {
            Romans = new string?[2];
            OnClickCommand_Str = ReactiveCommand.Create<string, string>((str) => Greeting = AddString(str));
            OnClickCommand_Op = ReactiveCommand.Create<string, string>((symb) => Greeting = Operation(symb));
            OnClickCommand_Eq = ReactiveCommand.Create(() => Greeting = Equation());
        }
        public string Greeting
        {
            set
            {
                this.RaiseAndSetIfChanged(ref result, value);
            }
            get
            {
                return result;
            }
        }

        public ReactiveCommand<string, string> OnClickCommand_Str { get; }
        public ReactiveCommand<string, string> OnClickCommand_Op { get; }
        public ReactiveCommand<Unit, string> OnClickCommand_Eq { get; }
    }
}
