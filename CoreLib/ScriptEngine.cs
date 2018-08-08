using CoreExtLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib
{
    public static class ScriptEngine
    {
        public struct CommandMapping
        {
            public CommandAction Command { get; set; }
            public string Parameter { get; set; }
            public CommandMapping(CommandAction command , string parameter)
            {
                Command = command;
                Parameter = parameter;
            }

        }
        static string[] GetCommands(string scriptString)
        {
            string[] lines = scriptString.Replace("\r", "").Split('\n');
            List<string> retList = new List<string>();
            foreach (string line in lines)
            {
                retList.Add(DeRand(line));
            }
            return retList.ToArray();
        }

        static string DeRand(string str)
        {

            if (str.Contains('~'))
            {
                int index = 0;
                string s = "";
                while ((s = str.Substring(index)).Contains('~'))
                {
                    index = str.IndexOf('~', index) + 1;
                }
                int endIndex = str.IndexOf('^', index) + 1;

                if ((endIndex - index - 1) < 0)
                    return str;

                string extracted = str.Substring(index, endIndex - index - 1);
                string option = extracted.Split('|').ChooseRandomly();
                str = str.Remove(index - 1, extracted.Length + 2);
                str = str.Insert(index - 1, option);

                if (str.Contains('~'))
                {
                    str = DeRand(str);
                }

            }
            return str;
        }



        public static List<CommandMapping> GetActionMapping(string commandText)
        {
            string[] actionCommands = GetCommands(commandText);

          List< CommandMapping> actions = new List<CommandMapping>();
            foreach (string cmd in actionCommands)
            {
                string command = cmd;

                if (command.Trim().StartsWith("["))
                {
                    do
                    {
                        string actionString = command.Substring(1, command.IndexOf(']') - 1).ToLower();
                        switch (actionString)
                        {

                            case "type":
                                {

                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    actions.Add(new CommandMapping(CommandAction.type, param));
                                }
                                break;
                            case "print":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    actions.Add(new CommandMapping(CommandAction.print, param));
                                }
                                break;
                            case "sp":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    actions.Add(new CommandMapping(CommandAction.speak, param));

                                }
                                break;
                            case "delay":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    if (param.IsInt())
                                        actions.Add(new CommandMapping(CommandAction.delay, param));
                                }
                                break;

                            case "h":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    string parameters = param.Replace("<<", " ");
                                    actions.Add(new CommandMapping(CommandAction.highlight, parameters));

                                    break;
                                }
                            case "hk":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    string parameters = param.Replace("<<", " ");
                                    actions.Add(new CommandMapping(CommandAction.highlightKeys, parameters));

                                }
                                break;
                            case "uh":
                                actions.Add(new CommandMapping(CommandAction.unHiglight, null));

                                break;

                            case "board":
                                actions.Add(new CommandMapping(CommandAction.showBoard, null));
                                break;
                            case "view":
                                actions.Add(new CommandMapping(CommandAction.showView, null));

                                break;
                            case "lesson":
                                {
                                    string param = command.Trim().Substring(command.IndexOf(">>") + 2);
                                    actions.Add(new CommandMapping(CommandAction.lesson, param));
                                }
                                break;
                            case "enableui":
                                actions.Add(new CommandMapping(CommandAction.enableUI, null));
                                break;
                            case "disableui":
                                actions.Add(new CommandMapping(CommandAction.disableUI, null));
                                break;
                            case "showkb":
                                actions.Add(new CommandMapping(CommandAction.showKeyboard, null));
                                break;
                            case "hidekb":
                                actions.Add(new CommandMapping(CommandAction.hideKeyboard, null));
                                break;
                        }


                        command = command.Trim().Substring(command.IndexOf(']') + 1);
                    }
                    while (command.StartsWith('['));
                }
            }
            return actions;
        }
    }
}


